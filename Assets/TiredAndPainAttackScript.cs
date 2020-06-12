using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiredAndPainAttackScript : MonoBehaviour
{
    public int damage;
    public float punchSpeed;
    CombatScript combat;
    void Awake()
    {
        damage  = transform.root.gameObject.GetComponent<Attributes>().damage;
        StartCoroutine(Punch());
    }

    public void StartPunch(CombatScript newCombat)
    {
        combat = newCombat;
    }

    public void StopPunch()
    {
        combat = null;
    }

    IEnumerator Punch()
    {       
        if (combat)
        {
            combat.TakeDamage(damage);
        }
        yield return new WaitForSeconds(punchSpeed);
        StartCoroutine(Punch());
    }
}
