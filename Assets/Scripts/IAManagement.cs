using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAManagement : MonoBehaviour
{
    public int maxActiveIA = 2;

    public void OnCameraList(Transform ia)
    {
        bool alreadyOnList = ia.root == transform;
        if (!alreadyOnList && transform.childCount < maxActiveIA)
        {
            ia.SetParent(transform, true);
        }
    }

    public void RemoveFromCamera(Transform ia)
    {
        if (ia.root == transform)
        {
            ia.parent = null;
        }
    }
}
