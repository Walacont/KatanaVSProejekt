using System;
using UnityEngine;

namespace haw.unitytutorium.s23
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] private Rotator rotator;
        [SerializeField] private Resizer resizer;

        [SerializeField] private KeyCode rotateKey;
        [SerializeField] private KeyCode resizeKey;
        
        private Mover mover;
        private PhysicsMover physicsMover;

        private void Awake()
        {
            // This is an example of getting a reference via GetComponent<T>()
            // It only works if the Mover Component is on the same GameObject as the InputHandler
            // otherwise mover will be 'null' 
            mover = GetComponent<Mover>();
            physicsMover = GetComponent<PhysicsMover>();
        }

        private void Update()
        {
            if (Input.GetKey(rotateKey))
            {
                rotator.Rotate();
            }

            if (Input.GetKeyDown(resizeKey))
            {
                resizer.ToggleResize();
            }
            
            // Null-Check to ensure the mover-reference exists
            if(mover)
                mover.MoveWithExternalInput(Input.GetAxis("Horizontal"));
        }

        private void FixedUpdate()
        {
            if(physicsMover)
                physicsMover.MoveWithExternalInput(Input.GetAxis("Horizontal"));
        }
    }
}