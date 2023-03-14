using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class IconGenerator : MonoBehaviour
{
    // Code from https://www.youtube.com/watch?v=SGz3sbZkfkg

    new private Camera camera;

    public string pathFolder;
    [Space]
    public List<GameObject> sceneObjects;
    public List<FurnitureItem> furnitureItems;

    private void Awake()
    {
        camera = GetComponent<Camera>();
    }

    [ContextMenu("Screenshot")]
    private void ProcessScreenshots()
    {
        StartCoroutine(Screenshot());
    }

    private IEnumerator Screenshot()
    {
        for (int i = 0; i < sceneObjects.Count; i++)
        {
            GameObject obj = sceneObjects[i];
            FurnitureItem furniture = furnitureItems[i];

            obj.gameObject.SetActive(true);

            yield return null;

            TakeScreenshot($"{Application.dataPath}/{pathFolder}/{furniture.id}_icon.png");

            yield return null;
            obj.gameObject.SetActive(false);
            Sprite s = AssetDatabase.LoadAssetAtPath<Sprite>($"Assets/{pathFolder}/{furniture.id}_icon.png");
            if (s != null)
            {
                furniture.icon = s;
                EditorUtility.SetDirty(furniture);
            }

            yield return null;

        }
    }

    private void TakeScreenshot(string fullPath)
    {
        if (camera == null)
        {
            camera = GetComponent<Camera>();
        }
        RenderTexture rt = new RenderTexture(256, 256, 32);
        camera.targetTexture = rt;
        Texture2D screenshot = new Texture2D(256, 256, TextureFormat.RGBA32, false);
        camera.Render();
        RenderTexture.active = rt;
        screenshot.ReadPixels(new Rect(0, 0, 256, 256), 0, 0);
        camera.targetTexture = null;
        RenderTexture.active = null;

        if (Application.isEditor)
        {
            DestroyImmediate(rt);
        }
        else
        {
            Destroy(rt);
        }

        byte[] bytes = screenshot.EncodeToPNG();
        System.IO.File.WriteAllBytes(fullPath, bytes);
#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
    }
}
