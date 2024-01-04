using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorScript : MonoBehaviour
{
    public GameObject myTarget;
    public GameObject myPlayer;

    private float x_dist;
    private float y_dist;

    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPos = cam.WorldToScreenPoint(transform.position);
        //print(screenPos.x);

        if (myTarget.GetComponent<TargetScript>().isVisible) {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        else {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }

        x_dist = myTarget.transform.position.x - myPlayer.transform.position.x;
        y_dist = myTarget.transform.position.y - myPlayer.transform.position.y;
        //print(y_dist);

        float topScreen = Camera.main.transform.position.y + Camera.main.orthographicSize * Screen.height / Screen.width;
        topScreen += 1;
        float botScreen = Camera.main.transform.position.y - Camera.main.orthographicSize * Screen.height / Screen.width;
        botScreen -= 1;
        float leftScreen = Camera.main.transform.position.x + Camera.main.orthographicSize * Screen.width / Screen.height;
        leftScreen -= 2;
        float rightScreen = Camera.main.transform.position.x - Camera.main.orthographicSize * Screen.width / Screen.height;
        rightScreen += 2;

        if (Mathf.Abs(y_dist) + 4.5f > Mathf.Abs(x_dist)) {
            //print(y_dist);
            float xPos = x_dist / -2.0f;
            if (y_dist > 0) {
                transform.position = new Vector3(xPos, topScreen, 0);
            }
            else {
                transform.position = new Vector3(xPos, botScreen, 0);
            }
        }

        else {
            float yPos = y_dist / 2.0f;
            if (yPos > topScreen && y_dist > 0) {
                yPos = topScreen;
            }
            // else if (yPos < botScreen && y_dist < 0) {
            //     yPos = botScreen;
            // }

            if (x_dist > 0) {
                transform.position = new Vector3(leftScreen, yPos, 0);
            }
            else {
                transform.position = new Vector3(rightScreen, yPos, 0);
            }
        }
    }
}
