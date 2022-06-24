using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UnityThirdPerson
{
    public class InputReader : MonoBehaviour, Controls.IPlayerActions
    {
        public Vector2 MovementValue { get; private set; }
        public event Action JumpEvent, DodgeEvent;
        private Controls controls;

        private void Start()
        {
            controls = new Controls();
            // Reference to class, will call methods
            controls.Player.SetCallbacks(this);

            controls.Player.Enable();
        }

        private void OnDestroy()
        {
            controls.Player.Disable();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            // If we pressed button
            if (context.performed)
            {
                JumpEvent?.Invoke();
            }
        }

        public void OnDodge(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                DodgeEvent?.Invoke();
            }
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            // If we press any move key, we store value 
            MovementValue = context.ReadValue<Vector2>();
        }

        public void OnLook(InputAction.CallbackContext context) { }
    }
}