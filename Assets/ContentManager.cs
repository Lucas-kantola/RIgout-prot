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
    [Space]
    public OnClickLoader onClickLoader;
    [Header("UI Elements")]
    public TextMeshProUGUI title;
    public GridLayoutGroup grid;
    public Button backButton;
    public Sidebar sidebar;

    // Start is called before the first frame update
    void Start()
    {
        RefreshMenu();
        backButton.onClick.AddListener(new UnityAction(() =>
        {
            if (currentMenu.parent)
            {
                currentMenu = currentMenu.parent;
                RefreshMenu();
            } else
            {
                sidebar.Close();
            }
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
            MenuItemDisplay display = Instantiate(menuItemPrefab, grid.transform).GetComponent<MenuItemDisplay>();
            display.template = item;

            Button button = display.GetComponent<Button>();
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
    }

    public void SelectItem(FurnitureItem item)
    {
        onClickLoader.SetSelected(item);
    }
}
