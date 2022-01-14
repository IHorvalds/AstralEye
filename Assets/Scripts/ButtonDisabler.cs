using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDisabler : MonoBehaviour
{
    // Start is called before the first frame update
    public Button[] buttons;
    private SaveFileLoader sfl;
    void Start()
    {
        sfl = GetComponent<SaveFileLoader>();
        sfl.LoadFile();

        foreach (var button in buttons)
        {
            button.enabled = false;
            button.GetComponent<Image>().color = Color.gray;
            button.GetComponentInChildren<Text>().color = new Color(0.6f, 0.6f, 0.6f);
        }

        for (int i = 0; i < sfl.runtimestate.unlockedLevels.Count; i++)
        {
            buttons[sfl.runtimestate.unlockedLevels[i]].enabled = true;
            buttons[sfl.runtimestate.unlockedLevels[i]].GetComponent<Image>().color = new Color(1.0f, 0.4f, 0.4f);
            buttons[sfl.runtimestate.unlockedLevels[i]].GetComponentInChildren<Text>().color = Color.white;
        }
    }
}
