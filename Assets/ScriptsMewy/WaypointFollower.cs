using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentIndex = 0;

    [SerializeField] private float speed = 2f;

    [SerializeField] private bool doPauses = false;
    [SerializeField] private float pauseTime = 1f;
    private bool resume = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (Vector2.Distance(transform.position, waypoints[currentIndex].transform.position) < .1f) {
            currentIndex += 1;
            if (doPauses) {
                StartCoroutine(PauseRoutine());
            }
            if (currentIndex >= waypoints.Length) {
                currentIndex = 0;
            }
        }

        if (resume) {
            transform.position = Vector2.MoveTowards(transform.position, waypoints[currentIndex].transform.position, Time.deltaTime * speed);
        }
        //transform.position = Vector2.MoveTowards(transform.position, waypoints[currentIndex].transform.position, Time.deltaTime * speed);
    }

    private IEnumerator PauseRoutine() {
        resume = false;
        yield return new WaitForSeconds(pauseTime);
        resume = true;

    }
}
