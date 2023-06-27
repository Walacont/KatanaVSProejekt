using System;
using UnityEngine;

namespace haw.unitytutorium
{
    public class MouseScrollWheelInteractable : Interactable
    {
        [SerializeField] private float minValue;
        [SerializeField] private float maxValue;

        [SerializeField] private float startValue;
        [SerializeField] private float targetValue;
        
        public float Value { get; private set; }
        public float ValueRounded => (float) Math.Round(Value, 1);

        private Animator animator;
        private AnimationClip animationClip;
        
        private bool scrollEnabled;
        public bool ValueCheckEnabled { get; set; }

        private void Awake()
        {
            animator = GetComponent<Animator>();
            animator.speed = 0; // Animation Speed is also set to 0 (in case this component is deactivated on Awake)
            animationClip = animator.GetCurrentAnimatorClipInfo(0)[0].clip;
        }

        private void Start()
        {
            AnimateToValue(startValue);
            SetTargetValue(targetValue);
        }

        private void OnEnable()
        {
            AnimateToValue(startValue);
        }

        private void Update()
        {
            if(!scrollEnabled)
                return;

            var scrollValue = Input.GetAxis("Mouse ScrollWheel");
            Value += scrollValue;

            AnimateToValue(Value);
            CheckTargetValue(scrollValue);
        }

        private void AnimateToValue(float value)
        {
            Value = Mathf.Clamp(value, minValue, maxValue);
            
            var normalizedTime = Util.Map(Value, minValue, maxValue, 0, animationClip.length);

            if (normalizedTime >= 1.0f)
                normalizedTime = 0.99f;

            animator.Play(animationClip.name, 0, normalizedTime);
        }

        private void CheckTargetValue(float scrollValue)
        {
            if (scrollValue > 0 || scrollValue < 0)
            {
                // if the value check is not enabled the user should not use this interactor (move the scroll wheel while hovering)
                // Therefore we notify the interaction listener which will then trigger the display of an error
                if (!ValueCheckEnabled)
                {
                    Notify();
                    scrollEnabled = false;
                    return;
                }
                
                if (Math.Abs(targetValue - Value) < 0.01f)
                {
                    Notify();
                }
            }
        }
        
        private void OnMouseEnter() => scrollEnabled = true;

        private void OnMouseExit() => scrollEnabled = false;

        public void SetTargetValue(float value)
        {
            targetValue = Mathf.Clamp(value, minValue, maxValue);
            targetValue = (float) Math.Round(targetValue, 1);
        }
    }
}