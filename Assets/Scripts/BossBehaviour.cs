using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    public bool fire, melee;
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        anim.SetBool("Combo", melee);
        anim.SetBool("Fire", fire);
    }

    IEnumerator SpitFire()
    {
        yield return new WaitForSeconds(.5f);
    }

}
