using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHook : MonoBehaviour
{

    public GameObject hookPrefab;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(1)) {
            //print(currDirX);
            Instantiate(hookPrefab, transform.position, transform.rotation);
        }
    }
}
