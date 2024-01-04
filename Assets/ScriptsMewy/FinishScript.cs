using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class FinishScript : MonoBehaviour
{
    static public string[] bestTimes = {"99:99:99", "99:99:99", "99:99:99", "99:99:99", "99:99:99", "99:99:99", "99:99:99", "99:99:99"};


    private GameObject[] targets;
    private GameObject[] points;
    [SerializeField] UnityEvent OnCompleteEvent1;
    [SerializeField] UnityEvent OnCompleteEvent2;

    public GameObject player;
    private Rigidbody2D playerRb;
    public GameObject timer;
    public Text finishTime;
    private string timeString;

    private int currLevelIndex;

    [SerializeField] private AudioSource finish_sound;
        
    // Start is called before the first frame update
    void Start()
    {
        targets = GameObject.FindGameObjectsWithTag("Target");
        playerRb = player.GetComponent<Rigidbody2D>();

        currLevelIndex = SceneManager.GetActiveScene().buildIndex - 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (targets.Length == 0) {
            FreezePlayer();
            return;
        }

        targets = GameObject.FindGameObjectsWithTag("Target");

        if (targets.Length == 0) {
            player.GetComponent<Animator>().SetTrigger("finish");

            finishTime.text = timer.GetComponent<TimerScript>().timeText.text;
            timeString = finishTime.text;


            //CALCULATE BEST TIME:
            string[] newTime = finishTime.text.Split(':');
            string minutes = newTime[0];
            string seconds = newTime[1];
            string milliseconds = newTime[2];
            string total = minutes + seconds + milliseconds;
            int intTotal = int.Parse(total);

            string[] oldTime = bestTimes[currLevelIndex].Split(':');
            string minutes2 = oldTime[0];
            string seconds2 = oldTime[1];
            string milliseconds2 = oldTime[2];
            string total2 = minutes2 + seconds2 + milliseconds2;
            int intTotal2 = int.Parse(total2);

            //if newTime faster than oldTime
            if (intTotal < intTotal2) {
                PlayerPrefs.SetString("BestTime"+currLevelIndex, finishTime.text);
                bestTimes[currLevelIndex] = finishTime.text;
            }






            points = GameObject.FindGameObjectsWithTag("Point");
            foreach (GameObject point in points) {
                point.SetActive(false);
            }
            OnCompleteEvent1.Invoke();
            finish_sound.Play();
            StartCoroutine(Event2());
        }

    }

    private IEnumerator Event2() {
        yield return new WaitForSeconds(1f);
        OnCompleteEvent2.Invoke();
    }

    private void FreezePlayer() {
        playerRb.velocity = new Vector2(0f, 0f);
        playerRb.gravityScale=0;
    }


}
