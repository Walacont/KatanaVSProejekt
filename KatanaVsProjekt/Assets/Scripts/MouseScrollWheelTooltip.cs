using TMPro;
using UnityEngine;


namespace haw.unitytutorium
{
    public class MouseScrollWheelTooltip : MonoBehaviour
    {
        [SerializeField] private RectTransform canvasRectTransform;
        [SerializeField] private TextMeshProUGUI label;
        private MouseScrollWheelInteractable mouseScrollWheelInteractable;
        private RectTransform rectTransform;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        void Update()
        {
            rectTransform.anchoredPosition = Input.mousePosition / canvasRectTransform.localScale.x;
            if (mouseScrollWheelInteractable)
                label.text = mouseScrollWheelInteractable.ValueRounded + "";
        }

        public void SetScrollWheelInteractable(MouseScrollWheelInteractable interactable)
        {
            mouseScrollWheelInteractable = interactable;
        }
    }
}