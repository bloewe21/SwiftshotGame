using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIndicator : MonoBehaviour
{

    public GameObject Indicator;
    public GameObject Player;

    private SpriteRenderer sr;
    private SpriteRenderer indicator_sr;

    

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        indicator_sr = Indicator.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if (sr.isVisible == false) {
            indicator_sr.enabled = true;

            Vector2 direction = Player.transform.position - transform.position;
            RaycastHit2D ray = Physics2D.Raycast(transform.position, direction, 10000, LayerMask.GetMask("CamBox"));

            if (ray.collider != null) {
                Indicator.transform.position = ray.point;
            }
        }

        else {
            indicator_sr.enabled = false;
        }
    }
}
