using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuScreen : MonoBehaviour
{
    public List<string> textNames = new List<string>();
    public List<TMP_Text> texts = new List<TMP_Text>();

    public List<string> buttonNames = new List<string>();
    public List<Button> buttons = new List<Button>();

    public TMP_Text GetText(string name)
    {
        TMP_Text text = null;
        for (int i = 0; i < textNames.Count; i++)
        {
            if (name.Trim().Equals(textNames[i].Trim()))
            {
                text = texts[i];
                break;
            }
        }

        return text;
    }

    public Button GetButton(string name)
    {
        Button button = null;
        for (int i = 0; i < buttonNames.Count; i++)
        {
            if (name.Trim().Equals(buttonNames[i].Trim()))
            {
                button = buttons[i];
                break;
            }
        }

        return button;
    }
}
