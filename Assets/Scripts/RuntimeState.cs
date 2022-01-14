using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "RuntimeState", menuName = "RuntimeState/RuntimeState", order = 1)]
public class RuntimeState : ScriptableObject
{
    public int _bottledCollectedDuringLevel = 0;
    public int currentSaveFile = 0;
    public int goldenBottles = 0;
    public int lives = 0;
    public float currentHealth;
    public List<int> unlockedLevels;

    public string lastPlayed;
}
