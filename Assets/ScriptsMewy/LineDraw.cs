using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDraw : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private LineController line;
    private GameObject magnet;

    // Start is called before the first frame update
    void Start()
    {
        points[0] = GameObject.FindWithTag("Player").transform;
        //points[0].position += new Vector3(0f, 10f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindGameObjectsWithTag("Magnet").Length == 0) {
            points[1] = GameObject.FindWithTag("Player").transform;
        }
        else {
            points[1] = GameObject.FindWithTag("Magnet").transform;
        }
        line.SetUpLine(points);

        //points[1] = GameObject.FindWithTag("Magnet").transform;
    }
}
