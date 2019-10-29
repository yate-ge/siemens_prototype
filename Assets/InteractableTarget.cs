using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InteractableTarget : MonoBehaviour
{
    private Renderer render;
    public bool m_isHovered;
    GazeController m_GazeController;
    // Start is called before the first frame update
    void Start()
    {
        m_isHovered = false;
        m_GazeController = GameObject.Find("GazeController").GetComponent<GazeController>();
        // m_GazeController.hovered = IsHovered;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_isHovered){
            GetComponent<Renderer>().material.color = Color.red;
        }else{
            GetComponent<Renderer>().material.color = Color.white;
        }        
    }

    void IsHovered(bool isHoverd){
        if(isHoverd){
            GetComponent<Renderer>().material.color = Color.red;
        }else{
            GetComponent<Renderer>().material.color = Color.white;
        }
    }
        
}
