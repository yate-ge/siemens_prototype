using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(ARPlaneManager))]
[RequireComponent(typeof(ARRaycastManager))]
public class PlaceController : MonoBehaviour
{
    ARRaycastManager m_RaycastManager;  
    ARSessionOrigin arOrigin;

    ARPlaneManager m_ARPlaneManager;

    
    public GameObject objectToPlace;
    private GameObject placementIndicator;
    private Pose placementPose;

    private Text debugInfo;

    private int hitCounter;

    private bool placementPoseIsValid = false;

    // 调试用
    public bool SetIndicatorDisplay = true;
    
    void Awake() {
        arOrigin = GetComponent<ARSessionOrigin>();
        m_RaycastManager = GetComponent<ARRaycastManager>();
        m_ARPlaneManager = GetComponent<ARPlaneManager>();  
        placementIndicator = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("Indicator"));      
    }

    // Start is called before the first frame update
    void Start()
    {
        arOrigin = FindObjectOfType<ARSessionOrigin>();
        debugInfo = GameObject.Find("DebugInfo").GetComponent<Text>();
        m_ARPlaneManager.planePrefab.SetActive(true);
        objectToPlace.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlacementPose();
        UpdateIndicator();

        debugInfo.text = "Hits.count =   " + hitCounter + "   placementPose Is Valid = " + placementPoseIsValid + "\r\n" + placementPose;
    }

    private void UpdatePlacementPose(){
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        
        // Cast a Ray against trackables, i.e., detected features such as planes. 探测被检测到的平面,参数 hits
        m_RaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);
        hitCounter = hits.Count;

        // 判断是否有检测到平面 hits数组已经被改变
        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid){
            placementPose = hits[0].pose;

        // 设置指示标志的方向
            var cameraForward = Camera.current.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing); 
        }

    }
        
    private void UpdateIndicator(){
        if (placementPoseIsValid && SetIndicatorDisplay)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }            
    }

    public void PlaceMachine(){
        if(placementPoseIsValid){
            objectToPlace.SetActive(true);
            objectToPlace.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
            // Instantiate(objectToPlace, placementPose.position, placementPose.rotation);
        }
        
    }


    public void TogglePlaneDetection(){
        m_ARPlaneManager.enabled = !m_ARPlaneManager.enabled;

        if (m_ARPlaneManager.enabled)
        {
            SetAllPlanesActive(true);
        }
        else
        {
            SetAllPlanesActive(false);
        }
    }

    public void ToggleIndicatorDisplay(){
        SetIndicatorDisplay = !SetIndicatorDisplay;
 
    }

    void SetAllPlanesActive(bool value)
    {
        foreach (var plane in m_ARPlaneManager.trackables)
            plane.gameObject.SetActive(value);
    }



}
