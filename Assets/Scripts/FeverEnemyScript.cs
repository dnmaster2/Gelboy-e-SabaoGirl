using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverEnemyScript : MonoBehaviour
{
    public ParticleSystem fireParticles;
    public GameObject triggerFire;
    public Transform gun;
    public EnemyIA enemyIA;
    public bool attacking, animAttack;
    public float rangeRadius, preparationTime, attackTime, burnTime;
    public AnimationClip attackAnimation;
    public AnimationClip raiseAnimation;

    void Awake()
    {
        enemyIA = GetComponent<EnemyIA>();
        fireParticles.Pause();
        fireParticles.Clear();
        if(attackAnimation && raiseAnimation)
        {
            preparationTime = raiseAnimation.length;
            attackTime += attackAnimation.length;
        }
    }

    void Update()
    {
        if (!enemyIA.dead)
        {
            if (enemyIA.onCamera)
            {
                RaycastHit[] hits = Physics.SphereCastAll(transform.position, rangeRadius, Vector3.forward);
                foreach (RaycastHit hit in hits)
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        if (!attacking)
                        {
                            print("estou rodando a corotina");
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
        print("preparando");
        StartCoroutine(enemyIA.Stun(attackTime + preparationTime));
        animAttack = true;
        yield return new WaitForSeconds(preparationTime + 1f);
        print("ataque");
        fireParticles.Play();
        triggerFire.SetActive(true);
        yield return new WaitForSeconds(attackTime - 1f);
        print("fim do ataque");
        fireParticles.Pause();
        fireParticles.Clear();
        triggerFire.SetActive(false);
        animAttack = false;
        yield return new WaitForSeconds(2f);
        print("reset");
        attacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangeRadius);
    }
}
