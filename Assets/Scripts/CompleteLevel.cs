using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompleteLevel : MonoBehaviour
{
    private AudioSource finishSound;
    private bool enteredExitArea = false;
    
    void Start()
    {
        finishSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("some random collision");
        if (other.gameObject.name == "Player")
        {
            enteredExitArea = true;
            Debug.Log("hit player and is pointing ip");
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.name == "Player")
        {
            enteredExitArea = false;
            Debug.Log("unhit player and is pointing ip");
        }
    }

    private void Update() {
        if (enteredExitArea && Input.GetAxisRaw("Vertical") > 0.1f)
        {
            finishSound.Play();
            LevelCompleted();
        }
    }

    private void LevelCompleted()
    {
        if (SceneManager.GetActiveScene().name == "Level 4") {
            SceneManager.LoadScene(6);
        } else {
            SceneManager.LoadScene(1);
        }
    }
}
