using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottleCollector : MonoBehaviour
{
    private int collected = 0;
    [SerializeField] private Text someDisplay;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("CollectibleBottle"))
        {
            Destroy(other.gameObject);
            collected++;

            someDisplay.text = "Collectibles: " + collected;
        }
    }
}
