using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRaycast : MonoBehaviour
{
    Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
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
