using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraScript : MonoBehaviour
{
    /*
    public Transform player;
    //private Vector3 offset = new Vector3(0f, -5.0f, -10f);
    private Vector3 offset = new Vector3(0f, 5.0f, -10f);
    private float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;

    public CinemachineVirtualCamera vcam;
    private bool zoomedIn = true;

    private Transform camBoxTransform;
    // Start is called before the first frame update
    void Start()
    {
        camBoxTransform = this.gameObject.transform.GetChild(0).transform;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = player.transform.position + new Vector3(0, 2, -5);
        Vector3 targetPosition = player.transform.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        //transform.position = new Vector3(10f, 10f, 10f);

        if (Input.GetKeyDown(KeyCode.Tab)) {
            zoomedIn = !zoomedIn;

            if (zoomedIn) {
                vcam.m_Lens.OrthographicSize = 10;
                camBoxTransform.localScale = new Vector3(1, 1, 1);
                this.gameObject.transform.GetChild(0).transform.localScale = new Vector3(1, 1, 1);
            }
            else {
                vcam.m_Lens.OrthographicSize = 20;
                camBoxTransform.localScale = new Vector3(2, 2, 2);
                this.gameObject.transform.GetChild(0).transform.localScale = new Vector3(2, 2, 2);
            }
        }
    }
    */
}
