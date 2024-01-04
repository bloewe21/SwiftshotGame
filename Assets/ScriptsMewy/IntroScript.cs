using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class IntroScript : MonoBehaviour
{
    [SerializeField] UnityEvent OnCompleteEvent;
    [SerializeField] UnityEvent OnCompleteEvent2;
    private float timePassed;

    private GameObject slider;
    private bool hasInvoked = false;

    [SerializeField] private AudioSource start_sound;
    //OnCompleteEvent.Invoke();
    // Start is called before the first frame update
    void Start()
    {
        slider = this.gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;

        if (slider.GetComponent<Slider>().value < slider.GetComponent<Slider>().maxValue) {
            slider.GetComponent<Slider>().value = timePassed;
        }
        else if (hasInvoked == false) {
            OnCompleteEvent.Invoke();
            start_sound.Play();
            StartCoroutine(Event2());
            hasInvoked = true;
        }
        
    }



    private IEnumerator Event2() {
        yield return new WaitForSeconds(0.5f);
        OnCompleteEvent2.Invoke();

    }
}
