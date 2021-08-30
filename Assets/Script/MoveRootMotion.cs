using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MoveRootMotion : MonoBehaviour
{
    
    Text txtCoveredDistance;
    float coveredDistance=0f;

    Camera arCamera;

    
    void Start()
    {
        
        txtCoveredDistance=GameObject.Find("txtCoveredDistance").GetComponent<Text>();      
        coveredDistance=0f;
        arCamera=Camera.main;
    }
    
    void OnAnimatorMove()
    { 
        Animator animator = GetComponent<Animator>();                              
        if (animator)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
            {
                
                //we want him to move towards the camera
                Quaternion newQuaternion;
                Vector3 rot=arCamera.transform.rotation.eulerAngles;
                Vector3 newRot=new Vector3(rot.x, rot.y+180, rot.z);
                transform.rotation=Quaternion.Euler(newRot);
                                
                /*
                Vector3 newPosition = transform.position;
                coveredDistance+=animator.GetFloat("walkspeed") * Time.deltaTime;
                newPosition.z -=animator.GetFloat("walkspeed") * Time.deltaTime; 
                transform.position = newPosition;
                */
                //Vector3.MoveTowards(arCamera.transform.position, transform.position, animator.GetFloat("walkspeed") * Time.deltaTime);

                transform.position = Vector3.MoveTowards(transform.position, arCamera.transform.position, animator.GetFloat("walkspeed") * Time.deltaTime);


                

            }                           
        }
        txtCoveredDistance.text=coveredDistance.ToString();
    }  
}
