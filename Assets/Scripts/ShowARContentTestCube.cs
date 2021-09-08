using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;


public class ShowARContentTestCube : MonoBehaviour
{
    //track the state of the game
    public GameObject AR_object;
    public ARRaycastManager raycastManager;
    public List<ARRaycastHit> hits = new List<ARRaycastHit>();
    Camera arCamera;      
    ARPlaneManager planeManager;
    bool bVibrate=true;
    bool bShowModel=true;
    GameObject model;

    Text labelOriginPos, labelCameraPos, labelOriginRot, labelCameraRot;

    // Update is called once per frame
    void Start(){
        arCamera=Camera.main;        
        //define the plane manager
        planeManager = GetComponent<ARPlaneManager>();
        labelOriginPos=GameObject.Find("txtOriginPos").GetComponent<Text>(); 
        labelCameraPos=GameObject.Find("txtCameraPos").GetComponent<Text>();  
        labelOriginRot=GameObject.Find("txtOriginRot").GetComponent<Text>(); 
        labelCameraRot=GameObject.Find("txtCameraRot").GetComponent<Text>();    
    }
    void Update()
    {     
        if (labelOriginPos!=null){
            labelOriginPos.text=transform.position.ToString(); 
        } 
        if (labelCameraPos!=null){
            labelCameraPos.text=arCamera.transform.position.ToString(); 
        }
        if (labelOriginRot!=null){
            labelOriginRot.text=transform.transform.rotation.ToString(); 
        } 
        if (labelCameraRot!=null){
            labelCameraRot.text=arCamera.transform.rotation.ToString(); 
        }            
        //vibrate when first plane is detected
        if (planeManager.trackables.count>0 && bVibrate==true){
            Handheld.Vibrate();
            bVibrate=false;                                         
        }
                 
        //when clicked
        if (Input.GetMouseButtonDown(0)){ 
            //show the model
            Ray ray = arCamera.ScreenPointToRay(Input.mousePosition);
            /*
            if(raycastManager.Raycast(ray, hits))
            {
                Pose pose = hits[0].pose;
                //Vector3 rot=pose.rotation.eulerAngles;
                //rotation of the ar object, inverted 
                //rot = new Vector3(rot.x, rot.y+180, rot.z); 
                //place the model on the plane and in spdcific distance of the camera
                //Vector3 newPos=arCamera.transform.forward*5;   
                //newPos=new Vector3(arCamera.transform.forward.x, -0.5f, arCamera.transform.forward.z)*5;
                //Instantiate(AR_object, newPos, Quaternion.Euler(rot));  
                if (bShowModel==true){
                    model=Instantiate(AR_object, pose.position, pose.rotation);
                    Handheld.Vibrate();
                    bShowModel=false;
                }  else {
                    Destroy(model);
                    bShowModel=true;
                }            
            }
            */
             if (bShowModel==true){
                Vector3 newPos;
                //newPos= new Vector3(arCamera.transform.position.x, 0, arCamera.transform.position.z+5);
                newPos= new Vector3(arCamera.transform.forward.x, 0, arCamera.transform.forward.z)*5;
                newPos=newPos + arCamera.transform.position;
                model=Instantiate(AR_object, newPos, transform.rotation);
                Handheld.Vibrate();
                bShowModel=false;
            } else {
                Destroy(model);
                bShowModel=true;
            }
        }
    }
}
