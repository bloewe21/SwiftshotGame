using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class DeathScript : MonoBehaviour
{
    [SerializeField] UnityEvent OnDeathEvent;
    //private GameObject[] players;
    public GameObject player;
    private bool amDead = false;

    [SerializeField] private AudioSource youdied_sound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (amDead) {
            return;
        }
        if (player.GetComponent<PlayerDeath>().isDead) {
            OnDeathEvent.Invoke();
            youdied_sound.Play();
            amDead = true;
        }
    }
}
