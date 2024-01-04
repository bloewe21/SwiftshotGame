using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSounds : MonoBehaviour
{
    [SerializeField] private AudioSource button_sound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneScript.buttonClicked && !button_sound.isPlaying) {
            // button_sound.Play();
            // SceneScript.buttonClicked = false;
            playButtonSound();
        }
    }

    public void playButtonSound() {
        button_sound.Play();
        SceneScript.buttonClicked = false;
    }
}
