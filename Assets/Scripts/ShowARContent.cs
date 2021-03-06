using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

public class ShowARContent : MonoBehaviour
{
    //track the state of the game
    public enum State {chooseModel, recognizePlanes, showModel, hideModel};
    //keep the app state
    public State appState;
    //the character model we will show
    GameObject AR_object;
    public ARRaycastManager raycastManager;
    public List<ARRaycastHit> hits = new List<ARRaycastHit>();
    Camera arCamera;
    bool bVibrate=true;   
    ARPlaneManager planeManager;
    int planes_count; 
    int current_planes;  
    Text txtPlanesCount;
    Text txtCameraPosition;
    Text txtPosePosition;
    Text txtModelName;
    Text txtAppState;
    

    //the 3d model 
    GameObject model;
    
    //reference to the sliding panel
    GameObject panel;
    //The selected panel toggle
    Toggle t;

    void Awake(){
         appState=State.recognizePlanes; 
    }
    
    void Start()
    {
        //define the message text
        /*
        
        txtCameraPosition=GameObject.Find("txtCameraPosition").GetComponent<Text>();
        
        */
        txtPlanesCount=GameObject.Find("txtPlanesCount").GetComponent<Text>();
        txtModelName=GameObject.Find("txtModelName").GetComponent<Text>();
        //txtModelName.text="No model set";
        txtAppState=GameObject.Find("txtAppState").GetComponent<Text>(); 
        /*
        txtPosePosition=GameObject.Find("txtPosePosition").GetComponent<Text>();       
        //define the ARCamera
        arCamera=Camera.main;
        */        
        //define the plane manager
        planeManager = GetComponent<ARPlaneManager>();        
        //deactivate
        planeManager.enabled=false;        
       
        //get the camera
        arCamera=Camera.main; 
        //referebce the panel 
        panel=GameObject.Find("SlidingPanel"); 
        
        planes_count=0;
        current_planes=0; 
          
           
    }

    // Update is called once per frame
    void Update()
    {       
        txtAppState.text=appState.ToString();
        //txtCameraPosition.text=arCamera.transform.position.ToString();        
        txtModelName.text=planeManager.enabled.ToString(); 
        txtPlanesCount.text=planes_count.ToString();
        if (appState==State.recognizePlanes && planeManager.enabled==true){            
            planes_count=planeManager.trackables.count;
            if (planes_count>current_planes){           
                Handheld.Vibrate();
                appState=State.showModel;
                planeManager.enabled=false;
                current_planes=planes_count;                                  
            }       
        }     
        
        //when clicked
        if (Input.GetMouseButtonDown(0)){
            //Handheld.Vibrate();
            if (appState==State.recognizePlanes){               
               planeManager.enabled=true;                                             
            } else if (appState==State.showModel){
                //check which toggle is selected           
                t=panel.GetComponent<MoveSlidingPanel>().GetSelectedTogle();
                //show the label of the selected model
                txtModelName.text=t.GetComponentInChildren<Text>().text;              
                //Set the prefab according the selected toggle
                if (t.name=="Toggle1"){                
                    AR_object=(GameObject)Resources.Load("jasper");                    
                } else if (t.name=="Toggle2"){
                    AR_object=(GameObject)Resources.Load("pearl");   
                } else if (t.name=="Toggle3"){
                    AR_object=(GameObject)Resources.Load("Athenian_A");                   
                } else {
                    AR_object=(GameObject)Resources.Load("jasper");                      
                }
                //show the model               
                Ray ray = arCamera.ScreenPointToRay(Input.mousePosition);
                if(raycastManager.Raycast(ray, hits))
                {                   
                    //find the point on the plane
                    Pose pose = hits[0].pose;
                    Vector3 rot=pose.rotation.eulerAngles;
                    //rotation of the ar object, inverted 
                    rot = new Vector3(rot.x, rot.y+180, rot.z); 
                    
                    //place the model on the plane and in spdcific distance of the camera
                    /*
                    Vector3 newPos=arCamera.transform.forward*5;   
                    newPos=new Vector3(arCamera.transform.forward.x, -0.5f, arCamera.transform.forward.z)*5;
                    */           
                    //model=Instantiate(AR_object, newPos, Quaternion.Euler(rot));
                    model=Instantiate(AR_object, pose.position, Quaternion.Euler(rot));
                    //txtPosePosition.text=pose.position.ToString();
                    //DeactivatePlanes(planeManager);
                    Handheld.Vibrate();
                    appState=State.hideModel;
                }
                
            } else if (appState==State.hideModel){
                //hide the modl
                Destroy(model);
                //prepare for next click                
                Handheld.Vibrate();
                appState=State.showModel;
            }
        }
    }   
}
