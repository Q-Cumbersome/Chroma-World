using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 0f;
    [SerializeField] List<Transform> waypoints;

    private int waypointIndex = 0;
    private float range;

    void Start()
    {
        waypointIndex = 0;
        range = 1.0f;
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        if (waypointIndex <= waypoints.Count - 1)
        {
            var waypointPosition = waypoints[waypointIndex].transform.position;
            var enemyMovement = movementSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards
                (transform.position, waypointPosition, enemyMovement);

            if (Vector2.Distance(transform.position, waypointPosition) < range)
            {
                waypointIndex++;
                if (waypointIndex >= waypoints.Count)
                {
                    waypointIndex = 0;
                }
            }
        }
    }
}
