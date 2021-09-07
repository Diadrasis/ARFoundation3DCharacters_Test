using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveSlidingPanel : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    bool slide=true;
    //runs when the button is clicked
    
    //reference to ShowARContentScript
    GameObject arSessionOrigin;

    void Start(){
        arSessionOrigin=GameObject.Find("AR Session Origin");        
    }
    
    public void Slide()
    {
        animator=GetComponent<Animator>(); 
        if (slide==true){
            animator.SetBool("bSlide", true);
            arSessionOrigin.GetComponent<ShowARContent>().appState=ShowARContent.State.chooseModel;
        } else {
            animator.SetBool("bSlide", false);
            arSessionOrigin.GetComponent<ShowARContent>().appState=ShowARContent.State.showModel;
        }
        slide=!slide;
    }

    public Toggle GetSelectedTogle(){
        Toggle[] toggles=GetComponentsInChildren<Toggle>();       
        foreach (Toggle t in toggles){
            if (t.isOn==true){
                return t;
            }
        }
        return toggles[0];       
    }  
}
