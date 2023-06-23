using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class TriggerCube : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI triggerLabel;
    [SerializeField] private GameObject objectToDestroy;

    private void Start()
    {
        triggerLabel.SetText("");
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            triggerLabel.SetText("Trigger ENTER");
            objectToDestroy.SetActive(false);
        }
    }
    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
            triggerLabel.SetText("Trigger STAY");
    }
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
            triggerLabel.SetText("Trigger EXIT");
    }
}
