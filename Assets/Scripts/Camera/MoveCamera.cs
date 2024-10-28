using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Camera
{
    internal class MoveCamera : MonoBehaviour
    {
        public Transform cameraPosition;

        private void Update()
        {
            transform.position = cameraPosition.position;
        }
    }

    
}
