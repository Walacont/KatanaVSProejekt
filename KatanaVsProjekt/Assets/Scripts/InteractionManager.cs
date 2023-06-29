using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionManager : MonoBehaviour, IInteractionListener
{
    [SerializeField] private AudioSource correctSound;
    [SerializeField] private AudioSource errorSound;
    [SerializeField] private InteractionUI ui;
    [SerializeField] private List<Interaction> interactions;

    [SerializeField] private UnityEvent OnCompleted;

    // You can use properties to return logic (boolean or any other kind), which is quite useful. 
    public bool InteractionsCompleted => interactionIndex >= interactions.Count;

    private int interactionIndex;
    private Interaction currentInteraction;
    private bool interactionInProgress;

    private int errorCount;
    private int helpCount;

    private void Start()
    {
        // Display a little warning if no interactions are defined in the interaction manager.
        if (interactions.Count == 0)
        {
            Debug.LogWarning("Keine Interactions im InteractionManager");
            return;
        }

        // Set the first interaction from the list as our current interaction and display its instruction in the ui.
        currentInteraction = interactions[interactionIndex];
        ui.DisplayInstruction(currentInteraction.Instruction);
    }

    private void Update()
    {
        // Users can request help with the H key as long as we still have "open" interactions (the training is not completed).
        if (Input.GetKeyDown(KeyCode.H) && !InteractionsCompleted)
        {
            // If your help counter is limited (because you display the help permanently after it was requested)
            // then you can do this ...
            // if (!currentInteraction.HelpCounted)
            // {
            //     helpCount++;
            //     currentInteraction.HelpCounted = true;
            // }
            // otherwise just do ...
            helpCount++;

            ui.DisplayHelp(currentInteraction.HelpMsg, helpCount);
        }
    }

    private void OnEnable()
    {
        var globalInteractables = FindObjectsOfType<Interactable>(true);

        foreach (var interactable in globalInteractables)
        {
            interactable.AddListener(this);
        }
    }

    private void OnDisable()
    {
        var globalInteractables = FindObjectsOfType<Interactable>(true);

        foreach (var interactable in globalInteractables)
        {
            interactable.RemoveListener(this);
        }
    }

    /// <summary>
    /// This is called whenever ANY interactable in the scene calls its Notify method. It checks if the interaction that the user performed was the correct one
    /// and updates the training simulation accordingly.
    /// </summary>
    /// <param name="interactable"></param>
    public void OnNotify(Interactable interactable)
    {
        // if we are currently processing an interaction (which for example plays an animation) we don't want another user interaction to be processed
        // so we can this flag to lock the interaction manager.
        if (interactionInProgress)
            return;

        // We don't have any more interactions in our list. This means the training is completed and we don't need to process any further interaction.
        if (InteractionsCompleted)
            return;

        // check if the interactable that notified the interaction manager equals the interactable of the required current interaction
        // (e.g. did the user click the correct gameObject) 
        if (interactable.Equals(currentInteraction.Interactable))
        {
            // This is how you start a coroutine in Unity.
            // You find more explanations below in the method 'UpdateInteraction'
            StartCoroutine(UpdateInteraction());
        }
        // If the user performed an interaction that is not the current interaction he / she did something wrong (e.g. clicked the wrong gameObject)
        // so we display the error message of our current interaction, increment the error count and play the errorSound
        else
        {
            ui.DisplayError(currentInteraction.ErrorMsg, ++errorCount);
            errorSound.Play();
        }
    }

    /// <summary>
    /// This is a function that runs as a coroutine.
    /// It is a control structure that allows us to "pause" the execution of code under certain conditions (e.g. Wait for a finite amount of time).
    /// </summary>
    /// <returns></returns>
    private IEnumerator UpdateInteraction()
    {
        // first we lock the interaction manager so that no other interactions are processed until the current one is completed.
        interactionInProgress = true;

        // if any help or error messages are displayed in the UI we stop them (set their text fields to "")
        ui.StopHelpAndErrorDisplay();

        // We invoke the Unity Event that should fire immediately when the interactable notifies the manager
        // This is usually used to start animations that should be played when the user interacts with anything in our scene
        currentInteraction.OnStart?.Invoke();

        // Play the correctSound when the user interacted with the correct Interactable
        correctSound.Play();

        // We then PAUSE the code execution for the duration of the interaction (e.g. for the duration of the animation that we want to play)
        // The yield statement is the most important (and interesting) part about this function
        // it marks a sort of "re-entry" point at which we continue our code execution after we returned a WaitForSeconds object.
        yield return new WaitForSeconds(currentInteraction.Duration);

        // After we have waited for the time of the interaction duration our interaction should be completed
        // so we raise the UnityEvent that should fire at the end of the interaction.
        // This event is usually used to trigger the transition of the camera.
        currentInteraction.OnEnd?.Invoke();

        yield return new WaitForSeconds(currentInteraction.DurationExtra);

        currentInteraction.OnExtra?.Invoke();

        // after the current interaction has been completed we increment the interactionIndex;
        interactionIndex++;

        // If we still have "next" interactions in our list of interactions
        // then we update the current interaction and display its instruction in the UI (which is the next thing the user has to do).
        if (interactionIndex < interactions.Count)
        {
            currentInteraction = interactions[interactionIndex];
            ui.DisplayInstruction(currentInteraction.Instruction);
        }
        else
        {
            // All interactions have been completed and we reached the end of the training
            // We tell the UI the final help and error count and fire an extra Unity Event at the end of the training.
            // This can for example be used to show the final score screen to the user or display a custom "Thank you for using deutsche Bahn" text :) 
            ui.SetResultsPanel(errorCount, helpCount);
            OnCompleted?.Invoke();
        }

        // At the end we unlock the Interaction Manager to process interactions again.
        interactionInProgress = false;
    }
}