using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

public class ShowARContent : MonoBehaviour
{
    //track the state of the game
    public enum State {start, chooseModel, showModel, hideModel};
    //keep the app state
    public State appState;
    //the character model we will show
    GameObject AR_object;
    public ARRaycastManager raycastManager;
    public List<ARRaycastHit> hits = new List<ARRaycastHit>();
    Camera arCamera;
    bool bVibrate=true;   
    ARPlaneManager planeManager;
    int planes_count=0;   
    Text txtPlanesCount;
    Text txtCameraPosition;
    Text txtModelName;

    //the 3d model 
    GameObject model;
    
    //reference to the sliding panel
    GameObject panel;
    //The selected panel toggle
    Toggle t;

    void Awake(){
         appState=State.start;
    }
    
    void Start()
    {
        //define the message text
        txtPlanesCount=GameObject.Find("txtPlanesCount").GetComponent<Text>();
        txtCameraPosition=GameObject.Find("txtCameraPosition").GetComponent<Text>();
        txtModelName=GameObject.Find("txtModelName").GetComponent<Text>();
        txtModelName.text="No model set";
        //define the ARCamera
        arCamera=Camera.main;        
        //define the plane manager
        planeManager = GetComponent<ARPlaneManager>();        
        //deactivate
        planeManager.enabled=false;        
        //get the camera
        arCamera=Camera.main; 
        //referebce the panel 
        panel=GameObject.Find("SlidingPanel");         
    }

    // Update is called once per frame
    void Update()
    {       
        
        txtCameraPosition.text=arCamera.transform.position.ToString();
        if (planeManager.enabled==true){
            planes_count=planeManager.trackables.count;
            //vibrate when first plane is detected
            if (planes_count>0 && bVibrate==true){
                Handheld.Vibrate();
                bVibrate=false;                                         
            }
        }
        txtPlanesCount.text=planes_count.ToString();
        
        //when clicked
        if (Input.GetMouseButtonDown(0)){        
            if (appState==State.showModel){
                if (!planeManager.enabled){
                    planeManager.enabled=!planeManager.enabled;               
                }
                //check which toggle is selected           
                t=panel.GetComponent<MoveSlidingPanel>().GetSelectedTogle();
                txtModelName.text=t.name;
                //Set the prefab according the selected toggle
                if (t.name=="Toggle1"){                
                    AR_object=(GameObject)Resources.Load("jasper");

                } else if (t.name=="Toggle2"){
                    AR_object=(GameObject)Resources.Load("pearl");
                } else {
                    AR_object=(GameObject)Resources.Load("jasper");   
                }
                //show the model
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
                //prepare for next click
                appState=State.hideModel;
            }else if (appState==State.hideModel){
                //hide the modl
                Destroy(model);
                //stop the plane manager
                if (planeManager.enabled){
                    planeManager.enabled=!planeManager.enabled;                
                }
                //prepare for next click
                appState=State.showModel;
            }
        }
    }
}
