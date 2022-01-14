using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[Serializable]
public class SaveData
{
    public int goldenBottles;
    public int lives;
    public float currentHealth;
    public int[] unlockedLevels;

    public DateTime lastPlayed;
}

public class SaveFileLoader : MonoBehaviour
{
    public int saveFile;
    public RuntimeState runtimestate;
    public Button button;


    // private void OnGUI() {
    //     string filename = Application.persistentDataPath + "AstralEye-" + this.saveFile.ToString() + ".save";

    //     if (File.Exists(filename)) 
    //     {
    //         BinaryFormatter bf = new BinaryFormatter();
    //         FileStream file_stream = File.Open(filename, FileMode.Open);
    //         SaveData data = (SaveData)bf.Deserialize(file_stream);
    //         file_stream.Close();

    //         Text t = button.GetComponent<Text>();

    //         t.text = data.lastPlayed.ToShortDateString();
    //     }
    // }

    public void LoadFile()
    {
        if (this.saveFile != -1) {
            PlayerPrefs.SetInt("CurrentSaveFile", this.saveFile);
        } else {
            this.saveFile = PlayerPrefs.GetInt("CurrentSaveFile", 0);
        }

        runtimestate.currentSaveFile = this.saveFile;
        string filename = Application.persistentDataPath + "/AstralEye-" + runtimestate.currentSaveFile.ToString() + ".save";
        Debug.Log(filename);

        if (File.Exists(filename)) 
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file_stream = File.Open(filename, FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file_stream);
            file_stream.Close();

            runtimestate.currentSaveFile = saveFile;
            runtimestate.goldenBottles = data.goldenBottles;
            runtimestate.lives = data.lives;
            runtimestate.currentHealth = data.currentHealth;
            runtimestate.unlockedLevels = data.unlockedLevels;

            Debug.Log("Game data loaded!");

        }
	    else {
		    Debug.LogError("There is no save data!");
            SaveFile();
        }
    }

    public void SaveFile()
    {
        this.saveFile = PlayerPrefs.GetInt("CurrentSaveFile", 0);
        runtimestate.currentSaveFile = this.saveFile;
        string filename = Application.persistentDataPath + "/AstralEye-" + runtimestate.currentSaveFile.ToString() + ".save";

        BinaryFormatter bf = new BinaryFormatter(); 
        FileStream file = File.Create(filename); 
        SaveData data = new SaveData();

        data.goldenBottles = runtimestate.goldenBottles;
        data.lives = runtimestate.lives;
        data.currentHealth = runtimestate.currentHealth;
        data.unlockedLevels = runtimestate.unlockedLevels;
        data.lastPlayed = new DateTime();

        bf.Serialize(file, data);
        file.Close();
        Debug.Log(filename);
        Debug.Log("Game data saved!");
    }
}
