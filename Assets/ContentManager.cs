using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class ContentManager : MonoBehaviour
{
    public CategoryItem currentMenu;
    public GameObject menuItemPrefab;
    [Header("UI Elements")]
    public TextMeshProUGUI title;
    public GridLayoutGroup grid;
    public Button backButton;

    // Start is called before the first frame update
    void Start()
    {
        RefreshMenu();
        backButton.onClick.AddListener(new UnityAction(() =>
        {
            currentMenu = currentMenu.parent;
            RefreshMenu();
        }));
    }

    [ContextMenu("Refresh")]
    public void RefreshMenu()
    {
        title.text = currentMenu.displayName;

        foreach (Transform child in grid.transform)
        {
            Destroy(child.gameObject);
        }

        foreach(MenuItem item in currentMenu.items)
        {
            MenuItemObject menuItem = Instantiate(menuItemPrefab, grid.transform).GetComponent<MenuItemObject>();
            menuItem.text = item.displayName;
            menuItem.icon = item.icon;

            Button button = menuItem.GetComponent<Button>();
            UnityAction listener;
            if (item is CategoryItem)
            {
                listener = new UnityAction(() =>
                {
                   currentMenu = (CategoryItem)item;
                   RefreshMenu();
                });
            } else if (item is FurnitureItem)
            {
                listener = new UnityAction(() =>
                {
                    SelectItem((FurnitureItem)item);
                });
            } else
            {
                Debug.LogWarning($"{item.displayName} is neither a FurnitureItem nor CategoryItem");
                continue;
            }
            button.onClick.AddListener(listener);
        }

        backButton.gameObject.SetActive(currentMenu.parent != null);
    }

    public void SelectItem(FurnitureItem item)
    {
        Debug.Log($"Loaded {item.displayName}");
    }
}
