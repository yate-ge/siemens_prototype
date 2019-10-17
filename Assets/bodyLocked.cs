using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class bodyLocked : MonoBehaviour
{
    public Transform Locator;

    void  Awake() {
        Locator.position = transform.position;
        Locator.eulerAngles = transform.eulerAngles;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = Locator.transform.position;
        transform.eulerAngles = new Vector3(0,Locator.transform.eulerAngles.y,0);
        //Debug.Log(transform.eulerAngles);
    }
}
