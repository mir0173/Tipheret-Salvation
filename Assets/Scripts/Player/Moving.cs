using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Moving : MonoBehaviour
{
    private Rigidbody2D rigid2D;
    private Vector2 moveDirection;
    private bool isJumping = false;
    private readonly float movePower = 2;
    private readonly float jumpPower = 6;
    public PlayerInput playerInput;
    public static bool isCanmove;

    void Awake()
    {
        transform.position = new Vector2(-7.5f, -2);
        playerInput.actions["Move"].performed += OnMove;
        playerInput.actions["Move"].canceled += OnMove;
        playerInput.actions["Jump"].performed += OnJump;
        rigid2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Move();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (isCanmove)
        {
            moveDirection = context.ReadValue<Vector2>();
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && !isJumping && isCanmove)
        {
            isJumping = true;
            rigid2D.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }

    private void Move()
    {
        if (isCanmove)
        {
            Vector2 velocity = moveDirection * movePower;
            rigid2D.linearVelocity = new Vector2(velocity.x, rigid2D.linearVelocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Plane"))
        {
            isJumping = false;
        }
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Door"))
        {
            Scene currentScene = SceneManager.GetActiveScene();
            if (currentScene.name == "B6Scene")
            {
                transform.position = new Vector2(-10f, -2.8f);
                GameManager.B6_num += 1;
                B6SceneManager.key = true;
            }
        }
    }

}
