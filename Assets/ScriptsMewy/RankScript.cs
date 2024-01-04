using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RankScript : MonoBehaviour
{

    private int index;
    static public string[] goldTimes = {"00:19:00", "00:18:00", "00:21:00", "00:34:00", "00:32:00", "00:34:00", "00:44:00", "00:20:00"};
    static public string[] silverTimes = {"00:25:00", "00:24:00", "00:28:00", "00:42:00", "00:40:00", "00:42:00", "00:54:00", "00:26:00"};
    static public string[] bronzeTimes = {"00:33:00", "00:32:00", "00:38:00", "00:54:00", "00:50:00", "00:52:00", "01:05:00", "00:34:00"};

    public GameObject goldText;
    public GameObject silverText;
    public GameObject bronzeText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // index = LevelSelect.currLevel;
        index = LevelSelect.currCol + ((LevelSelect.currRow - 1) * 4);
        //print(index);

        goldText.GetComponent<TMPro.TextMeshProUGUI>().text = goldTimes[index-1];
        silverText.GetComponent<TMPro.TextMeshProUGUI>().text = silverTimes[index-1];
        bronzeText.GetComponent<TMPro.TextMeshProUGUI>().text = bronzeTimes[index-1];
        
    }
}
