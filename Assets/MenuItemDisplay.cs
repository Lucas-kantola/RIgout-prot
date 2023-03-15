using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MenuItemDisplay : MonoBehaviour
{
    public MenuItem template
    {
        set
        {
            if (value)
            {
                text = value.displayName;
                icon = value.icon;
            } else
            {
                text = "N/A";
                icon = null;
            }
        }
    }

    public Image imageObject;
    public TextMeshProUGUI textObject;

    private string text
    {
        get { return textObject.text; }
        set { textObject.text = value; }
    }

    private Sprite icon
    {
        get { return imageObject.sprite; }
        set { imageObject.sprite = value; }
    }
}
