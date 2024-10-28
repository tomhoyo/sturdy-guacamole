using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Camera
{
    internal class PlayerCamera : MonoBehaviour
    {
        public float axisX;
        public float axisY;
        
        public float xRotation;
        public float yRotation;

        public Transform orientation;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * axisX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * axisY;

            yRotation += mouseX;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90, 90);

            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);

        }
    }
}
