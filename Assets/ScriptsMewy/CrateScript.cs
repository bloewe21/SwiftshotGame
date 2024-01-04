using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateScript : MonoBehaviour
{
    private ParticleSystem particle;
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private BoxCollider2D coll;

    [SerializeField] private AudioSource breakSound;
    // Start is called before the first frame update
    void Start()
    {
        particle = GetComponent<ParticleSystem>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Melee") {
            sr.enabled = false;
            rb.simulated = false;
            coll.enabled = false;
            Break();
        }
    }

    private void Break() {
        breakSound.Play();
        particle.Play();
        StartCoroutine(timeToDie());
    }

    private IEnumerator timeToDie() {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
