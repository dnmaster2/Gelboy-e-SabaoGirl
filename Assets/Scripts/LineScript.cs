using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineScript : MonoBehaviour
{
    LineRenderer line;
    public Transform player;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        line.enabled = gameObject.activeSelf;
        if (line.enabled)
        {
            Vector3[] points = new Vector3[2];
            points[0] = transform.position;
            points[1] = player.position;
            line.SetPositions(points);
        }
    }
}
