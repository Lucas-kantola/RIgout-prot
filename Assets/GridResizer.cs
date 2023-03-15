using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridResizer : MonoBehaviour
{
    [Range(1, 10)]
    public int columns = 3;

    private RectTransform tf;
    private GridLayoutGroup grid;

    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<RectTransform>();
        grid = GetComponent<GridLayoutGroup>();
        Apply();
    }

    [ContextMenu("Apply")]
    public void Apply()
    {
        float cellWidth = (tf.rect.width - grid.spacing.x * (columns - 1)) / columns;
        grid.cellSize = new Vector3(cellWidth, grid.cellSize.y);
    }
}
