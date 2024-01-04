using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookSounds : MonoBehaviour
{
    private bool didMagnetHit = false;
    [SerializeField] private AudioSource magnet_attach;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (MagnetScript.magnetHit) {
            didMagnetHit = true;
        }
        if (didMagnetHit && !magnet_attach.isPlaying) {
            magnet_attach.Play();
            didMagnetHit = false;
            MagnetScript.magnetHit = false;
        }
    }
}
