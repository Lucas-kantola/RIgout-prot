using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ArCursor : MonoBehaviour
{
    public GameObject sofa;
    public GameObject armchair;
    public GameObject beanbag;

    public GameObject cursorChildObject;
    public ARRaycastManager raycastManager;

    public bool useCursor = true;

    // Start is called before the first frame update
    void Start()
    {
        sofa.SetActive(false);
        armchair.SetActive(false);
        beanbag.SetActive(true);
        cursorChildObject = beanbag;

    }

    // Update is called once per frame
    void Update()
    {
        if (useCursor)
        {
            UpdateCursor();
        }
    }

    void UpdateCursor(){

        Vector2 screenPosition = Camera.main.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        raycastManager.Raycast(screenPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

        if (hits.Count > 0)
        {
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation; 
        }
    }

}
