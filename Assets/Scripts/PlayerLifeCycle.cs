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

    private void Die() {
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("dead");
    }

    private void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
