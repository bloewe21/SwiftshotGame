using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScript : MonoBehaviour
{
    public BoxCollider2D triggerBox;
    public BoxCollider2D collisionBox;
    private Animator anim;
    private bool canTrigger = true;

    [SerializeField] private AudioSource activate_sound;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (canTrigger) {
            anim.SetTrigger("activate");
            canTrigger = false;
        }
        
    }

    public void  AcivateColl() {
        collisionBox.enabled = true;
        
        activate_sound.Play();
    }

    public void  DeacivateColl() {
        anim.SetTrigger("deactivate");
        collisionBox.enabled = false;
        canTrigger = true;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        
    }
}
