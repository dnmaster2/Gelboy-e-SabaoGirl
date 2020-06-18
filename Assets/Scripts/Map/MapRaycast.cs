using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRaycast : MonoBehaviour
{
    Camera cam;
    public ChoosePlayerScript cps;

    private void Awake()
    {
        cam = Camera.main;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !cps._playerInterface.activeInHierarchy)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("MapSphere"))
                {
                    hit.collider.GetComponent<MapScript>().GoToMap();
                }
            }
        }
    }
}
