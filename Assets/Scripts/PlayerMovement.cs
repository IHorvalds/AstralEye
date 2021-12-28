using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SharedPlayerProperties {
    public static bool isInSecondForm = false;
}

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D m_rb;
    private Animator m_animator;
    private SpriteRenderer spriteRenderer;
    public float horizontalSpeed;
    public float verticalSpeed;
    private bool _previousDirection = true; // false left, true right
    private BoxCollider2D col;

    // private bool isInSecondForm = false;
    [SerializeField] private LayerMask groundLayer;

    public CameraController cc;


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
        bool grounded = isGrounded();
        if (m_rb.bodyType == RigidbodyType2D.Dynamic && !SharedPlayerProperties.isInSecondForm) {

            cc.playerTransform = this.transform;

            float horizontal = Input.GetAxisRaw ("Horizontal");
            Vector2 _vel = new Vector2(horizontal * horizontalSpeed, m_rb.velocity.y);

            // Jump
            if (Input.GetButtonDown("Jump") && grounded) {
                _vel.y = verticalSpeed;
            }

            m_rb.velocity = _vel;


            spriteRenderer.flipX = !_previousDirection;


        }
        // Switch form
        if (Input.GetButtonDown("SwitchForm") && grounded) {
            SharedPlayerProperties.isInSecondForm = !SharedPlayerProperties.isInSecondForm;
            m_rb.bodyType = SharedPlayerProperties.isInSecondForm ? RigidbodyType2D.Static : RigidbodyType2D.Dynamic;
        }
        SetAnimation(m_rb.velocity);
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
