using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
        //PlayerPrefs.DeleteAll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake () {
	    //QualitySettings.vSyncCount = 0;  // VSync must be disabled

        //TO RESET FRAMERATE TO UNLIMITED:
	    //Application.targetFrameRate = -1;
    }
}
