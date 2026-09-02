using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Logic : MonoBehaviour
{
    [SerializeField] private int moveSpeed;
    [SerializeField] private int jumpStrength;
    Rigidbody rb;
    Vector3 moveInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        /*Old Input System
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        moveInput.x = h * moveSpeed;
        moveInput.y = v * moveSpeed;
        */
    }

    void FixedUpdate()
    {
        rb.AddForce(new Vector3 (moveInput.x, 0, moveInput.y) * moveSpeed, ForceMode.Acceleration);
    }

    //New Input System (Invoke Unity Events)
    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
    }

    /*New Input System (Send Messages)
    public void OnJump()
    {
        rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
    }*/
    public void OnMove(InputAction.CallbackContext ctx)
    {
        Vector2 v = ctx.ReadValue<Vector2>();
        moveInput = v; 
    }
}
