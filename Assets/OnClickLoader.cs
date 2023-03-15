using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using UnityEngine.EventSystems;

public class OnClickLoader : MonoBehaviour
{
    public MenuItemDisplay selectionPreview;

    private ArCursor cursor;
    private FurnitureItem selected;

    private void Start()
    {
        cursor = GetComponent<ArCursor>();

        SetSelected(null);

        if (selectionPreview == null)
        {
            Debug.LogWarning($"{transform.name}: selectionPreview is null");
        }
    }

    public void SetSelected(FurnitureItem item)
    {
        Debug.Log($"Select {item.displayName}");
        selected = item;
        if (selectionPreview)
        {
            selectionPreview.template = selected;
            selectionPreview.gameObject.SetActive(selected != null);
        }
    }

    public GameObject PlaceSelected(Pose target)
    {
        if (target == null)
            return null;
        Debug.Log($"Place {selected.displayName}");
        return Instantiate(selected.prefab, target.position, target.rotation);
    }

    public GameObject PlaceSelected(Transform target)
    {
        return PlaceSelected(new Pose(target.position, target.rotation));
    }
}
