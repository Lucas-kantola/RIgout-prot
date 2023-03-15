using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class OnClickLoader : MonoBehaviour
{

    public ArCursor cursor;
    public Button placeButton;
    private FurnitureItem selected;

    private void Start()
    {
        placeButton.onClick.AddListener(new UnityAction(() => {
            PlaceSelected();
        }));

        SetSelected(null);
    }

    public void SetSelected(FurnitureItem item)
    {
        selected = item;
        placeButton.interactable = selected != null;
    }

    public void PlaceSelected()
    {
        Debug.Log($"Placed {selected.displayName}");
    }
}
