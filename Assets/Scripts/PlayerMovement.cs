using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Image greenWheel;
    [SerializeField] Image redWheel;

    public float walkSpeed;
    public float sprintSpeed;
    public float groundDrag;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;
    public KeyCode jumpkey = KeyCode.Space;
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;
    public Transform orientation;
    bool sprinting;
    float stamina;
    bool staminaExhausted;
    public float maxStamina;
    float moveSpeed;

    Vector3 moveDirection;
    Vector2 inputDirection;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
        moveSpeed = walkSpeed;
        stamina = maxStamina;
    }




    public void OnMove(InputValue value)
    {
        inputDirection = value.Get<Vector2>();
        
    }

    public void OnJump(InputValue value)
    {
        if (readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(Resetjump), jumpCooldown);
        }
    }

    public void OnSprint(InputValue value)
    {
        if (sprinting)
        {
            moveSpeed = walkSpeed;
            sprinting = false;
        }
        else
        {
            moveSpeed = sprintSpeed;
            sprinting = true;
        }

        


    }

    private void MovePlayer()
    {
        if (sprinting && moveDirection != Vector3.zero && !staminaExhausted)
        {
            if (stamina > 0)
            {
                stamina -= 30 * Time.deltaTime;
            }
            else
            {
                greenWheel.enabled = false;
                staminaExhausted = true;
            }
            redWheel.fillAmount = (stamina / maxStamina + 0.07f);
        }
        else
        {
            if (stamina < maxStamina)
            {
                stamina += 30 * Time.deltaTime;
            }
            else
            {
                greenWheel.enabled = true;
                staminaExhausted = false;
                if (sprinting)
                {
                    moveSpeed = sprintSpeed;
                }
            }

            redWheel.fillAmount = (stamina / maxStamina);
        }

        if (staminaExhausted)
        {
            moveSpeed = walkSpeed;
        }

        moveDirection = orientation.forward * inputDirection.y + orientation.right * inputDirection.x;
        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }


        

        greenWheel.fillAmount = (stamina / maxStamina);

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        MovePlayer();
    }
    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        SpeedControl();
        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void Resetjump()
    {
        readyToJump = true;
    }
}
