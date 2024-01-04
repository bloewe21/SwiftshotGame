using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    private Animator anim;
    private BoxCollider2D coll;
    private GameObject door;
    private Animator doorAnim;
    private BoxCollider2D doorColl;

    [SerializeField] private AudioSource open_sound;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        door = this.gameObject.transform.GetChild(0).gameObject;
        doorAnim = door.GetComponent<Animator>();
        doorColl = door.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Bullet") {
            anim.SetTrigger("pull");
            coll.enabled = false;
            doorAnim.SetTrigger("pull");
            doorColl.enabled = false;

            open_sound.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Melee") {
            anim.SetTrigger("pull");
            coll.enabled = false;
            doorAnim.SetTrigger("pull");
            doorColl.enabled = false;

            open_sound.Play();
        }
    }
}
