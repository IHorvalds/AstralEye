using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private int sceneToLoad;
    // Start is called before the first frame update
    public void StartGame()
    {
        SceneManager.LoadScene(sceneToLoad);
    }


}
