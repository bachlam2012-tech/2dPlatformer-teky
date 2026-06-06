using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;
using UnityEngine.Scripting.APIUpdating;

public class SnakeAI : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed;
    public GameObject[] waypoints;
    int nextWayPoint = 1;
    float distToPoint;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    void Move()
    {
        distToPoint = Vector3.Distance(transform.position, waypoints[nextWayPoint].transform.position);
        transform.position = Vector3.MoveTowards(
            transform.position,
            waypoints[nextWayPoint].transform.position,
            moveSpeed * Time.deltaTime
        );
        if (distToPoint < 0.05f)
        {
            transform.position = waypoints[nextWayPoint].transform.position;
            TakeTurn();
        }
    }
    void TakeTurn()
    {
        Vector3 currentRotation = transform.eulerAngles;
        currentRotation.z += waypoints[nextWayPoint].transform.eulerAngles.z;
        transform.eulerAngles = currentRotation;
        ChooseNextWayPoint();
    }
    void ChooseNextWayPoint()
    {
        nextWayPoint++;
        if (nextWayPoint == waypoints.Length)
        {
            nextWayPoint = 0;
        }
    }
}
