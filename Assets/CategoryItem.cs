using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Menu Category")]
public class CategoryItem : MenuItem
{
    public CategoryItem parent;
    public List<MenuItem> items;
}
