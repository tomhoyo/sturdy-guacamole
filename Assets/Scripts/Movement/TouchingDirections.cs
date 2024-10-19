using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Movement
{
    internal class TouchingDirections : MonoBehaviour
    {

        private RaycastHit raycastHit;
        private float groundDistance = 1.1f;

        [SerializeField]
        private bool _isGrounded;
        public bool IsGrounded { get { return _isGrounded; } private set { _isGrounded = value; } }

        private void FixedUpdate()
        {

            Debug.DrawRay(transform.position, Vector3.down * groundDistance, Color.red);

            IsGrounded = Physics.Raycast(transform.position, Vector3.down, out raycastHit, groundDistance);
        }

    }
}
