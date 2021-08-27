using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

public class PlaneDetected : MonoBehaviour
{
    
    public GameObject AR_object;
    public ARRaycastManager raycastManager;
    public List<ARRaycastHit> hits = new List<ARRaycastHit>();
    Camera arCamera;

    bool bVibrate=true;
    bool bCharacterAdded=false;
    ARPlaneManager planeManager;
    int planes_count=0;   
     Text txtPlanesCount;
     Text txtPosition;
    
    void Start()
    {
        //find the message text
        txtPlanesCount=GameObject.Find("txtPlanesCount").GetComponent<Text>();
        txtPosition=GameObject.Find("txtPlanesCount").GetComponent<Text>(); 
        
        //define the plane manager
        planeManager = GetComponent<ARPlaneManager>();
        //deactivate
        planeManager.enabled=false;
        arCamera=Camera.main;
        
         
         
    }

    // Update is called once per frame
    void Update()
    {       
        
        txtPosition.text=arCamera.transform.position.ToString();
        if (planeManager.enabled==true){
            planes_count=planeManager.trackables.count;
            //vibrate when first plane is detected
            if (planes_count>0 && bVibrate==true){
                Handheld.Vibrate();
                bVibrate=false;                                         
            }
        }
        txtPlanesCount.text=planes_count.ToString();
        
        if (Input.GetMouseButtonDown(0)){
            if (planeManager.enabled==false){
                planeManager.enabled=true;               
            } else {
                if (bCharacterAdded==false){                    
                    Ray ray = arCamera.ScreenPointToRay(Input.mousePosition);
                    if(raycastManager.Raycast(ray, hits))
                    {
                        Pose pose = hits[0].pose;
                        Vector3 rot=pose.rotation.eulerAngles;
                        //rotation of the ar object, inverted 
                        rot = new Vector3(rot.x, rot.y+180, rot.z); 
                        //calculate the rotation accordign to the camera              
                        Instantiate(AR_object, pose.position, Quaternion.Euler(rot));
                        //planeManager.enabled=false;                    
                    }
                    bCharacterAdded=!bCharacterAdded;
                }                
            }
        }
    }

    
}
