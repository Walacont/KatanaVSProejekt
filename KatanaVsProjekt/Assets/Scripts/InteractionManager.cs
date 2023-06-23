using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI instructionLabel;
    [SerializeField] private TextMeshProUGUI helpLabel;
    [SerializeField] private TextMeshProUGUI errorLabel;
    [SerializeField] private LayerMask layerMask;
    
    [SerializeField] private List<Interaction> interactions;
    private Interaction currentInteraction;
    private int interactionIndex;
    
    private Camera cam;

    private void Awake() => cam = Camera.main;

    private void Start()
    {
        helpLabel.SetText("");
        errorLabel.SetText("");

        currentInteraction = interactions[interactionIndex];
        instructionLabel.SetText(currentInteraction.Instruction);
    }

    void Update()
    {
        DebugDrawRay();

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 20.0f, layerMask))
            {
                CheckInteractionOrder(hit.transform.gameObject);
            }
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            // helpCount++
            StopHelpAndErrorDisplay();
            StartCoroutine(DisplayForDuration(helpLabel, currentInteraction.HelpMsg, 5.0f));
        }
    }

    private void CheckInteractionOrder(GameObject selectedGameObject)
    {
        if (selectedGameObject.Equals(currentInteraction.GameObject))
        {
            StopHelpAndErrorDisplay();
            currentInteraction.OnExecution?.Invoke();
            
            interactionIndex++;
            if(interactionIndex >= interactions.Count)
                return;
            
            currentInteraction = interactions[interactionIndex];
            instructionLabel.SetText(currentInteraction.Instruction);
        }
        else
        {
            StartCoroutine(DisplayForDuration(errorLabel, currentInteraction.ErrorMsg, 7.0f));
        }
    }

    private void DebugDrawRay()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 20.0f, layerMask))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.green);
        }
        else
        {
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * 20.0f, Color.red);
        }
    }
    
    /// <summary>
    /// This coroutine displays a text (msg) for a fixed number of seconds (duration)
    /// on a Text UI Element (label).
    /// </summary>
    private IEnumerator DisplayForDuration(TextMeshProUGUI label, string msg, float duration)
    {
        label.text = msg;
        yield return new WaitForSeconds(duration);
        label.text = "";
    }

    private void StopHelpAndErrorDisplay()
    {
        StopAllCoroutines();
        errorLabel.SetText("");
        helpLabel.SetText("");
    }
}
