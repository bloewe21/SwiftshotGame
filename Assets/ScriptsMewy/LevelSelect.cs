using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class LevelSelect : MonoBehaviour
{
    private bool loading = false;


    private float levelCount = 8;
    static public int currLevel = 1;
    static public int currRow = 1;
    static public int currCol = 1;
    private int rowAmount = 2;
    private int colAmount = 4;

    private int[,] numbers = new int[2,4] { { 1, 2, 3, 4}, { 5, 6, 7, 8} };

    public GameObject player;
    [SerializeField] private GameObject[] levels;

    [SerializeField] UnityEvent OnLoadingEvent;

    [SerializeField] private AudioSource select_sound;

    public GameObject buttonSounds;
    private ButtonSounds bs;
    // Start is called before the first frame update
    void Start()
    {
        bs = buttonSounds.GetComponent<ButtonSounds>();
    }

    // Update is called once per frame
    void Update()
    {
        if (loading) {
            return;
        }
        // print(TimerScript.bestTimes[2]);

        if (Input.GetKeyDown(KeyCode.Space)) {
            currLevel = numbers[currRow-1, currCol-1];
            player.GetComponent<Animator>().SetTrigger("select");
            loading = true;

            select_sound.Play();

            StartCoroutine(LoadLevel());
        }

        if (Input.GetKeyDown(KeyCode.D) && currCol+1 <= colAmount) {
            currCol += 1;
            bs.playButtonSound();
        }

        if (Input.GetKeyDown(KeyCode.A) && currCol-1 >= 1) {
            currCol -= 1;
            bs.playButtonSound();
        }

        if (Input.GetKeyDown(KeyCode.S) && currRow+1 <= rowAmount) {
            currRow += 1;
            bs.playButtonSound();
        }

        if (Input.GetKeyDown(KeyCode.W) && currRow-1 >= 1) {
            currRow -= 1;
            bs.playButtonSound();
        }

        for (int i=0; i < levelCount; i++) {
            if (i == numbers[currRow-1,currCol-1]-1) {
                levels[i].transform.GetChild(2).gameObject.SetActive(true);
                levels[i].transform.GetChild(4).gameObject.SetActive(true);
                player.transform.position = levels[i].transform.GetChild(1).gameObject.transform.position;
            }
            else {
                levels[i].transform.GetChild(2).gameObject.SetActive(false);
                levels[i].transform.GetChild(4).gameObject.SetActive(false);
            }
        }


    }

    private IEnumerator LoadLevel() {
        OnLoadingEvent.Invoke();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(currLevel);
        //SceneManager.LoadScene("TutorialLevel");
    }
}
