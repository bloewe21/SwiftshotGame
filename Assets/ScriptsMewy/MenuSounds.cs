using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSounds : MonoBehaviour
{
    [SerializeField] private AudioSource nature_sound;
    [SerializeField] private AudioSource bow_sound;
    [SerializeField] private AudioSource shoot_sound;
    // Start is called before the first frame update
    void Start()
    {
        bow_sound.Play();
        //nature_sound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void playShootSound() {
        shoot_sound.Play();
        //nature_sound.Stop();
    }
}
