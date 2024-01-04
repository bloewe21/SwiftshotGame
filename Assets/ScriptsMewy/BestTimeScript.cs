using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BestTimeScript : MonoBehaviour
{
    public int myArraySpot = 0;
    public GameObject bestTime;
    private TextMeshProUGUI timeText;

    private string textToDisplay;

    private int goldTotal;
    private int silverTotal;
    private int bronzeTotal;
    public GameObject goldStar;
    public GameObject silverStar;
    public GameObject bronzeStar;
    // Start is called before the first frame update
    void Start()
    {
        timeText = bestTime.gameObject.GetComponent<TextMeshProUGUI>();

        if (PlayerPrefs.HasKey("BestTime"+myArraySpot)) {
            FinishScript.bestTimes[myArraySpot] = PlayerPrefs.GetString("BestTime"+myArraySpot);
        }



        string[] goldTime = RankScript.goldTimes[myArraySpot].Split(':');
        string minutes2 = goldTime[0];
        string seconds2 = goldTime[1];
        string milliseconds2 = goldTime[2];
        string total2 = minutes2 + seconds2 + milliseconds2;
        goldTotal = int.Parse(total2);
        //print(goldTotal);

        string[] silverTime = RankScript.silverTimes[myArraySpot].Split(':');
        string minutes3 = silverTime[0];
        string seconds3 = silverTime[1];
        string milliseconds3 = silverTime[2];
        string total3 = minutes3 + seconds3 + milliseconds3;
        silverTotal = int.Parse(total3);
        //print(silverTotal);

        string[] bronzeTime = RankScript.bronzeTimes[myArraySpot].Split(':');
        string minutes4 = bronzeTime[0];
        string seconds4 = bronzeTime[1];
        string milliseconds4 = bronzeTime[2];
        string total4 = minutes4 + seconds4 + milliseconds4;
        bronzeTotal = int.Parse(total4);
        //print(bronzeTotal);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.HasKey("BestTime"+myArraySpot)) {
            textToDisplay = FinishScript.bestTimes[myArraySpot];
        }
        else {
            textToDisplay = "00:00:00";
        }
        //timeText.text = "Best Time: " + FinishScript.bestTimes[myArraySpot];
        timeText.text = "Best Time: " + textToDisplay;

        //print(timeText);
        string[] newTime = textToDisplay.Split(':');
        string minutes = newTime[0];
        string seconds = newTime[1];
        string milliseconds = newTime[2];
        string total = minutes + seconds + milliseconds;
        int myTotal = int.Parse(total);

        //print(intTotal);

        if (myTotal < bronzeTotal && myTotal > 0) {
            bronzeStar.SetActive(true);
            silverStar.SetActive(false);
            goldStar.SetActive(false);
        }
        if (myTotal < silverTotal && myTotal > 0) {
            bronzeStar.SetActive(false);
            silverStar.SetActive(true);
            goldStar.SetActive(false);
        }
        if (myTotal < goldTotal && myTotal > 0) {
            bronzeStar.SetActive(false);
            silverStar.SetActive(false);
            goldStar.SetActive(true);
        }
    }
}
