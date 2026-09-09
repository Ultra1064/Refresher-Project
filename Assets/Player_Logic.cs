using System.Collections; //For IEnum
using UnityEngine;
using UnityEngine.InputSystem;
//Note to self: Physics.gravity messes with gravity! It's a vector3 you can mess with in the Physics settings
public class Player_Logic : MonoBehaviour
{
    [SerializeField] private int moveSpeed;
    [SerializeField] private int jumpStrength;

    [SerializeField] LayerMask groundLayer; //For Raycasting
    [SerializeField] Animator anim; //For the animations
    Rigidbody rb;
    Vector3 moveInput;

    [SerializeField] bool inLava = false;

    Coroutine currentLavaCoroutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("moveSpeed", Mathf.Abs(moveInput.magnitude));
        /*Old Input System
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        moveInput.x = h * moveSpeed;
        moveInput.y = v * moveSpeed;
        */
    }

    void FixedUpdate()
    {
        anim.SetBool("grounded", Physics.Raycast(transform.position, Vector3.down, 1, groundLayer));

        Vector3 correctedInput = GetCameraBasedInput(moveInput, Camera.main);

        rb.AddForce(correctedInput * moveSpeed, ForceMode.Acceleration);

        anim.transform.forward = correctedInput;
    }

    //New Input System (Invoke Unity Events)
    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);

        anim.SetTrigger("jump");

        StartCoroutine(HandleJumpReset());
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
    public Vector3 GetCameraBasedInput(Vector2 input, Camera camera)
    {
        Vector3 camRight = camera.transform.right;
        camRight.y = 0;
        camRight.Normalize();

        Vector3 camForward = camera.transform.forward;
        camForward.y = 0;
        camForward.Normalize();

        return (input.x * camRight) + (input.y * camForward);
    }
    IEnumerator HandleJumpReset()
    {
        yield return new WaitForSeconds(0.5f);
        anim.ResetTrigger("jump");
    }

    [ContextMenu("Touch Lava")]
    public void TouchLava()
    {
        if (currentLavaCoroutine != null)
            return;

        inLava = true;
        GetComponent<PlayerHealth>().TakeDamage(10);
        currentLavaCoroutine = StartCoroutine(HandleDamageOverTime());
    }

    IEnumerator HandleDamageOverTime()
    {
        yield return new WaitForSeconds(2);

        if (inLava)
        {
            GetComponent<PlayerHealth>().TakeDamage(10);
            StartCoroutine(HandleDamageOverTime());
        }
    }

    [ContextMenu("Leave Lava")]
    public void LeaveLava()
    {
        inLava = false;
        StopCoroutine(currentLavaCoroutine);
        currentLavaCoroutine = null;
    }
}
