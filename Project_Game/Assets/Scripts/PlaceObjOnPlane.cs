using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;


public class PlaceObjOnPlane : MonoBehaviour
{
    //GPS
    public Text GPSStatus;
    public Text latitudeValue;
    public Text longitudeValue;

    

   



    public GameObject objectToPlace;
    // public GameObject objectToPlace2;
    //public GameObject objectToPlace3;


    private GameObject PlaceablePrefab;
    
    public GameObject placementIndicator;
    private Pose placementPose;
    private Transform placementTransform;
    private bool placementPoseIsValid = false;
    private TrackableId placedPlaneId = TrackableId.invalidId;

    ARRaycastManager m_RaycastManager;
    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    void Awake()
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();
    }

    void Start()
    {

        StartCoroutine(GPSLoc());
       
      
    }

    void Update()
    {
        UpdatePlacementPosition();
        UpdatePlacementIndicator();

        var lat = "49,6091";
        var lon = "20,70439";




        //  if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        //  {

        if (latitudeValue.ToString() != lat.ToString())

            if (longitudeValue.ToString() != lon.ToString())

                PlaceObject();





        //} 
    }

    private void UpdatePlacementPosition()
    {
        var screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));

        if (m_RaycastManager.Raycast(screenCenter, s_Hits, TrackableType.PlaneWithinPolygon))
        {
            placementPoseIsValid = s_Hits.Count > 0;

            if (placementPoseIsValid)
            {
                placementPose = s_Hits[0].pose;
                placedPlaneId = s_Hits[0].trackableId;

               //Debug.Log(placementPose);
               // Debug.Log(placedPlaneID);

                var planeManager = GetComponent<ARPlaneManager>();
                ARPlane arPlane = planeManager.GetPlane(placedPlaneId);
                placementTransform = arPlane.transform;
            }
        }
    }

    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementTransform.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    public void PlaceObject()
    {
        var positionfixed = placementPose.position;
        positionfixed += Vector3.up;
            Instantiate(PlaceablePrefab, placementPose.position, placementTransform.rotation);
           
    }



    

    public void SetPrefabType(GameObject prefabType)
    {
        PlaceablePrefab = prefabType;


        //Debug.Log(prefabType);
    }

    IEnumerator GPSLoc()
    {
        if (!Input.location.isEnabledByUser)
            yield break;

        Input.location.Start();

        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }


        if (maxWait < 1)
        {
            GPSStatus.text = "Time out";
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            GPSStatus.text = "Unable to determine device location";
            yield break;
        }
        else
        {
            GPSStatus.text = "Running";
            InvokeRepeating("UpdateGPSData", 0.5f, 1f);
            //Access granted
        }
    }
    private void UpdateGPSData()
    {
        if (Input.location.status == LocationServiceStatus.Running)
        {
            GPSStatus.text = "Running";
            latitudeValue.text = Input.location.lastData.latitude.ToString();
            longitudeValue.text = Input.location.lastData.longitude.ToString();
           
        }
        else
        {
            GPSStatus.text = "Stop";
        }

    }
}
