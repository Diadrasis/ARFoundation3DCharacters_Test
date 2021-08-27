using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MoveRootMotion : MonoBehaviour
{

    
    Text txtCoveredDistance;
    float coveredDistance=0f;
    
    void Start()
    {
        
        txtCoveredDistance=GameObject.Find("txtCoveredDistance").GetComponent<Text>();      
        coveredDistance=0f;
    }
    
    void OnAnimatorMove()
    { 
        Animator animator = GetComponent<Animator>();                              
        if (animator)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
            {
                Vector3 newPosition = transform.position;
                coveredDistance+=animator.GetFloat("walkspeed") * Time.deltaTime;
                newPosition.z -=animator.GetFloat("walkspeed") * Time.deltaTime; 
                transform.position = newPosition;
                //Vector3.MoveTowards(arCamera.transform.position, transform.position, animator.GetFloat("walkspeed") * Time.deltaTime);
            }                           
        }
        txtCoveredDistance.text=coveredDistance.ToString();
    }  
}
