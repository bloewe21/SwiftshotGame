using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public Vector3 lastVelocity;

    // The velocity of the bullet.
    public Vector3 Velocity;

    private Rigidbody2D rb;

    private Animator anim;

    private bool collided = false;


    [SerializeField] private AudioSource shoot_sound;
    [SerializeField] private AudioSource hit_sound;

    void Start()
    {
        StartCoroutine(threeSeconds());
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        shoot_sound.Play();
    }

    void Update()
    {
        if (!collided) {
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else {
            this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }

    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<BoxCollider2D>().enabled = false;
        collided = true;

        // hit_sound.Play();
        StartCoroutine(soundRoutine());

        if (collision.gameObject.tag == "Target") {
            hit_sound.Stop();
            anim.SetTrigger("break");
            collision.gameObject.GetComponent<Animator>().SetTrigger("break");
        }
    }

    private IEnumerator soundRoutine() {
        hit_sound.Play();
        yield return new WaitForSeconds(.1f);
        hit_sound.Stop();
    }
    

    
    // Causes bullet explosion 3 seconds after start
    private IEnumerator threeSeconds()
    {
        yield return new WaitForSeconds(3f);
        timeToDie();
    }

    // Called at end of explosion animation
    private void timeToDie()
    {
        Destroy(gameObject);
    }
}
