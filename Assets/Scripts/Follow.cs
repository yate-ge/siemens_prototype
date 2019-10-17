using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform Anchor;
    private Vector3 lastPos;
    private Vector3 lastRot;
    

    void Start()
    {
        lastPos = Anchor.transform.position;
        lastRot = Anchor.transform.eulerAngles;
    }


    void Update()
    {
        //rotate
        float deltaY = (Anchor.eulerAngles - lastRot).y;
        if (deltaY != 0)
        {
            transform.RotateAround(Anchor.position, Vector3.up, deltaY);
        }
        lastRot = Anchor.eulerAngles;

        //pos
        Vector3 deltaPos = Anchor.position - lastPos;
        transform.position += new Vector3(deltaPos.x, 0, deltaPos.z);
        lastPos = Anchor.position;
    }
}
