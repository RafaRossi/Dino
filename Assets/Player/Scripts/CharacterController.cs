using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController : MonoBehaviour
{
	private GameManager gameManager;

	[Header("Player Components")]
	[SerializeField] private Animator animator = default;
	[SerializeField] private Rigidbody2D rb = default;
	[SerializeField] private PlayerInput input = default;

	[Header("Movement")]
	[SerializeField] private float movementSpeed = 10f;
	[Range(0, .3f)] [SerializeField] private float smoothMovement = .05f;
	private float facingDirection = 1;
	private float direction = 0;
	private Vector3 _velocity = Vector3.zero;

	[Header("Jump")]
	[SerializeField] private float jumpForce = 400f;
	[SerializeField] private int maxJumps = 1;
	[SerializeField] private Sound jumpSounds = null;
	private int jumpCounter = 0;
	private float gravityMultiplier = 3f;

	[Header("Ground Checker")]
	[SerializeField] private LayerMask groundLayer = default;
	[SerializeField] private Transform groundChecker = default;
	const float GROUND_CHECK_RADIUS = .3f;
	private bool _isGrounded = true;
	public bool IsGrounded
    {
		get => _isGrounded;
        set
        {
			if(_isGrounded != value)
            {
				_isGrounded = value;
				jumpCounter = _isGrounded == true ? 0 : jumpCounter;

				animator.SetBool("isGrounded", _isGrounded);
            }
        }
    }

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		gameManager = GameManager.Instance;
	}

    private void OnEnable()
    {
		GameManager.Instance.onLevelEnd += EndLevel;
    }

    private void FixedUpdate()
	{
		IsGrounded = Physics2D.OverlapCircle(groundChecker.position, GROUND_CHECK_RADIUS, groundLayer);

		Move();
	}

	public void SetMoveDirection(InputAction.CallbackContext value)
    {
		SetDirection(value.ReadValue<float>());
    }

	public void SetDirection(float value)
    {
		direction = value;
	}

	public void Move()
	{
		Vector3 targetVelocity = new Vector2(direction * movementSpeed * Time.fixedDeltaTime, rb.velocity.y);
		rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref _velocity, smoothMovement);

		if (direction != 0)
		{
			transform.localScale = new Vector3(facingDirection * (direction / Mathf.Abs(direction)), transform.localScale.y, transform.localScale.z);
		}

		if (rb.velocity.y < 0)
		{
			rb.velocity += Vector2.up * Physics2D.gravity.y * (gravityMultiplier - 1) * Time.fixedDeltaTime;
		}

		animator.SetFloat("Speed", Mathf.Abs(direction));
	}

	public void Jump(InputAction.CallbackContext value)
    {
		if(value.started)
        {
			Jump();
        }
    }

	public void Jump()
    {
		if ((IsGrounded || jumpCounter < maxJumps))
		{
			rb.velocity = Vector2.zero;

			IsGrounded = false;

			jumpCounter++;
			rb.AddForce(new Vector2(0f, jumpForce / (jumpCounter * 1.2f)), ForceMode2D.Impulse);

			animator.SetTrigger("Jump");

			AudioManager.Instance.PlayFX(jumpSounds);
		}
	}

	public void Die()
    {
		animator.SetTrigger("Die");
		enabled = false;

		rb.velocity = Vector2.zero;

		gameManager.PlayerDie();
    }

	public void Respawn()
    {
		transform.SetPositionAndRotation(gameManager.GetLastCheckPointPosition(), Quaternion.identity);
		gameManager.isDead = false;

		animator.SetTrigger("Revive");
	}

	private void EndLevel()
    {
		input.enabled = false;
	}
}
