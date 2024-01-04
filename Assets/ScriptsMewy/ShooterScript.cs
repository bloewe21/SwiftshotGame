using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterScript : MonoBehaviour
{
    public GameObject firepoint;
    public GameObject bullet;
    public float fireForce;

    [SerializeField] private AudioSource fire_sound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        //ShootBullet();
    }

    public void ShootBullet() {
        Vector2 direction = firepoint.transform.position - transform.position;
        GameObject igbullet = Instantiate(bullet, transform.position, transform.rotation);
        igbullet.GetComponent<Rigidbody2D>().velocity = direction * fireForce;

        fire_sound.Play();
    }

    
}
