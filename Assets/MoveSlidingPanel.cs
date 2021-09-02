using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveSlidingPanel : MonoBehaviour
{
    // Start is called before the first frame update

    Animator animator;
    bool slide=true;
    public void Slide()
    {
       animator=GetComponent<Animator>(); 
         if (slide==true){
            animator.SetBool("bSlide", true);
        } else {
            animator.SetBool("bSlide", false);
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
