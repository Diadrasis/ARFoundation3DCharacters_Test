using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RootMotionScript : MonoBehaviour
{

    Camera arCamera;
    Text txtPosition;
    void Start(){
        arCamera=Camera.main;
        txtPosition=GameObject.Find("txtPlanesCount").GetComponent<Text>();        
    }

    void OnAnimatorMove()
    {
        Animator animator = GetComponent<Animator>();                              
        if (animator)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
            {
                Vector3 newPosition = transform.position;
                newPosition.z -= animator.GetFloat("walkspeed") * Time.deltaTime; 
                transform.position = newPosition;
                //Vector3.MoveTowards(arCamera.transform.position, transform.position, animator.GetFloat("walkspeed") * Time.deltaTime);
            }
            txtPosition.text=arCamera.transform.position.ToString();                
        } 
    }  
}
