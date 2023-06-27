using UnityEngine;
using UnityEngine.Events;

namespace haw.unitytutorium
{
    [System.Serializable]
    public class MouseOverEvent : UnityEvent<GameObject> {}
    
    public class MouseSelectionController : MonoBehaviour
    {
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private MouseOverEvent OnMouseEnter;
        [SerializeField] private MouseOverEvent OnMouseExit;

        private GameObject mouseOverGameObject;
        private Camera cam;

        private void Awake() => cam = Camera.main;

        private void Update()
        {
            UpdateMouseOverInteractable();
        }

        private void UpdateMouseOverInteractable()
        {
            var ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit, 100, layerMask))
            {
                Debug.DrawLine(ray.origin, hit.point, Color.green);

                if (!mouseOverGameObject)
                {
                    // CASE 1: Mouse Pointer goes from No GameObject to a Selectable GameObject.
                    // Mouse enters 'Selectable' GameObject and NO previous GameObject was hovered (mouseOverInteractable == null)
                    // We set the current mouseOverInteractable to the GameObjects that was hit by the raycast
                    MouseEnter(hit.collider.gameObject);
                }
                else if (!mouseOverGameObject.Equals(hit.transform.gameObject))
                {
                    // CASE 2: Mouse Pointer goes from a Selectable GameObject to a different Selectable GameObject.
                    // Mouse enters 'Selectable' GameObject and a previous Selectable GameObject was hovered (mouseOverInteractable != null)
                    // We set the previous mouseOverInteractable to null, so in the next frame (update call) the script will go in the if clause above
                    // and set the new Selectable GameObject as the current mouseOverInteractable.
                    MouseExit();
                }
            }
            else if (mouseOverGameObject)
            {
                // CASE 1: Mouse Pointer goes from a Selectable GameObject to No GameObject.
                // If our Ray hits "nothing" but we've hovered a Selectable GamObject in the previous frame this means that we just exited the GameObject with our mouse cursor.
                // In this case we set the previous mouseOverInteractable to null.
                MouseExit();
            }
            else
            {
                Debug.DrawLine(ray.origin, ray.direction * 20, Color.red);
            }
        }

        private void MouseEnter(GameObject moGameObject)
        {
            OnMouseEnter?.Invoke(mouseOverGameObject = moGameObject);
        }

        private void MouseExit()
        {
            OnMouseExit?.Invoke(mouseOverGameObject);
            mouseOverGameObject = null;
        }
    }
}