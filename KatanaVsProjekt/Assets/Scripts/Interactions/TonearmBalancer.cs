using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace haw.unitytutorium
{
    [RequireComponent(typeof(Rigidbody))]
    public class TonearmBalancer : Interactable
    {
        [SerializeField] private Slider balanceLoadingBar;
        [SerializeField] private TextMeshProUGUI balanceLabel;
        [SerializeField] private float balanceTime = 5.0f;
        private float timer = 0.0f;
        private bool balanced;
        private Rigidbody rb;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.centerOfMass = transform.localPosition;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (balanced)
                return;
            
            balanceLoadingBar.gameObject.SetActive(true);
            balanceLabel.gameObject.SetActive(true);
            timer = 0.0f;
        }

        private void OnTriggerExit(Collider other)
        {
            if (balanced)
                return;
            
            balanceLoadingBar.gameObject.SetActive(false);
            balanceLabel.gameObject.SetActive(false);
            timer = 0.0f;
        }

        private void OnTriggerStay(Collider other)
        {
            if (balanced)
                return;

            timer += Time.deltaTime;
            balanceLoadingBar.value = timer;

            if (timer > balanceTime)
            {
                balanced = true;
                rb.isKinematic = true;
                Notify();
            }
        }
    }
}