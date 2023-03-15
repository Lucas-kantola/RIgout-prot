using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sidebar : MonoBehaviour
{
    public bool open = false;
    public float speed = 1.0f;
    private RectTransform tf;
    private Vector3 openTarget;
    private Vector3 closedTarget;
    private float x;


    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<RectTransform>();
        openTarget   = new Vector3(tf.rect.width / 2, tf.rect.height / 2, tf.position.z);
        closedTarget = new Vector3(-tf.rect.width / 2, tf.rect.height / 2, tf.position.z);
        tf.position = open ? openTarget : closedTarget;
        x = open ? 1.0f : 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (open)
        {
            x += Time.deltaTime * speed;
        } else
        {
            x -= Time.deltaTime * speed;
        }
        x = Mathf.Clamp(x, 0.0f, 1.0f);
        float t = easeInOut(x);
        
        tf.position = Vector3.Lerp(closedTarget, openTarget, t);
    }

    private float easeInOut(float x)
    {
        return x < 0.5 ? 2 * x * x : 1 - Mathf.Pow(-2 * x + 2, 2) / 2;
    }

    public void Toggle()
    {
        open = !open;
    }

    public void Open()
    {
        open = true;
    }

    public void Close()
    {
        open = false;
    }
}
