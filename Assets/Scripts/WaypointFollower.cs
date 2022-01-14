using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    public GameObject player;

    private Animator anim;

    private enum EnemyState{
        SkeletonAttack, SkeletonWalk, SkeletonDeath
    }

    // private EnemyState state = EnemyState.SkeletonWalk;

    private SpriteRenderer spriteRenderer;

    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;

    private bool currentDirection = false;

    [SerializeField] private float speed = 2f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = true;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(Vector2.Distance(player.transform.position,transform.position) < 5)
        {
            Attack();
        }
        else
        {
            Move();
        }
    }

    private void Attack()
    {
        anim.SetInteger("state", 1);
    }

    private void Move()
    {
        anim.SetInteger("state", 0);

        if(Vector2.Distance(waypoints[currentWaypointIndex].transform.position,transform.position) < .1f)
        {
            currentWaypointIndex++;

            if(currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }

            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);

    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Spikes"))
        {
            Die();
        }
    }

    private void Die() {
        anim.SetInteger("state", 2);
        anim.ResetTrigger("attack");
        anim.SetTrigger("dead");
        rb.bodyType = RigidbodyType2D.Static;
        SharedPlayerProperties.isDead = true;
    }
}
