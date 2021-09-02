using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

public class PlaneDetected : MonoBehaviour
{
    
    enum State {showModel, hideModel};
    GameObject AR_object;
    public ARRaycastManager raycastManager;
    public List<ARRaycastHit> hits = new List<ARRaycastHit>();
    Camera arCamera;

    bool bVibrate=true;
   
    ARPlaneManager planeManager;
    int planes_count=0;   
     Text txtPlanesCount;
     Text txtPosition;
     Text txtCoveredDistance;

      //the 3d model 
      GameObject model;

     //keep the app state
     State appState=State.showModel;

     //toggle
     Toggle t;
     GameObject panel; 
    
    void Start()
    {
        //find the message text
        txtPlanesCount=GameObject.Find("txtPlanesCount").GetComponent<Text>();
        txtPosition=GameObject.Find("txtPlanesCount").GetComponent<Text>();
        txtCoveredDistance=GameObject.Find("txtCoveredDistance").GetComponent<Text>();                
        
        //define the plane manager
        planeManager = GetComponent<ARPlaneManager>();
        
        //deactivate
        planeManager.enabled=false;
        arCamera=Camera.main; 
        //  
        panel=GameObject.Find("SlidingPanel");
        txtCoveredDistance.text="MITSOS";
         
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
            

            
            //check which toggle is selected           
            t=panel.GetComponent<MoveSlidingPanel>().GetSelectedTogle();
            txtCoveredDistance.text=t.name;
          //Set the prefab according the selected toggle
            if (t.name=="Toggle1"){                
                AR_object=(GameObject)Resources.Load("jasper");

            } else if (t.name=="Toggle2"){
                AR_object=(GameObject)Resources.Load("pearl");
            } else {
                AR_object=(GameObject)Resources.Load("jasper");   
            }
            

            if (planeManager.enabled==false){
                planeManager.enabled=true;               
            } else {
                if (appState==State.showModel){
                    Ray ray = arCamera.ScreenPointToRay(Input.mousePosition);
                    if(raycastManager.Raycast(ray, hits))
                    {
                        Pose pose = hits[0].pose;
                        Vector3 rot=pose.rotation.eulerAngles;
                        //rotation of the ar object, inverted 
                        rot = new Vector3(rot.x, rot.y+180, rot.z); 
                        //calculate the rotation accordign to the camera              
                        model=Instantiate(AR_object, pose.position, Quaternion.Euler(rot));
                        //planeManager.enabled=false;                    
                    }
                    appState=State.hideModel;
                }else{
                    Destroy(model);
                    appState=State.showModel;
                }                
            }
        }
    }

    
}
