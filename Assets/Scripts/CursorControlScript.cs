using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorControlScript : MonoBehaviour
{
    Camera cam;
    public GameObject cursor;
    GameObject target;
    bool moving;
    bool combat;
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
            moving = false;
            if (!combat)
            {                
                pathfinding.NewPath(cursor.transform.position);
                return;
            }
            if (combat)
            {
                if (target)
                {
                    combatScript.DashTarget(target, target.tag);
                    pathfinding.NewPath(target.transform.position);
                    pathfinding.speed *= 4;
                }              
            }
        }
    }
}
