using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatScript : MonoBehaviour
{
    public GameObject target;
    Attributes playerAttributes;
    PlayerPathScript pathScript;
    public string targetTag;
    public bool startCombat;

    private void Awake()
    {
        playerAttributes = GetComponent<Attributes>();
        pathScript = GetComponent<PlayerPathScript>();
    }
    public void DashTarget(GameObject newTarget, string tag)
    {
        startCombat = true;
        target = newTarget;
        targetTag = tag;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(targetTag != "" && target)
        {
            if (hit.collider.CompareTag(targetTag))
            {
                if (startCombat)
                {
                    hit.collider.gameObject.GetComponent<Attributes>().health -= playerAttributes.damage;
                    pathScript.ResetSpeed();
                }
            }
        }

    }
}
