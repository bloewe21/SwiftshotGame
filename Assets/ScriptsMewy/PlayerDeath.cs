using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Animator anim;
    private PlayerMove pm;
    private Shooting sh;
    private ShootingMagnet shm;
    private PlayerMelee pmelee;
    public GameObject playerhand;
    public GameObject firebar;
    private GameObject[] points;

    public bool isDead = false;

    [SerializeField] private AudioSource hurt_sound;
    [SerializeField] private AudioSource death_sound;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();

        pm = GetComponent<PlayerMove>();
        sh = GetComponent<Shooting>();
        shm = GetComponent<ShootingMagnet>();
        pmelee = GetComponent<PlayerMelee>();
    }

    // Update is called once per frame
    void Update()
    {
        //debug purpose
        if (Input.GetKeyDown(KeyCode.R) && Time.timeScale == 1) {
            //StartCoroutine(playDeath());
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Spikes") {
            hurt_sound.Play();
            StartCoroutine(playDeath());
        }
    }

    private IEnumerator playDeath()
    {
        points = GameObject.FindGameObjectsWithTag("Point");
        foreach (GameObject point in points) {
            point.SetActive(false);
        }

        playerhand.SetActive(false);
        firebar.SetActive(false);


        coll.enabled = false;
        float deathForce = -10.0f;
        if (!pm.facingRight) {
            deathForce = 10.0f;
        }
        rb.gravityScale = 0.0f;
        rb.velocity = new Vector2(0, 0);
        rb.AddForce(new Vector2(deathForce, 0), ForceMode2D.Impulse);
        anim.SetTrigger("death");
        pm.enabled = false;
        sh.enabled = false;
        shm.enabled = false;
        pmelee.enabled = false;
        yield return new WaitForSeconds(0.3f);
        //rb.AddForce(new Vector2(-deathForce, 0), ForceMode2D.Impulse);
        rb.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(1.0f);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //isDead = true;
    }

    private void isDeadMakeTrue() {
        isDead = true;
        PauseScript.canPause = false;
    }

    private void playDeathSound() {
        death_sound.Play();
    }
}
