using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Tools
{
    internal class InputManager : MonoBehaviour
    {
        private PlayerControls playerControls;
        public Vector2 movementInput { get; private set; } = Vector2.zero;
        public float jumpInput { get; private set; } = 0;
        public UnityEvent EventOnJump;


        private void OnEnable()
        {
            playerControls = new PlayerControls();

            playerControls.Player.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.Player.Jump.performed += i =>
            {
                jumpInput = i.ReadValue<float>();
                OnJump();
            };


            playerControls.Enable();
        }

        private void OnDisable()
        {
           playerControls.Disable();
        }

        private void OnJump()
        {
            EventOnJump?.Invoke();
        }



    }
}
