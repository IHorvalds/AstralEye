using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottleCollector : MonoBehaviour
{
    // private int collected = 0;
    [SerializeField] private Text collectiblesDisplay;
    [SerializeField] private Text healthDisplay;
    [SerializeField] private Text livesDisplay;
    public RuntimeState runtimeState;

    private void Start() {
        collectiblesDisplay.text = "Collectibles: " + runtimeState.goldenBottles;
        healthDisplay.text = "Health: " + runtimeState.currentHealth;
        livesDisplay.text = "Lives: " + runtimeState.lives;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("CollectibleBottle"))
        {
            Destroy(other.gameObject);
            runtimeState._bottledCollectedDuringLevel++;
        }
    }

    void Update()
    {
        collectiblesDisplay.text = "Collectibles: " + (runtimeState.goldenBottles + runtimeState._bottledCollectedDuringLevel);
        healthDisplay.text = "Health: " + runtimeState.currentHealth;
        livesDisplay.text = "Lives: " + runtimeState.lives;
    }
}
