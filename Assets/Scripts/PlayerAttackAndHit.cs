using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackAndHit : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;
    private Rigidbody2D m_rb;
    private SpriteRenderer spriteRenderer;
    private bool startedAttack = false;
    private bool takingDamage = false;
    private GameObject attackBox;
    private BoxCollider2D attackBoxCollider;
    private int attackDirection = 0;

    void Start()
    {
        anim = this.GetComponent<Animator>();
        m_rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        attackBox = transform.Find("PlayerAttackBox").gameObject;
        attackBoxCollider = attackBox.GetComponent<BoxCollider2D>();
        //attackBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // reset attackBox
        //attackBox.SetActive(false);
        attackBox.transform.position = transform.position;
        if (Input.GetButton("Fire1") && !SharedPlayerProperties.isDead && !startedAttack)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        startedAttack = true;
        anim.SetTrigger("attack");
  
        attackDirection = spriteRenderer.flipX ? -1 : 1;
        attackBox.transform.position +=  new Vector3(attackDirection * 0.7f, 0);
        //attackBox.SetActive(true);

        yield return new WaitForSeconds(1f); // can only hit once a second
        startedAttack = false;
    }

    IEnumerator Damage()
    {
        takingDamage = true;
        // TODO: actually take the damage. Decrese player health
        yield return new WaitForSeconds(1f); // can only get hit once a second
        takingDamage = false;
    }
}
