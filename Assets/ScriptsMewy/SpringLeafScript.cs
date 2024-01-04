using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringLeafScript : MonoBehaviour
{
    public GameObject player;
    private BoxCollider2D playerColl;
    private PlayerMove pm;
    [SerializeField] private LayerMask jumpableGround;

    private BoxCollider2D coll;
    private Animator anim;

    private bool updating = false;
    private bool oneTime = false;
    public float springForce;
    private float ogTransformY;
    private float ogJumpSpeed;

    [SerializeField] private AudioSource leaf_sound;
    // Start is called before the first frame update
    void Start()
    {
        playerColl = player.GetComponent<BoxCollider2D>();
        pm = player.GetComponent<PlayerMove>();

        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();

        ogTransformY = transform.position.y;
        ogJumpSpeed = pm.jumpSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (updating) {
            anim.SetTrigger("on");
            oneTime = true;
            if (transform.position.y > (ogTransformY - 0.25f)) {
                transform.position = new Vector2(transform.position.x, transform.position.y - (Time.deltaTime * .2f));
                pm.jumpSpeed += Time.deltaTime * springForce;
            }
        }

        else {
            anim.SetTrigger("off");
            if (oneTime) {
                //anim.SetTrigger("off");
                transform.position = new Vector2(transform.position.x, ogTransformY);
                pm.jumpSpeed = ogJumpSpeed;
                oneTime = false;

                leaf_sound.Play();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            if (IsGrounded() && (player.transform.position.y - transform.position.y > 0.5f)) {
                updating = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            if (!IsGrounded()) {
                updating = false;
            }
            //print("exit");
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(playerColl.bounds.center, playerColl.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
