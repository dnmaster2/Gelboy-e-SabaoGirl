using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAManagement : MonoBehaviour
{
    public List<GameObject> activeIA;
    public int maxActiveIA = 2;

    public bool OnCameraList(GameObject enemyOnCamera)
    {
        bool alreadyOnList = activeIA.Contains(enemyOnCamera);
        if (!alreadyOnList && activeIA.Count < maxActiveIA)
        {
            activeIA.Add(enemyOnCamera);
            print("adcionado " + enemyOnCamera.name + " no Array");
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsAIActive(GameObject ia)
    {
        return activeIA.Contains(ia);
    }

    public void RemoveFromCamera(GameObject objectToBeRemoved)
    {
        if (activeIA.Contains(objectToBeRemoved))
        {
            print("Removido " + objectToBeRemoved.name + " no Array");
            activeIA.Remove(objectToBeRemoved);
        }
    }
}
