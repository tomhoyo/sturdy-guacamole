using Assets.Scripts.Action;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent (typeof(Rigidbody), typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    private PlayerContextActions actions;

    private void Start()
    {
        actions = gameObject.AddComponent<PlayerBasicActions>();
    }
    
    public void OnMove(InputValue value)
    {
       actions.Move(value);
    }

    public void OnJump(InputValue value)
    {
        actions.Jump(value);
    }



}
