using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyIA : MonoBehaviour
{
    public bool dead, onCamera, stunned;
    public int maxViewpoint = 1;
    public int minViewpoint = 0;
    public float rotationSpeed;

    Seeker seeker;
    Camera cam;
    CharacterController controller;
    Attributes attributes;
    public Path path;
    public Transform targetPosition;

    public float speed = 2;
    public int rewardPoints;
    public float nextWaypointDistance = 3;
    private int currentWaypoint = 0;
    public bool reachedEndOfPath;

    // Start is called before the first frame update
    void Awake()
    {
        cam = Camera.main;
        seeker = GetComponent<Seeker>();
        controller = GetComponent<CharacterController>();
        attributes = GetComponent<Attributes>();
    }

    public void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    public void ResetPath()
    {
        path = null;
        speed = 2;
        nextWaypointDistance = 3;
    }

    IEnumerator Stun(float stunTime)
    {
        stunned = true;
        path = null;
        yield return new WaitForSeconds(stunTime);
        stunned = false;
    }

    void Update()
    {
        if (attributes.health <= 0 && !dead)
        {
            Destroy(GetComponent<Collider>());
            Destroy(controller);
            GameObject.Find("Canvas").GetComponent<UIControler>().HitCombo(rewardPoints);
            Destroy(gameObject, 2f);
            dead = true;
        }
        Vector3 positionInViewport = cam.WorldToViewportPoint(transform.position);
        onCamera = positionInViewport.x > minViewpoint && positionInViewport.x < maxViewpoint
            && positionInViewport.y > minViewpoint && positionInViewport.y < maxViewpoint;

        if (onCamera)
        {
            targetPosition = GameObject.FindGameObjectWithTag("Player").transform;
            seeker.StartPath(transform.position, targetPosition.position, OnPathComplete);
        }
        else
        {
            targetPosition = null;
            path = null;
        }

        if (!dead && !stunned && onCamera)
        {
            var lookPos = targetPosition.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);

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
}
