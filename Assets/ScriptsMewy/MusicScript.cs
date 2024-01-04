using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
    public bool keepMusic = false;
    public float musicVolume = .2f;
    public bool isMainMenu = false;
    public AudioSource _audioSource;

    private void Start()
    {
        //_audioSource = GetComponent<AudioSource>();

        if (isMainMenu) {
            return;
        }

        if (!keepMusic) {
            GameObject.FindGameObjectWithTag("Music").GetComponent<MusicScript>().StopMusic();
            PlayMusic();
        }
        else {
            GameObject.FindGameObjectWithTag("Music").GetComponent<MusicScript>().PlayMusic();
            //_audioSource.volume = .2f;

            GameObject[] musics = GameObject.FindGameObjectsWithTag("Music");
            foreach (GameObject m in musics) {
                m.GetComponent<AudioSource>().volume = musicVolume;
            }

            
            GameObject[] musics2 = GameObject.FindGameObjectsWithTag("Music");
            if (musics2.Length > 1) {
                for (int i=0; i<musics2.Length; i++) {
                    if (i > 0) {
                        Destroy(musics2[i]);
                    }
                }
            }
        }
        
    }
    private void Awake()
    {
        if (isMainMenu) {
            return;
        }

        if (keepMusic) {
            DontDestroyOnLoad(transform.gameObject);
        }
        //DontDestroyOnLoad(transform.gameObject);
        //_audioSource = GetComponent<AudioSource>();
    }

    public void PlayMusic()
    {
        if (_audioSource.isPlaying) return;
        _audioSource.Play();
    }

    public void StopMusic()
    {
        _audioSource.Stop();
    }

    public void PlayMainMenuMusic() {
        //_audioSource.Play();
    }
}
