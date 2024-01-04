using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    public bool isVisible;
    private Animator anim;
    private GameObject myIndicator;
    private GameObject player;
    private float myDistance;

    private float minScale = .3f;
    private float maxScale = 1.0f;
    private float alterDistance = 27.0f;

    [SerializeField] private AudioSource break_sound;
    // Start is called before the first frame update
    void Start()
    {
        isVisible = false;
        anim = GetComponent<Animator>();

        myIndicator = this.gameObject.transform.GetChild(0).gameObject;
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        myDistance = Vector3.Distance(player.transform.position, transform.position);

        if ((alterDistance / myDistance) < minScale) {
            myIndicator.transform.localScale = new Vector2(minScale, minScale);
        }
        else if (alterDistance / myDistance > maxScale) {
            myIndicator.transform.localScale = new Vector2(maxScale, maxScale);
        }
        else {
            myIndicator.transform.localScale = new Vector2(alterDistance / myDistance, alterDistance / myDistance);
        }
        //print(myIndicator.transform.localScale);
    }

    private void timeToDie()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<CircleCollider2D>().enabled = false;

        // if (collision.gameObject.tag == "Bullet") {
        //     anim.SetTrigger("break");
        //     collision.gameObject.GetComponent<Animator>().SetTrigger("break");
        //     collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        // }

        if (collision.gameObject.tag == "Melee") {
            anim.SetTrigger("break");
            break_sound.Play();
        }
        // if (collision.gameObject.tag == "Bullet") {
        //     break_sound.Play();
        // }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Bullet") {
            break_sound.Play();
        }
    }
}
