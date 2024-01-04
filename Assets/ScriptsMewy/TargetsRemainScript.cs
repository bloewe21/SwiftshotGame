using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TargetsRemainScript : MonoBehaviour
{
    //private string myText;
    private GameObject[] targets;
    private int arrayLength;
    // Start is called before the first frame update
    void Start()
    {
        //myText = GetComponent<TMPro.TextMeshProUGUI>().text;
    }

    // Update is called once per frame
    void Update()
    {
        targets = GameObject.FindGameObjectsWithTag("Target");
        arrayLength = targets.Length;
        //myText = "Targets: "+arrayLength;
        GetComponent<TMPro.TextMeshProUGUI>().text = "Targets: "+arrayLength;
    }
}
