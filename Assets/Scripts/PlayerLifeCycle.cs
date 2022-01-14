using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLifeCycle : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    public RuntimeState runtimestate;
    private bool shouldRestart = false;
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        if (runtimestate.currentHealth == 0)
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Spikes") && !SharedPlayerProperties.isDead) // massive race condition. idc
        {
            Die();
        }
        if (other.gameObject.CompareTag("LevelExit"))
        {
            // NextLevel(); // TODO
        }
    }

    private void Die() {
        runtimestate._bottledCollectedDuringLevel = 0;
        runtimestate.lives -= 1;
        anim.ResetTrigger("attack");
        anim.SetTrigger("dead");
        rb.bodyType = RigidbodyType2D.Static;
        SharedPlayerProperties.isDead = true;
        runtimestate.currentHealth = 100;

        if (runtimestate.lives <= 0)
        {
            runtimestate.unlockedLevels.Clear();
            runtimestate.unlockedLevels.Add(0);
            runtimestate.lives = 10;
            runtimestate.goldenBottles = 0;
            shouldRestart = true;
        }

        SaveFileLoader sfl = new SaveFileLoader();
        sfl.runtimestate = runtimestate;
        sfl.SaveFile();
    }

    private void ResetGame()
    {
        if (shouldRestart) {
            SceneManager.LoadScene(1);
        } else {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
