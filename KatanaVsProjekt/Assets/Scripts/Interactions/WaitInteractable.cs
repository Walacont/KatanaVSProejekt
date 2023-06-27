using System.Collections;
using UnityEngine;

namespace haw.unitytutorium
{
    public class WaitInteractable : Interactable
    {
        [SerializeField] private float WaitTimeInSeconds;

        public void StartWaitTimer()
        {
            StartCoroutine(Wait(WaitTimeInSeconds));
        }

        private IEnumerator Wait(float duration)
        {
            yield return new WaitForSeconds(duration);
            Notify();
        }
    }
}