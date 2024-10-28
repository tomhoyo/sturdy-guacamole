using System;
using Assets.Scripts.Movement;
using Assets.Scripts.Tools;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Assets.Scripts.Action
{
    [RequireComponent(typeof(Rigidbody), typeof(TouchingDirections), typeof(InputManager))]
    internal class PlayerBasicActions : MonoBehaviour, PlayerContextActions
    {
        private InputManager inputManager;

        public Rigidbody rb { get; private set; }
        private TouchingDirections td;
        private Transform orientation;

        private Vector3 moveDirection;

       
        [Header("Movement")]
        public float moveSpeed { get; private set; } = 7.0f;

        [Header("Jump")]
        public float jumpForce { get; private set; } = 20;
        public bool endedJumpEarly { get; private set; } = false;
        public bool jumpToConsume { get; private set; } = false;
        private const float jumpDeltaTime = 50;

        [Header("Gravity")]
        private const float fallDeltaTime = 50;
        private const float freeMaxFallSpeed = 20;
        private const float groundingForce = 0;

        public void Start()
        {
            inputManager = GetComponent<InputManager>();
            inputManager.EventOnJump.AddListener(Onjump);

            rb = GetComponent<Rigidbody>();
            td = gameObject.GetComponent<TouchingDirections>();
            orientation = gameObject.transform.GetChild(0).transform;
        }

        private void FixedUpdate()
        {
            HandleMovement();
            HandleJump();
            HandleGravity();

            ApplyMovement();
        }

        private void HandleMovement()
        {
            float yDirection = moveDirection.y;
            moveDirection = orientation.forward * inputManager.movementInput.y;
            moveDirection += orientation.right * inputManager.movementInput.x;
            moveDirection.Normalize();
            moveDirection *= moveSpeed;

            moveDirection.y = yDirection;
        }
       
        private void Onjump()
        {
            jumpToConsume = true;
        }

        private void HandleJump()
        {
            if (!endedJumpEarly && !td.IsGrounded && inputManager.jumpInput != 1 && rb.velocity.y > 0) endedJumpEarly = true;
                
            if (td.IsGrounded && jumpToConsume) Jump(jumpForce);

            jumpToConsume = false;
        }

        private void Jump(float force)
        {
            moveDirection.y = force * inputManager.jumpInput;

            endedJumpEarly = false;
        }
       

        private void HandleGravity()
        {
            if (td.IsGrounded && moveDirection.y <= 0f) moveDirection.y = groundingForce;
            
            else if (IsJumping()) moveDirection.y = Mathf.MoveTowards(moveDirection.y, 0, jumpDeltaTime * Time.fixedDeltaTime);
            
            else moveDirection.y = Mathf.MoveTowards(moveDirection.y, -freeMaxFallSpeed, fallDeltaTime * Time.fixedDeltaTime);
        }

        private bool IsJumping()
        {
            return (moveDirection.y > 0f && !endedJumpEarly);
        }
        
        private void ApplyMovement() => rb.velocity = moveDirection;

    }
}
