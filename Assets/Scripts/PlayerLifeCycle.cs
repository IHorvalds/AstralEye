using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLifeCycle : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Spikes"))
        {
            Die();
        }
        if (other.gameObject.CompareTag("LevelExit"))
        {
            // NextLevel(); // TODO
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("EnemyAttackBox")) {
            Die(); // deduct health points here
        }
    }

    private void Die() {
        anim.ResetTrigger("attack");
        anim.SetTrigger("dead");
        rb.bodyType = RigidbodyType2D.Static;
        SharedPlayerProperties.isDead = true;
    }

    private void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
