using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public float fadeSpeed = 5f;
    [SerializeField] UnityEvent AnimationEndEvent;
    public GameObject whiteFlash;

    private bool fadeOut = false;

    private SpriteRenderer sr;
    private SpriteRenderer whiteSr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        whiteSr = whiteFlash.gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeOut) {
            Color thisColor = whiteSr.material.color;
            float fadeAmount = thisColor.a - (fadeSpeed * Time.deltaTime);
            whiteSr.material.color = new Color(thisColor.r, thisColor.g, thisColor.b, fadeAmount);

            if (thisColor.a < 0) {
                fadeOut = false;
            }
        }
    }

    public void AnimEnd() {
        sr.enabled = false;
        whiteSr.enabled = true;
        fadeOut = true;
        AnimationEndEvent.Invoke();
    }
}
