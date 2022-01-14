using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameState : MonoBehaviour
{
    public Text someDisplay;
    public RuntimeState runtimeState;
    void Start()
    {
        someDisplay.text = "Collectibles: " + runtimeState.goldenBottles;
    }
}
