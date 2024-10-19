 using UnityEngine;
 using UnityEngine.InputSystem;


[RequireComponent (typeof(Rigidbody), typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Vector2 AxisXZ;
    private float AxisY = 0;
    private float speed = 10;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        AxisXZ = rb.velocity;
    }

    public void OnMove(InputValue value)
    {
        AxisXZ = new Vector2(value.Get<Vector2>().normalized.x, value.Get<Vector2>().normalized.y);
    }

    public void OnJump(InputValue value)
    {
        AxisY = value.Get<float>();

    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(AxisXZ.x, AxisY, AxisXZ.y);
        rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
    }

}
