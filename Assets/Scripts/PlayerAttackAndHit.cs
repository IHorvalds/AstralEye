using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackAndHit : MonoBehaviour
{
    // Start is called before the first frame update
    public RuntimeState runtimestate;
    private Animator anim;
    private Rigidbody2D m_rb;
    private bool startedAttack = false;
    private bool takingDamage = false;
    void Start()
    {
        anim = this.GetComponent<Animator>();
        m_rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && !SharedPlayerProperties.isDead && !startedAttack)
        {
            StartCoroutine(Attack());
        }
        if (Input.GetButton("TakeDamage") && !SharedPlayerProperties.isDead && !takingDamage)
        {
            Debug.Log("Taking damage");
            StartCoroutine(Damage());
        }
    }

    IEnumerator Attack()
    {
        startedAttack = true;
        anim.SetTrigger("attack");
        yield return new WaitForSeconds(1f); // can only hit once a second
        startedAttack = false;
    }

    IEnumerator Damage()
    {
        takingDamage = true;
        // TODO: actually take the damage. Decrese player health
        runtimestate.currentHealth -= 2;
        yield return new WaitForSeconds(1f); // can only get hit once a second
        takingDamage = false;
    }
}
