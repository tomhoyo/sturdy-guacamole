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
        public Vector2 inputAxisXZ { get; private set; }
        public float inputAxisY { get; private set; }
        public float moveSpeed { get; private set; }

        private TouchingDirections touchingDirections;
        private Transform orientation;

        private Vector3 movedirection;


        public void Start()
        {
            rb = GetComponent<Rigidbody>();
            inputAxisXZ = rb.velocity;
            inputAxisY = 0;
            moveSpeed = 7;

            touchingDirections = gameObject.GetComponent<TouchingDirections>();
            orientation = gameObject.transform.GetChild(0).transform;
            
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
            rb.AddForce(movedirection.normalized * moveSpeed * 10f, ForceMode.Force);

            SpeedControl();
        }

        private void SpeedControl()
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.y);

            if(flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }

        public void Jump(InputValue value)
        {
            
        }

    }
}
