using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportSkill : MonoBehaviour
{
    public GameObject teleportCursor;
    public GameObject fbx;
    Camera cam;
    public int speedMultiplier = 4;
    public bool preparing, teleporting;
    PlayerPathScript playerPath;
    Vector3 teleportDestination;

    private void Awake()
    {
        playerPath = GetComponent<PlayerPathScript>();
        cam = Camera.main;
    }

    public void CallTeleport()
    {
        preparing = true;
        Inventory.instance.asInventory.pitch = Random.Range(0.85f, 1.15f);
        Inventory.instance.asInventory.volume = .15f;
        Inventory.instance.asInventory.clip = Inventory.instance.adButtonClick;
        Inventory.instance.asInventory.Play();
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
		    if (hit.collider.CompareTag("Walkable"))
                    {
                        teleportCursor.transform.position = hit.point;
                    }

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
        fbx.SetActive(false);
        playerPath.NewPath(teleportDestination);
        playerPath.speed *= speedMultiplier;
        teleporting = true;
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Cursor"))
        {
            if (teleporting)
            {
                playerPath.ResetSpeed();
                fbx.SetActive(true);
                teleporting = false;
                teleportCursor.transform.position = Vector3.zero;
            }
        }
    }
}
