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
                    if (hit.collider.GetComponent<Attributes>().id == 4)
                    {
                        hit.collider.GetComponent<CoughEnemyScript>().StopExplosion(3f);
                    }
                    pathScript.ResetSpeed();
                    startCombat = false;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fire"))
        {
            playerAttributes.health -= other.GetComponent<FireScript>().damage;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        StartCoroutine(FireDamage(other.GetComponent<FireScript>().burnTime, 1));
    }

    IEnumerator FireDamage(float burnTime, int damage)
    {
        float i = 0;
        while(i < burnTime)
        {
            print("queimando!");
            playerAttributes.health -= damage;
            i += .5f;
            yield return new WaitForSeconds(.5f);
        }
    }
}
