using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverEnemyScript : MonoBehaviour
{
    public ParticleSystem fireParticles;
    public GameObject triggerFire;
    public Transform gun;
    public EnemyIA enemyIA;
    Transform targetRef;
    bool attacking;
    public float rangeRadius, preparationTime, attackTime, burnTime;

    void Awake()
    {
        enemyIA = GetComponent<EnemyIA>();
        fireParticles.Pause();
        fireParticles.Clear();
    }

    void Update()
    {
        if (!enemyIA.dead)
        {
            targetRef = enemyIA.targetPosition;
            if (enemyIA.onCamera)
            {

                RaycastHit[] hits = Physics.SphereCastAll(transform.position, rangeRadius, Vector3.forward);
                foreach (RaycastHit hit in hits)
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        if (!attacking)
                        {
                            StartCoroutine(FlameThrowerRoutine());
                        }
                    }
                }
            }
        }
    }

    IEnumerator FlameThrowerRoutine()
    {
        attacking = true;
        yield return new WaitForSeconds(preparationTime);
        fireParticles.Play();
        triggerFire.SetActive(true);
        yield return new WaitForSeconds(attackTime);
        fireParticles.Pause();
        fireParticles.Clear();
        triggerFire.SetActive(false);
        attacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangeRadius);
    }
}
