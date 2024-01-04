using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeBar : MonoBehaviour
{

    public GameObject player;
    public PlayerMove pm;
    public Image fillImage;
    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pm.enabled == false) {
            return;
        }
        if (pm.currWeapon == 0 && Input.GetMouseButton(0)) {
            foreach(Transform child in transform) {
                child.gameObject.SetActive(true);
            }
        }
        else {
            foreach(Transform child in transform) {
                child.gameObject.SetActive(false);
            }
        }

        gameObject.transform.position = new Vector2(player.transform.position.x + .1f, player.transform.position.y + 1.0f);
        if (pm.currWeapon != 0) {
            return;
        }

        
        if (slider.value <= slider.minValue) {
            fillImage.enabled = false;
        }
        else {
            fillImage.enabled = true;
        }

        float fillValue = pm.chargeShot;
        slider.value = fillValue;
    }
}
