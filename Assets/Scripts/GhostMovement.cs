using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    private Rigidbody2D m_rb;
    private Animator m_animator;
    private SpriteRenderer spriteRenderer;
    public float horizontalSpeed;
    public float verticalSpeed;
    private BoxCollider2D col;
    private bool _previousDirection = true;

    public LineRenderer ghostLink;

    public CameraController cc;

    public GameObject player;

    private float _time = 0f;
    private bool _blink = true;

    public float first_threshold;
    public float second_threshold;
    public float time_threshold;
    public float blink_time;


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
        Vector2 _vel = new Vector2(0.0f, 0.0f);
        if (SharedPlayerProperties.isInSecondForm) {
            cc.playerTransform = this.transform;

            
            float horizontal = Input.GetAxisRaw ("Horizontal");
            _vel.x = horizontal * horizontalSpeed;

            // Jump
            if (Input.GetAxisRaw("Vertical") > 0) {
                _vel.y = verticalSpeed;
            }
            else if (Input.GetAxisRaw("Vertical") < 0) {
                _vel.y = -verticalSpeed;
            } else {
                _vel.y += Mathf.Sin(Time.realtimeSinceStartup * Mathf.PI) / 10;
            }



            spriteRenderer.flipX = !_previousDirection;
            ghostLink.positionCount = 2;
            ghostLink.SetPosition(0, player.GetComponent<Collider2D>().bounds.center);
            ghostLink.SetPosition(1, col.bounds.center);

        } else {
            // Switch form
            this.transform.position = player.GetComponent<Collider2D>().bounds.center + new Vector3(5.0f, 0.0f, 0.0f);
        }
        m_rb.velocity = _vel;


        SetAnimation(m_rb.velocity);

        
        _time += Time.deltaTime;

        if (getDistanceToPlayer() > first_threshold && _time > time_threshold) {
            _blink = false;

        }

        if (_time > time_threshold + blink_time) {
            _time = 0f;
            _blink = true;
        }

        ghostLink.gameObject.SetActive(SharedPlayerProperties.isInSecondForm);
        col.enabled = SharedPlayerProperties.isInSecondForm;

        m_animator.enabled = SharedPlayerProperties.isInSecondForm && _blink;
        spriteRenderer.enabled = SharedPlayerProperties.isInSecondForm && _blink;
        ghostLink.GetComponent<Renderer>().enabled = _blink;

        if (getDistanceToPlayer() > second_threshold) {
            _blink = true;
            _time = 0f;
            SharedPlayerProperties.isInSecondForm = false;
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

    private float getDistanceToPlayer()
    {
        return Mathf.Abs(Vector3.Distance(this.transform.position, player.GetComponent<Collider2D>().bounds.center));
    }
}
