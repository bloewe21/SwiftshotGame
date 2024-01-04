using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class SceneScript : MonoBehaviour
{
    public GameObject pauseMenu;
    [SerializeField] UnityEvent OnLoadingEvent;
    [SerializeField] UnityEvent OnUnpauseEvent;

    [SerializeField] UnityEvent OnSettingsEvent;
    [SerializeField] UnityEvent OffSettingsEvent;

    [SerializeField] UnityEvent OnControlsEvent;

    static public bool buttonClicked = false;
    //[SerializeField] UnityEvent OffControlsEvent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Retry() {
        //buttonClicked = true;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ToLevelSelect() {
        buttonClicked = true;
        Time.timeScale = 1;
        StartCoroutine(LevelRoutine());
    }

    public void QuitGame() {
        buttonClicked = true;
        Time.timeScale = 1;
        Application.Quit();
    }

    public void Resume() {
        buttonClicked = true;
        pauseMenu.GetComponent<PauseScript>().isPaused = false;
        OnUnpauseEvent.Invoke();
        Time.timeScale = 1;

        AudioSource[] audios = FindObjectsOfType<AudioSource>();
        foreach (AudioSource a in audios) {
            a.UnPause();
        }
    }

    public void ToSettings() {
        buttonClicked = true;
        OnSettingsEvent.Invoke();
    }

    public void LeaveSettings() {
        buttonClicked = true;
        OffSettingsEvent.Invoke();
    }

    public void ToMenu() {
        buttonClicked = true;
        StartCoroutine(MenuRoutine());
    }

    public void ToControls() {
        buttonClicked = true;
        OnControlsEvent.Invoke();
    }

    public void ResetTime() {
        //buttonClicked = true;
        if (PlayerPrefs.HasKey("BestTime"+(LevelSelect.currLevel-1))) {
            PlayerPrefs.DeleteKey("BestTime"+(LevelSelect.currLevel-1));
        }
        FinishScript.bestTimes[LevelSelect.currLevel-1] = "99:99:99";
    }

    private IEnumerator LevelRoutine() {
        OnLoadingEvent.Invoke();
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("LevelSelect");
    }

    private IEnumerator MenuRoutine() {
        OnLoadingEvent.Invoke();
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("MainMenu");
    }
}
