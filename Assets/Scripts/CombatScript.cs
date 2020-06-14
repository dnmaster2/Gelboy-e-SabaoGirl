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
    public float regenerationRate = 1.2f, damageCooldown = 3f;
    bool damageTaken;

    private void Awake()
    {
        playerAttributes = GetComponent<Attributes>();
        pathScript = GetComponent<PlayerPathScript>();
        StartCoroutine(RegenerationRotine());
    }

    public void DashTarget(GameObject newTarget, string tag)
    {
        startCombat = true;
        target = newTarget;
        targetTag = tag;
    }

    public void TakeDamage(int damage)
    {
        playerAttributes.health -= damage;
        StopCoroutine(RegenerationCooldown());
        StartCoroutine(RegenerationCooldown());
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (targetTag != "" && target)
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
            TakeDamage(other.gameObject.GetComponent<FireScript>().damage);
            StartCoroutine(FireDamage(other.GetComponent<FireScript>().burnTime, 1));
        }

        if (other.CompareTag("Punch"))
        {
            other.transform.root.gameObject.GetComponent<TiredAndPainAttackScript>().StartPunch(this);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Punch"))
        {
            other.transform.root.gameObject.GetComponent<TiredAndPainAttackScript>().StopPunch();
        }
    }

    IEnumerator FireDamage(float burnTime, int damage)
    {
        float i = 0;
        while (i < burnTime)
        {
            playerAttributes.health -= damage;
            i += .5f;
            yield return new WaitForSeconds(.5f);
        }
        StopCoroutine(RegenerationCooldown());
        StartCoroutine(RegenerationCooldown());
    }

    IEnumerator RegenerationCooldown()
    {
        damageTaken = true;
        yield return new WaitForSeconds(damageCooldown);
        damageTaken = false;
    }

    IEnumerator RegenerationRotine()
    {
        if (!damageTaken)
        {            
            if (playerAttributes.health < 100)
            {
                playerAttributes.health++;
            }
        }
        yield return new WaitForSeconds(regenerationRate);
        StartCoroutine(RegenerationRotine());
    }
}
