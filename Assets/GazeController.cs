using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeController : MonoBehaviour
{
    public delegate void Hovered(bool isHoverd);
    public Hovered hovered; 

    GameObject[] interactableTargets;
    // Start is called before the first frame update
    private Camera arCamera;
    void Start()
    {
        // 查找相机
        arCamera = GameObject.FindObjectOfType<Camera>();
        
        interactableTargets = GameObject.FindGameObjectsWithTag("interactable");

    }

    // Update is called once per frame
    void Update()
    {
       Ray ray = new Ray (arCamera.transform.position, arCamera.transform.forward);//显示一条射线，只有在scene视图中才能看到
       RaycastHit hit;
    //    Debug.DrawLine(ray.origin,hit.point,Color.red,2);
       if (Physics.Raycast (ray, out hit, 100)){
           Debug.DrawLine(ray.origin,hit.point,Color.red,2);
           var objectName = hit.collider.gameObject.name;
            foreach(var a in interactableTargets){
                if(a.name == objectName){
                    a.GetComponent<InteractableTarget>().m_isHovered = true;
                }else{
                    a.GetComponent<InteractableTarget>().m_isHovered = false;
                }
            }          
        //    if(hit.transform.tag=="interactable"){
        //        var objectName = hit.collider.gameObject.name;
        //        hovered(true);
        //        Debug.Log("find " + objectName);
        //    }
       }else{
           Debug.Log("no interactables");
            foreach(var a in interactableTargets){
                // hovered(false);
                a.GetComponent<InteractableTarget>().m_isHovered = false;
            }
       }
    }
}
