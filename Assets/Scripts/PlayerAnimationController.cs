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
        if (pathScript.reachedEndOfPath)
        {
            anim.SetBool("Walk", false);
        }
        else if (pathScript.walking)
        {
            anim.SetBool("Walk", true);
        }
        if (combat.startCombat)
        {
            anim.SetBool("Attack", true);
        }
        else if (!combat.startCombat)
        {
            anim.SetBool("Attack", false);
        }
    }
}
