using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Action
{
    internal interface PlayerContextActions
    {

        public void Move(InputValue value);
        public void Jump(InputValue value);

        





    }
}
