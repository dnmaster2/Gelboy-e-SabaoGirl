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
        StartCoroutine(RegenerationCooldown());
    }

    IEnumerator RegenerationCooldown()
    {
        print("regeneração interrompida");
        damageTaken = true;
        yield return new WaitForSeconds(damageCooldown);
        damageTaken = false;
        print("regeneração voltou");
    }

    IEnumerator RegenerationRotine()
    {
        if (!damageTaken)
        {
            print("+1 de vida");
            if (playerAttributes.health < 100)
            {
                playerAttributes.health++;
            }
        }
        yield return new WaitForSeconds(regenerationRate);
        StartCoroutine(RegenerationRotine());
    }
}
