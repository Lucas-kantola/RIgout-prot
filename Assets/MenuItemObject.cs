using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuItemObject : MonoBehaviour
{
    public Image imageObject;
    public TextMeshProUGUI textObject;

    public string text
    {
        get { return textObject.text; }
        set { textObject.text = value; }
    }

    public Sprite icon
    {
        get { return imageObject.sprite; }
        set { imageObject.sprite = value; }
    }
}
