using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    public ParticleSystem fireParticles;
    public GameObject triggerFire;
    public EnemyIA enemyIA;
    public bool attacking, animAttack;
    public float rangeRadius, preparationTime, attackTime, burnTime;
    public AnimationClip attackAnimation;
    public AnimationClip raiseAnimation;

    void Update()
    {
        
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
}
