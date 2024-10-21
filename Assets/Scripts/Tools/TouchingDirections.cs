using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Movement
{
    internal class TouchingDirections : MonoBehaviour
    {
        [SerializeField]
        private LayerMask fundation;
        private float groundDistance = 0.1f;

        [SerializeField]
        private List<Transform> GroundsRay;

        [SerializeField]
        private bool _isGrounded;
        public bool IsGrounded { get { return _isGrounded; } private set { _isGrounded = value; } }

        private void FixedUpdate()
        {
            if (Time.frameCount % 4 == 0)
            {

                IsGrounded = false;
                foreach (Transform groundRay in GroundsRay)
                {
                    Debug.DrawRay(groundRay.position, Vector3.down * groundDistance, Color.red);

                    if (Physics.Raycast(groundRay.position, Vector3.down, groundDistance, fundation)) IsGrounded = true;
                }
            }
            

            
        }

    }
}
