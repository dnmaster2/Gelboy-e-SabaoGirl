using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportSkill : MonoBehaviour
{
    public GameObject teleportCursor;
    Camera cam;
    public bool preparing, teleporting;
    PlayerPathScript playerPath;
    Vector3 teleportDestination;
    Renderer rend;

    private void Awake()
    {
        playerPath = GetComponent<PlayerPathScript>();
        rend = GetComponent<Renderer>();
        cam = Camera.main;
    }

    public void CallTeleport()
    {
        preparing = true;
    }

    private void Update()
    {
        teleportCursor.GetComponent<Renderer>().enabled = preparing;

        if (preparing)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                teleportCursor.transform.position = hit.point;
            }

            if (Input.GetMouseButtonDown(0))
            {
                teleportDestination = teleportCursor.transform.position;
                StartCoroutine(RunSkill());
                preparing = false;
            }
        }
    }

    IEnumerator RunSkill()
    {
        yield return new WaitForSeconds(.3f);
        rend.enabled = false;
        playerPath.NewPath(teleportDestination);
        playerPath.speed *= 4;
        teleporting = true;
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Cursor"))
        {
            if (teleporting)
            {
                playerPath.speed /= 4;
                rend.enabled = true;
                teleporting = false;
                teleportCursor.transform.position = Vector3.zero;
            }
        }
    }
}
