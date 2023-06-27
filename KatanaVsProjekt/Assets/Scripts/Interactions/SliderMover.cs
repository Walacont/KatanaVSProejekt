using UnityEngine;
using UnityEngine.UI;

namespace haw.unitytutorium
{
    public class SliderMover : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private float minVal;
        [SerializeField] private float maxVal;
        
        private void Update()
        {
            var val = Util.Map01(slider.normalizedValue, minVal, maxVal);
            var diff = val - transform.localPosition.z;
            transform.Translate(Vector3.forward * diff, Space.Self);
        }
    }
}