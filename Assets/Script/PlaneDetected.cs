using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

public class PlaneDetected : MonoBehaviour
{
    // Start is called before the first frame update
    ARPlaneManager planeManager;
    int planes_no;
    bool flag=false;
    Text myText;

    void Start()
    {
        myText=GameObject.Find("myText").GetComponent<Text>();;
    }

    // Update is called once per frame
    void Update()
    {
       planeManager = GetComponent<ARPlaneManager>(); 
       planes_no= planeManager.trackables.count;
       if (planes_no==1 && flag==false){
            Handheld.Vibrate();
            flag=true;            
       }
       myText.text=planes_no.ToString();
    }

}
