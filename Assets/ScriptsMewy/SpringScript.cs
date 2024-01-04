using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringScript : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D prb;
    private Animator anim;
    public float springForceY = 19.0f;
    public float springForceX = 80.0f;

    //0 = UP, 1 = RIGHT, 2 = DOWN, 3 = LEFT
    public int direction = 0;

    [SerializeField] private AudioSource spring_sound;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        prb = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            //prb.velocity = new Vector2(prb.velocity.x, 0.0f);
            //UP
            if (direction == 0) {
                prb.velocity = new Vector2(prb.velocity.x, 0.0f);
                prb.AddForce(new Vector2(0, springForceY), ForceMode2D.Impulse);
            }
            //RIGHT
            else if (direction == 1) {
                // //prb.velocity = new Vector2(0.0f, 0.0f);
                if (Mathf.Abs(prb.velocity.x) < .1f) {
                    prb.velocity = new Vector2(0.0f, 0.0f);
                    prb.velocity = new Vector2(springForceX * 6.0f, 0.0f);
                }
                else {
                    prb.velocity = new Vector2(0.0f, 0.0f);
                    prb.velocity = new Vector2(springForceX - Mathf.Abs(prb.velocity.x), 0.0f);
                    print("y");
                }

                //prb.velocity = new Vector2(0.0f, 0.0f);
                //prb.velocity = new Vector2(springForceX - Mathf.Abs(prb.velocity.x), 0.0f);

                //prb.AddForce(new Vector2(springForceX - Mathf.Abs(prb.velocity.x), 0.0f), ForceMode2D.Impulse);
            }
            //DOWN
            else if (direction == 2) {
                prb.velocity = new Vector2(prb.velocity.x, 0.0f);
                prb.AddForce(new Vector2(0, -springForceY), ForceMode2D.Impulse);
            }
            //LEFT
            else if (direction == 3) {
                prb.velocity = new Vector2(0.0f, 0.0f);
                prb.velocity = new Vector2(-springForceX + Mathf.Abs(prb.velocity.x), 0.0f);
            }
            //prb.AddForce(new Vector2(0, springForce), ForceMode2D.Impulse);
            player.GetComponent<PlayerMove>().hasDoubled = false;
            anim.SetTrigger("bounce");

            spring_sound.Play();
        }
    }

    private void returnToIdle() {
        anim.SetTrigger("idle");
    }
}
