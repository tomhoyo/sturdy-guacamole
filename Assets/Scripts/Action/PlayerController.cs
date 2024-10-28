using Assets.Scripts.Action;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerContextActions actions;

    private void Start()
    {
        actions = gameObject.AddComponent<PlayerBasicActions>();
    }
    

}
