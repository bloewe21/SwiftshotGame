using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    public bool canMelee = true;
    private Animator anim;
    private PlayerMove pm;
    private Shooting shooting;

    [SerializeField] private AudioSource melee_sound;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        pm = GetComponent<PlayerMove>();
        shooting = GetComponent<Shooting>();
    }

    // Update is called once per frame
    void Update()
    {
        //print(canMelee);
        Melee();

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Player_Melee") && 
        !anim.GetCurrentAnimatorStateInfo(0).IsName("Player_Melee_Walk") &&
        !anim.GetCurrentAnimatorStateInfo(0).IsName("Player_Melee_Jump")) {
            this.gameObject.transform.GetChild(4).gameObject.SetActive(false);
        }
    }

    private void Melee() {
        if (Input.GetMouseButtonDown(0) && canMelee && pm.currWeapon == 1 && !pm.isWallSliding) {
            //shooting.canShoot = false;
            this.gameObject.transform.GetChild(4).gameObject.SetActive(false);
            //MeleeStart();
        }
    }

    private void MeleeStart() {
        this.gameObject.transform.GetChild(4).gameObject.SetActive(false);
        canMelee = false;
        //anim.SetTrigger("melee");
        anim.SetInteger("state", 7);

        melee_sound.Play();
    }

    private void MeleeEnd() {
        //shooting.canShoot = true;
        canMelee = true;
        anim.SetTrigger("meleeEnd");
    }

    private void MeleeHitbox() {
        this.gameObject.transform.GetChild(4).gameObject.SetActive(true);
    }

    private void MeleeHitboxEnd() {
        this.gameObject.transform.GetChild(4).gameObject.SetActive(false);
    }
}
