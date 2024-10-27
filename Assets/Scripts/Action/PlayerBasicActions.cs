using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Movement;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Action
{
    [RequireComponent(typeof(TouchingDirections))]
    internal class PlayerBasicActions : MonoBehaviour, PlayerContextActions
    {
        public Rigidbody rb { get; private set; }
        private TouchingDirections touchingDirections;
        private Transform orientation;

        private Vector3 movedirection;
        public Vector2 inputAxisXZ { get; private set; }
        public float moveSpeed { get; private set; }

        public float inputAxisY { get; private set; }
        public float jumpForce { get; private set; }
        public float jumpCooldown { get; private set; }
        public float airMultiplier { get; private set; }
        public bool readyToJump { get; private set; }

        public void Start()
        {
            rb = GetComponent<Rigidbody>();
            touchingDirections = gameObject.GetComponent<TouchingDirections>();
            orientation = gameObject.transform.GetChild(0).transform;

            inputAxisXZ = rb.velocity;
            moveSpeed = 7;

            inputAxisY = 0;
            jumpForce = 7;
            jumpCooldown = 0.25f;
            airMultiplier = 0.4f;
            readyToJump = true;
        }

        private void FixedUpdate()
        {
            HandleMove();
        }

        public void Move(InputValue value)
        {
            inputAxisXZ = new Vector2(value.Get<Vector2>().normalized.x, value.Get<Vector2>().normalized.y);
        }

        private void HandleMove()
        {
            if (touchingDirections.IsGrounded) rb.drag = 5;
            else rb.drag = 0;
            
            movedirection = orientation.forward * inputAxisXZ.y + orientation.right * inputAxisXZ.x;
            if (touchingDirections.IsGrounded) 
                rb.AddForce(movedirection.normalized * moveSpeed * 10f, ForceMode.Force);
            else
                rb.AddForce(movedirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);

            SpeedControl();
        }

        private void SpeedControl()
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            if(flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }

        public void Jump(InputValue value)
        {
            if (value.Get<float>() == 1 && readyToJump && touchingDirections.IsGrounded)
            {
                readyToJump = false;

                rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
                rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

                Invoke(nameof(ResetJump), jumpCooldown);
            }
        }

        private void ResetJump()
        {
            readyToJump = true;
        }

    }
}
