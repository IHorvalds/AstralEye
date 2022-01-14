using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    public GameObject player;

    private Animator anim;
    // This should probably be an interface of Attack
    // An enemy class should also be derived from waypoint follower
    // instead of having everything here
    private GameObject attackBox;
    private BoxCollider2D attackBoxCollider;
    private int attackDirection = 0;

    [SerializeField] private float attackRangeTrigger = 3;
    [SerializeField] private float attackWindupFrames = 10; 
    private int attackFrame = 0;
    private bool isAttacking = false;

    private enum EnemyState{
        SkeletonAttack, SkeletonWalk, SkeletonDeath
    }

    // private EnemyState state = EnemyState.SkeletonWalk;

    private SpriteRenderer spriteRenderer;

    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;


    [SerializeField] private float speed = 2f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = true;
        anim = GetComponent<Animator>();
        attackBox = transform.Find("EnemyAttackBox").gameObject;
        attackBoxCollider = attackBox.GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        attackDirection = spriteRenderer.flipX ? -1 : 1;
        float distanceToPlayer = player.transform.position.x - transform.position.x;
        attackBox.transform.position = transform.position; // reset attackbox
        if(!isAttacking && distanceToPlayer * attackDirection > 0 && distanceToPlayer * attackDirection < attackRangeTrigger)
        {
            StartCoroutine(Attack());

        } else
        {
            if (!isAttacking)
            {
                Move();
            }
        }
    }

    private IEnumerator Attack()
    {
        // Attack shouldn't happen instantly;

        isAttacking = true;
        anim.SetInteger("state", 1);
        yield return new WaitForSeconds(1.0f);
        attackBox.transform.position += new Vector3(attackDirection * attackRangeTrigger, 0);
        isAttacking = false;
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

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("PlayerAttackBox"))
        {
            Die();
        }
    }

    private void Die() {
        // die should wait for animation TODO
        anim.SetInteger("state", 2);
        gameObject.SetActive(false);
    }
}
