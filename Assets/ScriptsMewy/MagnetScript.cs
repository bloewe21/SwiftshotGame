using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetScript : MonoBehaviour
{
    private bool updating = false;
    public float returnForce;
    private float constantAngle;

    public Vector3 lastVelocity;

    // The velocity of the bullet.
    public Vector3 Velocity;

    private Rigidbody2D rb;

    private GameObject player;
    private Rigidbody2D prb;

    static public bool magnetHit = false;


    [SerializeField] private AudioSource magnet_shoot;
    [SerializeField] private AudioSource magnet_attach;
    [SerializeField] private AudioSource magnet_fail;

    private float xForce;
    private float yForce;
    //private bool addingForce = false;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        prb = player.GetComponent<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();
        constantAngle = gameObject.transform.localRotation.eulerAngles.z;

        magnet_shoot.Play();

        StartCoroutine(threeSeconds());
    }

    void Update()
    {
        
        if (updating == false) {
            return;
        }
        transform.eulerAngles = new Vector3(0, 0, -constantAngle);
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, player.transform.position, (returnForce + Mathf.Abs(prb.velocity.magnitude)) * Time.deltaTime);

        if (gameObject.transform.position == player.transform.position) {
            timeToDie();
        }
    }

    private void FixedUpdate() {
        // //print(addingForce);
        // if (addingForce) {
        //     print(xForce);
        //     print(yForce);
        //     player.GetComponent<Rigidbody2D>().AddForce(new Vector2(xForce, yForce), ForceMode2D.Impulse);
        //     StartCoroutine(addingForceRoutine());
        // }
    }

    // private IEnumerator addingForceRoutine() {
    //     yield return new WaitForSeconds(.02f);
    //     addingForce = false;
    // }



    private void OnCollisionEnter2D(Collision2D collision)
    {

        Vector3 dir = player.transform.position - transform.position;
        dir.x = dir.x * -1;
        dir.y = dir.y * -1;
        //float angleDeg = Mathf.Atan2(dir.y, dir.x)  * Mathf.Rad2Deg;
        float angle = Mathf.Atan2(dir.y, dir.x);

        //float xForce = 20.0f * Mathf.Cos(angle);
        //float yForce = 22.0f * Mathf.Sin(angle);
        xForce = 20.0f * Mathf.Cos(angle);
        yForce = 22.0f * Mathf.Sin(angle);

        //in build:
        xForce = xForce * 6.0f;
        // if (Mathf.Abs(player.GetComponent<Rigidbody2D>().velocity.x) < .001f) {
        //     //player.GetComponent<Rigidbody2D>().velocity = new Vector2(.5f, player.GetComponent<Rigidbody2D>().velocity.y);
        //     xForce = xForce * 2.0f;
        //     player.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, player.GetComponent<Rigidbody2D>().velocity.y);
        //     print("a");
        // }
        // if (Mathf.Abs(player.GetComponent<Rigidbody2D>().velocity.x) > .001f) {
        //     xForce = xForce / (1.0f + (20.0f / 6.0f));
        //     print("b");
        // }
        xForce = xForce / (1.0f + (20.0f / 6.0f));
        // xForce = xForce / (1.0f + (20.0f / 6.0f));
        // xForce = xForce + (13.0f - Mathf.Abs(player.GetComponent<Rigidbody2D>().velocity.x));

        

        if (collision.gameObject.tag == "Hook") {
            //print(player.GetComponent<Rigidbody2D>().velocity.x);
            //print(xForce);
            player.GetComponent<PlayerMove>().hasDoubled = false;

            //prb.velocity = new Vector2(xForce, 0);
            // if (player.GetComponent<Rigidbody2D>().velocity.x < .00001f) {
            //     player.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, player.GetComponent<Rigidbody2D>().velocity.y);
            // }
            if (Mathf.Abs(player.GetComponent<Rigidbody2D>().velocity.x) == 0.0f) {
                //player.GetComponent<PlayerMove>().enabled = false;
                // prb.velocity = new Vector2(0, 0);
                // prb.velocity = new Vector2(200f * Mathf.Sign(xForce), 0);
                //player.GetComponent<Rigidbody2D>().AddForce(new Vector2(220f * Mathf.Sign(xForce), yForce), ForceMode2D.Impulse);
                //player.GetComponent<PlayerMove>().enabled = true;
                prb.velocity = new Vector2(xForce, 0);
                //print("a");
            }
            else {
                prb.velocity = new Vector2(xForce, 0);
                //player.GetComponent<Rigidbody2D>().AddForce(new Vector2(xForce, yForce), ForceMode2D.Impulse);
                //print("b");
            }
            player.GetComponent<Rigidbody2D>().AddForce(new Vector2(xForce, yForce), ForceMode2D.Impulse);
            //player.GetComponent<Rigidbody2D>().AddForce(new Vector2(xForce, yForce), ForceMode2D.Impulse);
            //player.GetComponent<Rigidbody2D>().AddForce(new Vector2(xForce, yForce), ForceMode2D.Force);

            //addingForce = true;


            player.GetComponent<PlayerMove>().CreateDust();

            magnetHit = true;

            timeToDie();
            //StartCoroutine(attachDeath());
        }
        else {
            updating = true;

            magnet_fail.Play();
        }
        
        GetComponent<BoxCollider2D>().enabled = false;
        this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }
    
    // Causes bullet explosion 3 seconds after start
    private IEnumerator threeSeconds()
    {
        yield return new WaitForSeconds(.8f);
        if (!updating) {
            GetComponent<BoxCollider2D>().enabled = false;
            this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            updating = true;
        }
    }

    // Called at end of explosion animation
    private void timeToDie()
    {
        Destroy(gameObject);
    }

    private IEnumerator attachDeath() {
        yield return new WaitForSeconds(.05f);
        Destroy(gameObject);
    }
}
