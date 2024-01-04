using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoScript : MonoBehaviour
{
    public int ammoCount;
    public int refreshAmmoCount;
    public bool refresh = false;
    public GameObject player;
    private PlayerMove pm;
    private Shooting sh;

    private bool playedSound = false;
    [SerializeField] private AudioSource noammo_sound;

    public GameObject noAmmoText;
    // Start is called before the first frame update
    void Start()
    {
        refreshAmmoCount = ammoCount;
        pm = player.GetComponent<PlayerMove>();
        sh = player.GetComponent<Shooting>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        noAmmoText.transform.position = new Vector2(player.transform.position.x, player.transform.position.y + .4f);
        if (ammoCount >= 1) {
            playedSound = false;
        }
        if (ammoCount <= 0) {
            //player.GetComponent<Shooting>().enabled = false;
            //player.GetComponent<Shooting>().canShoot = false;
            pm.canSwitch = false;
            pm.currWeapon = 1;
            pm.highlight1.enabled = false;
            pm.highlight2.enabled = true;
            pm.cursor1.enabled = false;
            pm.cursor2.enabled = true;

            if (!playedSound) {
                noammo_sound.Play();
                playedSound = true;
                StartCoroutine(noAmmoRoutine());
            }
            return;
        }
        else if (refresh == true) {
            pm.canSwitch = true;
            refresh = false;
        }

        if (sh.justShot == true) {
            sh.justShot = false;
            ammoCount -= 1;
        }

        GetComponent<TMPro.TextMeshProUGUI>().text = "Ammo: "+ammoCount;
    }

    private IEnumerator noAmmoRoutine() {
        noAmmoText.SetActive(true);
        yield return new WaitForSeconds(1f);
        noAmmoText.SetActive(false);
    }
}
