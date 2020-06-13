using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public PlayerPathScript pathScript;
    public CombatScript combat;
    public Animator anim;

    private void Update()
    {
        anim.SetBool("Walk", pathScript.walking);
        anim.SetBool("Attack", combat.startCombat);
        print(combat.startCombat);
    }
}
