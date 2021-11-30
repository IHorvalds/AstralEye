using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D m_rb;
    private Animator m_animator;
    private SpriteRenderer spriteRenderer;
    public float horizontalSpeed;
    public float verticalSpeed;
    private bool _previousDirection = true; // false left, true right
    private BoxCollider2D col;
    [SerializeField] private LayerMask groundLayer;


    private enum PlayerState {
        PlayerIdle, PlayerRunning, PlayerJump, PlayerLanding
    };

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_rb.bodyType == RigidbodyType2D.Dynamic) {

            float horizontal = Input.GetAxisRaw ("Horizontal");
            bool grounded = isGrounded();
            Vector2 _vel = new Vector2(horizontal * horizontalSpeed, m_rb.velocity.y);

            if (Input.GetButtonDown("Jump") && grounded) {
                _vel.y = verticalSpeed;
            }

            m_rb.velocity = _vel;

            SetAnimation(m_rb.velocity);

            spriteRenderer.flipX = !_previousDirection;
        }
    }

    private void SetAnimation(Vector2 velocity)
    {
        if (velocity.x > 0f) {
            m_animator.SetInteger("playerState", 1);
            _previousDirection = true;
        } else if (velocity.x < 0f) {
            m_animator.SetInteger("playerState", 1);
            _previousDirection = false;
        } else {
            m_animator.SetInteger("playerState", 0);
        }

        if (velocity.y < -0.1f) {
            m_animator.SetInteger("playerState", 3);
        } else if (velocity.y > 0.1f) {
            m_animator.SetInteger("playerState", 2);
        }
    }

    private bool isGrounded()
    {
        return Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0f, Vector2.down, 0.1f, groundLayer);
    }
}
