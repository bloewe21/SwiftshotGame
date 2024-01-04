using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingMagnet : MonoBehaviour
{

    public GameObject bullet;

    private GameObject currAmmo;

    public Transform firepoint;

    public float baseSpeed = 1f;

    public Camera sceneCamera;

    private Vector2 mousePosition;

    public float fireForce;


    private float triangleDist;
    private float hypot;
    private float xflip = 0f;


    void Start() {
    }

    public void fire()
    {
        //fireForce = chargeShot;
        currAmmo = bullet;

        Vector3 screenMousePos = Input.mousePosition;

        // Turn that screen position into the actual position in the world.
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(screenMousePos);


        // Find out the direction between the player and the mouse pointer.
        Vector2 direction = mousePos - (Vector2)firepoint.position;

        // Normalize the direction and multiply by bullet speed.
        direction.Normalize();
        direction *= fireForce;

        // Find the BulletScript prefab on that spawned bullet, and set it's velocity component.
        currAmmo.GetComponent<MagnetScript>().Velocity = direction;
        currAmmo.GetComponent<MagnetScript>().Velocity *= baseSpeed;

        // Calculate angle to bounce off ground, plats etc
        hypot = Mathf.Sqrt(Mathf.Pow(firepoint.position.x - mousePos.x, 2) + Mathf.Pow(firepoint.position.y - mousePos.y, 2));
        triangleDist = Mathf.Acos((firepoint.position.x - mousePos.x) / hypot) * Mathf.Rad2Deg;

        if (mousePos.y > firepoint.position.y)
        {
            xflip = 180f;
        }
        else
        {
            xflip = 0f;
        }
        Vector3 newRotation = new Vector3(xflip, 0, triangleDist);

        firepoint.transform.eulerAngles = newRotation;

        // Spawn the bullet from the prefab.
        GameObject igbullet = Instantiate(currAmmo, firepoint.position, firepoint.rotation);
        igbullet.GetComponent<Rigidbody2D>().velocity = direction * fireForce;
        
    }
}
