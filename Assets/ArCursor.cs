using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using TMPro;
using Unity.VisualScripting;

public class ArCursor : MonoBehaviour
{
    public GameObject cursorChildObject;
    public ARRaycastManager raycastManager;
    [Space]
    public bool useCursor = false;
    public Sidebar sidebar;

    private OnClickLoader onClickLoader;
    private Transform dragTarget = null;
    private float timeSinceTargetChange;
    public float doubleClickThreshold = 0.5f;
    private float furnitureStartAngle;
    private float touchStartAngle;
    private Vector2 touchCenter;

    public TextMeshProUGUI debugAngle;

    // Start is called before the first frame update
    void Start()
    {
        onClickLoader = GetComponent<OnClickLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        if (useCursor)
        {
            UpdateCursor();
        }

        if (!sidebar.open && Input.touchCount > 0)
        {
            float sumX = 0;
            float sumY = 0;
            foreach (Touch touch in Input.touches)
            {
                sumX += touch.position.x;
                sumY += touch.position.y;
            }
            touchCenter = new Vector2(sumX, sumY) / Input.touchCount;

            foreach (Touch touch in Input.touches)
            {
                ProcessTouch(touch);
            }
        } else
        {
            dragTarget = null;
        }
    }

    void ProcessTouch(Touch touch)
    {
        switch (touch.phase)
        {
            case TouchPhase.Began:
                if (dragTarget != null)
                    break;
                // Check for furniture touch
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                debugAngle.text = $"{ray.origin} | {ray.direction}";
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 10000f, LayerMask.GetMask("Furniture")))
                {
                    debugAngle.color = Color.green;
                    if (timeSinceTargetChange >= Time.time - doubleClickThreshold)
                    {
                        // Delete object when double clicked
                        Destroy(hit.transform.gameObject);
                    } else
                    {
                        // Start dragging
                        dragTarget = hit.transform;
                    }
                }
                else
                {
                    // Place new object
                    if (useCursor)
                    {
                        dragTarget = onClickLoader.PlaceSelected(transform).transform;
                    }
                    else
                    {
                        ARRaycastHit? possibleHit = Raycast(touch.position);
                        if (possibleHit == null)
                            break;
                        dragTarget = onClickLoader.PlaceSelected(possibleHit.Value.pose).transform;
                    }
                }
                if (dragTarget != null)
                {
                    furnitureStartAngle = dragTarget.eulerAngles.y;
                }
                break;

            case TouchPhase.Moved:
            case TouchPhase.Stationary:
                if (dragTarget != null)
                {
                    if (touch.fingerId == 1)
                    {
                        // Rotation logic
                        Vector2 touch1 = Input.GetTouch(0).position;
                        Vector2 touch2 = touch.position;
                        Vector2 dt = touch2 - touch1;
                        float currentAngle = (Mathf.Atan(dt.y / dt.x) * Mathf.Rad2Deg) % 360;
                        dragTarget.eulerAngles = new Vector3(
                            dragTarget.eulerAngles.x,
                            furnitureStartAngle + touchStartAngle - currentAngle,
                            dragTarget.eulerAngles.z);
                        // debugAngle.text = $"t1: {touch1}  t2: {touch2}\ndt: {dt}\na: {currentAngle}\ny: {dragTarget.eulerAngles.y}";
                    }
                    else
                    {
                        // Move dragTarget to follow touch
                        ARRaycastHit? possibleHit = Raycast(touchCenter);
                        if (possibleHit == null)
                            break;
                        dragTarget.position = possibleHit.Value.pose.position;
                    }
                }
                break;
        }
    }

    public void IWasClicked(Transform me)
    {
        if (dragTarget == null)
        {
            dragTarget = me;
        }
    }

    void UpdateCursor()
    {
        Vector2 screenPosition = Camera.main.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
        ARRaycastHit? possibleHit = Raycast(screenPosition);
        if (possibleHit != null)
        {
            ARRaycastHit hit = (ARRaycastHit)possibleHit;
            transform.position = hit.pose.position;
            transform.rotation = hit.pose.rotation; 
        }
    }

    private ARRaycastHit? Raycast(Vector2 screenPosition)
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        raycastManager.Raycast(screenPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);
        return hits.Count > 0 ? hits[0] : null;
    }
}
