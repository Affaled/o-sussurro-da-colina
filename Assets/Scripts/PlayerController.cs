using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;

    private PlayerControls playerControls;
    private Vector2 movement;
    private Vector2 lastMovementDirection = Vector2.down;
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;

    private void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Update()
    {
        PlayerInput();
        UpdateAnimatorParameters();
    }

    private void FixedUpdate()
    {
        AdjustPlayerFacingDirection();
        Move();
    }

    private void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();

        if (movement != Vector2.zero)
        {
            lastMovementDirection = movement.normalized;
        }
    }

    private void UpdateAnimatorParameters()
    {
        Vector2 animDirection = movement != Vector2.zero ? movement.normalized : lastMovementDirection;

        myAnimator.SetFloat("moveX", animDirection.x);
        myAnimator.SetFloat("moveY", animDirection.y);
        myAnimator.SetBool("isMoving", movement != Vector2.zero);
    }

    private void Move()
    {
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void AdjustPlayerFacingDirection()
    {
        if (movement.x > 0)
        {
            mySpriteRenderer.flipX = false;
        } else if (movement.x < 0)
        {
            mySpriteRenderer.flipX = true;
        }
    }
}
