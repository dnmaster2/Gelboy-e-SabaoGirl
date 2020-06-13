using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorControlScript : MonoBehaviour
{
    Camera cam;
    public GameObject cursor;
    public GameObject cannonShoot, cannonCursor;
    public float buffCombatTarget;
    GameObject target;
    public bool moving;
    public bool combat;
    PlayerPathScript pathfinding;
    CombatScript combatScript;

    private void Awake()
    {
        cam = Camera.main;
        pathfinding = GetComponent<PlayerPathScript>();
        combatScript = GetComponent<CombatScript>();
    }

    private void Update()
    {
        cannonCursor.SetActive(BuffManager.instance.cannonIsActive);
        if (Input.GetMouseButtonDown(0))
        {
            if (BuffManager.instance.cannonIsActive)
            {
                var cannonLookPos = cannonCursor.transform.position - transform.position;
                cannonLookPos.y = 0;
                var cannonRotation = Quaternion.LookRotation(cannonLookPos);
                GameObject cannonBall = Instantiate(cannonShoot, transform.position + transform.forward * 2, cannonRotation, transform.parent);
            }
        }

        if (BuffManager.instance.cannonIsActive)
        {
            cannonCursor.SetActive(true);
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                cannonCursor.transform.position = hit.point;
            }

            var cannonLookPos = cannonCursor.transform.position - transform.position;
            cannonLookPos.y = 0;
            var cannonRotation = Quaternion.LookRotation(cannonLookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, cannonRotation, Time.deltaTime * 10);
            return;
        }

        var lookPos = cursor.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10);

        cursor.SetActive(moving);
        if (Input.GetMouseButton(0))
        {
            if (!moving)
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        moving = true;
                        cursor.transform.position = hit.point;
                    }
                }
            }
            if (moving)
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.CompareTag("Walkable"))
                    {
                        cursor.transform.position = hit.point;
                        combat = false;
                    }
                    if (hit.collider.CompareTag("Enemy"))
                    {
                        combat = true;
                        cursor.transform.position = hit.point;
                        target = hit.collider.gameObject;
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (!combat && moving)
            {
                pathfinding.NewPath(cursor.transform.position);
                moving = false;
                return;
            }

            if (combat)
            {
                if (target)
                {
                    combatScript.DashTarget(target, target.tag);
                    pathfinding.NewPath(target.transform.position + transform.forward * buffCombatTarget);
                    pathfinding.speed *= 4;
                    moving = false;
                }
                return;
            }
        }
    }
}
