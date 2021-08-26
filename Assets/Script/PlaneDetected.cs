using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

public class PlaneDetected : MonoBehaviour
{
    
    public GameObject AR_object;
    public Camera AR_Camera;
    public ARRaycastManager raycastManager;
    public List<ARRaycastHit> hits = new List<ARRaycastHit>();

    bool bVibrate=true;
    ARPlaneManager planeManager;
    int planes_count;   
     Text myText;

    void Start()
    {
        //find the message text
        myText=GameObject.Find("txtPlanesCount").GetComponent<Text>();
        //define the plane manager
        planeManager = GetComponent<ARPlaneManager>();
        //deactivate
        planeManager.enabled=false;
    }

    // Update is called once per frame
    void Update()
    {       
        
        if (planeManager.enabled==true){
            planes_count= planeManager.trackables.count;
            if (planes_count==1 && bVibrate==true){
                Handheld.Vibrate();
                bVibrate=false;                                         
            }
            myText.text=planes_count.ToString();
        }
        
        if (Input.GetMouseButtonDown(0)){
            if (planeManager.enabled==false){
                planeManager.enabled=true;               
            } else {
                Ray ray = AR_Camera.ScreenPointToRay(Input.mousePosition);
                if(raycastManager.Raycast(ray, hits))
                {
                    Pose pose = hits[0].pose;
                    Vector3 rot=pose.rotation.eulerAngles;
                    rot = new Vector3(rot.x, rot.y+180, rot.z);               
                    Instantiate(AR_object, pose.position, Quaternion.Euler(rot));
                    planeManager.enabled=false;                    
                } 
            }
        }
    }
}
