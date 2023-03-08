using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickLoader : MonoBehaviour
{

    public ArCursor cursor;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeToSofa()
    {
        cursor.sofa.SetActive(true);
        cursor.beanbag.SetActive(false);
        cursor.armchair.SetActive(false);
    }

    public void changeToBeanbag()
    {
        cursor.beanbag.SetActive(true);
        cursor.sofa.SetActive(false);
        cursor.armchair.SetActive(false);
    }

    public void changeToArmchair()
    {
        cursor.armchair.SetActive(true);
        cursor.beanbag.SetActive(false);
        cursor.sofa.SetActive(false);
    }


}
