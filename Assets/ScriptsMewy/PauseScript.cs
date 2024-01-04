using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    //public GameObject myButton;
    public bool isPaused = false;
    static public bool canPause = true;
    //public GameObject shader;

    [SerializeField] UnityEvent OnPauseEvent;
    [SerializeField] UnityEvent OnUnpauseEvent;
    // Start is called before the first frame update
    void Start()
    {
        canPause = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canPause) {
            return;
        }
        //extra if because of bug during shooting
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused && !Input.GetMouseButton(0)) {
            isPaused = true;
            OnPauseEvent.Invoke();
            Time.timeScale = 0;

            AudioSource[] audios = FindObjectsOfType<AudioSource>();
            foreach (AudioSource a in audios) {
                if (a.gameObject.tag == "Music") {
                    a.volume /= 2;
                }
                else {
                    a.Pause();
                }
            }
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused) {
            isPaused = false;
            OnUnpauseEvent.Invoke();
            Time.timeScale = 1;

            AudioSource[] audios = FindObjectsOfType<AudioSource>();
            foreach (AudioSource a in audios) {
                if (a.gameObject.tag == "Music") {
                    a.volume *= 2;
                }
                else {
                    a.UnPause();
                }
            }
        }
    }

    // public void Resume() {
    //     // isPaused = false;
    //     // OnUnpauseEvent.Invoke();
    //     // Time.timeScale = 1;
    // }
}
