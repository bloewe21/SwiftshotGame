using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookScript : MonoBehaviour
{

    [SerializeField] public float moveSpeedX;
    [SerializeField] public float moveSpeedY;

    private bool goRight;

    private float frames;

    private PlayerMove pm;

    // Start is called before the first frame update
    void Start()
    {
        pm = GameObject.FindWithTag("Player").GetComponent<PlayerMove>();
        if (!pm.facingRight) {
            moveSpeedX = moveSpeedX * -1.0f;
            moveSpeedY = moveSpeedY * -1.0f;
        }
        
        if (Input.GetKey(KeyCode.W)) {
            moveSpeedY = 40.0f;
            moveSpeedX = 0.0f;
            //print("sup");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(moveSpeedX, moveSpeedY, 0.0f) * Time.deltaTime;
    }

    void FixedUpdate() {
        if (frames % 10 == 0) {
            moveSpeedX -= moveSpeedX - (moveSpeedX / 2);
            moveSpeedY -= moveSpeedY - (moveSpeedY / 2);
        }
        
        frames++;
    }
}
