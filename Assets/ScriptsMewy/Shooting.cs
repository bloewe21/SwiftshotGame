using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Shooting : MonoBehaviour
{

    public GameObject bullet;
    private GameObject currAmmo;

    public Transform firepoint;
    public Transform firepoint2;

    public float baseSpeed = 1f;
    public Camera sceneCamera;
    private Vector2 mousePosition;
    public float fireForce;

    private PlayerMove pm;

    private float triangleDist;
    private float hypot;
    private float xflip = 0f;


    public GameObject point;
    GameObject[] points;
    public int numberOfPoints;
    public float spaceBetweenPoints;
    private Vector2 direction;

    public bool canShoot = true;
    public bool justShot = false;


    void Start() {
        pm = GetComponent<PlayerMove>();

        points = new GameObject[numberOfPoints];
        for (int i = 0; i < numberOfPoints; i++) {
            points[i] = Instantiate(point, firepoint.position, Quaternion.identity);
            points[i].GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    

    void Update() {
        if (pm.currWeapon != 0) {
            //print("!= 0");
            return;
        }
        if (canShoot == false) {
            //print("cantShoot");
            return;
        }
        Vector3 screenMousePos = Input.mousePosition;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(screenMousePos);

        if (Input.GetMouseButton(0)) {
            firepoint.GetComponent<SpriteRenderer>().enabled = true;

            // Vector3 screenMousePos = Input.mousePosition;
            // Vector2 mousePos = Camera.main.ScreenToWorldPoint(screenMousePos);

            hypot = Mathf.Sqrt(Mathf.Pow(firepoint.position.x - mousePos.x, 2) + Mathf.Pow(firepoint.position.y - mousePos.y, 2));
            //triangleDist = Mathf.Acos((firepoint.position.x - mousePos.x) / hypot) * Mathf.Rad2Deg;
            triangleDist = Mathf.Acos((mousePos.x - firepoint.position.x) / hypot) * Mathf.Rad2Deg;

            if (pm.facingRight) {
                firepoint.GetComponent<SpriteRenderer>().flipX = false;
                if (mousePos.y > firepoint.position.y) {
                    xflip = 0f;
                    firepoint.GetComponent<SpriteRenderer>().flipY = false;
                }
                else {
                    xflip = 180f;
                    firepoint.GetComponent<SpriteRenderer>().flipY = true;
                }
            }
            else {
                firepoint.GetComponent<SpriteRenderer>().flipX = true;
                if (mousePos.y > firepoint.position.y) {
                    xflip = 0f;
                    firepoint.GetComponent<SpriteRenderer>().flipY = true;
                }
                else {
                    xflip = 180f;
                    firepoint.GetComponent<SpriteRenderer>().flipY = false;
                }
            }

            Vector3 newRotation = new Vector3(xflip, 0, triangleDist);
            firepoint.transform.eulerAngles = newRotation;
        }
        else {
            firepoint.GetComponent<SpriteRenderer>().enabled = false;
        }




        fireForce = pm.chargeShot;
        //Vector3 screenMousePos = Input.mousePosition;
        // Turn that screen position into the actual position in the world.
        //Vector2 mousePos = Camera.main.ScreenToWorldPoint(screenMousePos);

        // Find out the direction between the player and the mouse pointer.
        direction = mousePos - (Vector2)firepoint.position;

        // Normalize the direction and multiply by bullet speed.
        direction.Normalize();
        direction *= fireForce;

        if (Input.GetMouseButton(0)) {
            for (int i = 0; i < numberOfPoints; i++) {
                points[i].transform.position = PointPosition(i * spaceBetweenPoints);
                points[i].GetComponent<SpriteRenderer>().enabled = true;
            }
        }
        if (Input.GetMouseButtonUp(0)) {
            for (int i = 0; i < numberOfPoints; i++) {
                points[i].GetComponent<SpriteRenderer>().enabled = false;
            }
        }
        points[0].GetComponent<SpriteRenderer>().enabled = false;
        
        
        
    }

    public void fire(float chargeShot)
    {
        fireForce = chargeShot;
        currAmmo = bullet;

        Vector3 screenMousePos = Input.mousePosition;

        // Turn that screen position into the actual position in the world.
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(screenMousePos);


        // Find out the direction between the player and the mouse pointer.
        direction = mousePos - (Vector2)firepoint.position;

        // Normalize the direction and multiply by bullet speed.
        direction.Normalize();
        direction *= fireForce;

        // Find the BulletScript prefab on that spawned bullet, and set it's velocity component.
        currAmmo.GetComponent<BulletScript>().Velocity = direction;
        currAmmo.GetComponent<BulletScript>().Velocity *= baseSpeed;

        // Calculate angle to bounce off ground, plats etc
        hypot = Mathf.Sqrt(Mathf.Pow(firepoint.position.x - mousePos.x, 2) + Mathf.Pow(firepoint.position.y - mousePos.y, 2));
        triangleDist = Mathf.Acos((firepoint.position.x - mousePos.x) / hypot) * Mathf.Rad2Deg;

        // Spawn the bullet from the prefab.
        // ONLY SPAWN IF CURRWEAPON = BOW
        if (pm.currWeapon == 0) {
            GameObject igbullet = Instantiate(currAmmo, firepoint2.position, firepoint.rotation);
            igbullet.GetComponent<Rigidbody2D>().velocity = direction * fireForce;

            justShot = true;
        }
        
    }

    private Vector2 PointPosition(float t) {
        Vector2 position = (Vector2)firepoint.position + (direction * fireForce * t) + 0.5f * Physics2D.gravity * (t * t);
        return position;
    }
}
