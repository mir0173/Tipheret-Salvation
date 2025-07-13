using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Moving : MonoBehaviour
{
    private Rigidbody2D rigid2D;
    private Vector2 moveDirection;
    private bool isJumping = false;
    private readonly float movePower = 2.5f;
    private readonly float jumpPower = 6;
    public PlayerInput playerInput;
    public Animator animator;
    public static bool isCanmove;
     public static bool isMove;

    void Awake()
    {
        playerInput.actions["Move"].performed += OnMove;
        playerInput.actions["Move"].canceled += OnMove;
        playerInput.actions["Jump"].performed += OnJump;
        playerInput.actions["Interaction"].performed += OnInteraction;
        rigid2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        transform.position = new Vector2(-7.5f, -2);
    }

    void FixedUpdate()
    {
        Move();
        if (moveDirection.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if(moveDirection.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (moveDirection.x == 0 || !isCanmove)
        {
            animator.SetBool("Move", false);
        }
        else
        {
            animator.SetBool("Move", true);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (isCanmove)
        {
            moveDirection = context.ReadValue<Vector2>();
        }
    }

    public void OnInteraction(InputAction.CallbackContext context)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (context.phase == InputActionPhase.Performed && B6SceneManager.distance <= 2 && GameManager.B6_num == 2 && currentScene.name == "B6Scene")
        {
            B6SceneManager.key5 = true;
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
        if (!isCanmove) moveDirection = Vector2.zero; 
        Vector2 velocity = moveDirection * movePower;
        rigid2D.linearVelocity = new Vector2(velocity.x, rigid2D.linearVelocity.y);
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
                if (GameManager.B6_num == 1) B6SceneManager.key = true;
                if (GameManager.B6_num == 2) B6SceneManager.key3 = true;
            }
        }
        if (other.gameObject.CompareTag("Enter"))
        {
            if (other.gameObject.name == "Entercheck" && !B6SceneManager.key2)
            {
                B6SceneManager.key2 = true;
            }
            if (other.gameObject.name == "Lightcheck" && !B6SceneManager.key4)
            {
                B6SceneManager.key4 = true;
            }
        }
    }

}
