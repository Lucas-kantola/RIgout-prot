using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickLoader : MonoBehaviour
{

    public ArCursor cursor;
    public GameObject Sofa;
    public GameObject Beanbag;
    public GameObject Armchair;

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
        cursor.cursorChildObject = Sofa;
    }

    public void changeToBeanbag()
    {
        cursor.cursorChildObject = Beanbag;
    }

    public void changeToArmchair()
    {
        cursor.cursorChildObject = Armchair;
    }


}
