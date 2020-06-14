using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverAnimationController : MonoBehaviour
{
    public EnemyIA enemy;
    public FeverEnemyScript fever;
    public Animator anim;

    private void Update()
    {
        anim.SetBool("Walking", enemy.walking);
        anim.SetBool("Attack", fever.animAttack);
    }
}
