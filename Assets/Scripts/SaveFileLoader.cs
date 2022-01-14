using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[Serializable]
public class SaveData
{
    public int goldenBottles;
    public int lives;
    public float currentHealth;
    public List<int> unlockedLevels;

    public string lastPlayed;
}

public class SaveFileLoader : MonoBehaviour
{
    public int saveFile;
    public RuntimeState runtimestate;
    public Button button;

    private void Start()
    {
        if (button != null && this.saveFile != -1)
        {

            string filename = Application.persistentDataPath + "/AstralEye-" + this.saveFile.ToString() + ".json";
            string buttonText = "File " + (this.saveFile + 1);

            Debug.Log("Loading " + filename);

            if (File.Exists(filename)) 
            {
                string jsonSaveData = System.IO.File.ReadAllText(filename);
                SaveData data = JsonUtility.FromJson<SaveData>(jsonSaveData);

                buttonText = buttonText + " \n " + data.lastPlayed;
            }

            button.GetComponentInChildren<Text>().text = buttonText;
        }
    }

    public void LoadFile()
    {
        if (this.saveFile != -1) {
            PlayerPrefs.SetInt("CurrentSaveFile", this.saveFile);
        } else {
            this.saveFile = PlayerPrefs.GetInt("CurrentSaveFile", 0);
        }

        runtimestate.currentSaveFile = this.saveFile;
        string filename = Application.persistentDataPath + "/AstralEye-" + runtimestate.currentSaveFile.ToString() + ".json";
        Debug.Log(filename);

        int shouldDeleteFile = PlayerPrefs.GetInt("ShouldDeleteSelectedFile", 0);

        if (shouldDeleteFile != 0)
        {
            File.Delete(filename);
            Debug.Log("Was supposed to delete " + filename);
            Debug.Log(File.Exists(filename));
            PlayerPrefs.SetInt("ShouldDeleteSelectedFile", 0);
        }

        if (File.Exists(filename)) 
        {
            string jsonSaveData = System.IO.File.ReadAllText(filename);
            SaveData data = JsonUtility.FromJson<SaveData>(jsonSaveData);

            runtimestate.currentSaveFile = saveFile;
            runtimestate.goldenBottles = data.goldenBottles;
            runtimestate.lives = data.lives;
            runtimestate.currentHealth = data.currentHealth;
            runtimestate.unlockedLevels = data.unlockedLevels;

            Debug.Log("Game data loaded!");

        }
	    else {
		    Debug.LogError("There is no save data!");
            // default values
            runtimestate.currentHealth = 100;
            runtimestate.goldenBottles = 0;
            runtimestate.lives = 10;
            runtimestate.unlockedLevels.Clear();
            runtimestate.unlockedLevels.Add(0);
            SaveFile();
        }
    }

    public void SaveFile()
    {
        this.saveFile = PlayerPrefs.GetInt("CurrentSaveFile", 0);
        runtimestate.currentSaveFile = this.saveFile;
        string filename = Application.persistentDataPath + "/AstralEye-" + runtimestate.currentSaveFile.ToString() + ".json";

        SaveData data = new SaveData();

        data.goldenBottles = runtimestate.goldenBottles;
        data.lives = runtimestate.lives;
        data.currentHealth = runtimestate.currentHealth;
        data.unlockedLevels = runtimestate.unlockedLevels;
        data.lastPlayed = DateTime.Now.ToShortDateString();

        string saveData = JsonUtility.ToJson(data);
        System.IO.File.WriteAllText(filename, saveData);

        Debug.Log(filename);
        Debug.Log("Game data saved!");
    }
}
