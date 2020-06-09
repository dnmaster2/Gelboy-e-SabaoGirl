using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyIA : MonoBehaviour
{
    public int maxViewpoint = 1;
    public int minViewpoint = 0;
    private Seeker seeker;
    Camera cam;
    private CharacterController controller;
    public Path path;
    public Transform targetPosition;

    public float speed = 2;
    public float nextWaypointDistance = 3;
    private int currentWaypoint = 0;
    public bool reachedEndOfPath;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        seeker = GetComponent<Seeker>();
        controller = GetComponent<CharacterController>();
    }

    public void OnPathComplete(Path p)
    {
        Debug.Log("Caminho calculado, erros: " + p.error);

        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 positionInViewport = cam.WorldToViewportPoint(transform.position);

        if (positionInViewport.x > minViewpoint && positionInViewport.x < maxViewpoint
            && positionInViewport.y > minViewpoint && positionInViewport.y < maxViewpoint)
        {
            targetPosition = GameObject.FindGameObjectWithTag("Player").transform;
            seeker.StartPath(transform.position, targetPosition.position, OnPathComplete);
        }
        else
        {
            targetPosition = null;
            path = null;
        }

        if (path == null)
        {
            return;
        }

        reachedEndOfPath = false;
        float distanceToWaypoint;
        while (true)
        {
            distanceToWaypoint = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
            if (distanceToWaypoint < nextWaypointDistance)
            {
                if (currentWaypoint + 1 < path.vectorPath.Count)
                {
                    currentWaypoint++;
                }
                else
                {
                    reachedEndOfPath = true;
                    break;
                }
            }
            else
            {
                break;
            }
        }

        var speedFactor = reachedEndOfPath ? Mathf.Sqrt(distanceToWaypoint / nextWaypointDistance) : 1f;
        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        Vector3 velocity = dir * speed * speedFactor;
        controller.SimpleMove(velocity);

    }
}
