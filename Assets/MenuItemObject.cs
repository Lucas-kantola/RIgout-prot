using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MenuItemObject : MonoBehaviour
{
    public MenuItem template
    {
        set
        {
            text = value.displayName;
            icon = value.icon;
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
