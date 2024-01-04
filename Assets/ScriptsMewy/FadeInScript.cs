using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInScript : MonoBehaviour
{
    private SpriteRenderer sr;
    private bool fadeOut = true;
    public float fadeSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeOut) {
            Color thisColor = sr.material.color;
            float fadeAmount = thisColor.a - (fadeSpeed * Time.deltaTime);
            sr.material.color = new Color(thisColor.r, thisColor.g, thisColor.b, fadeAmount);

            if (thisColor.a < 0) {
                fadeOut = false;
            }
        }
    }
}
