using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuvaScript : MonoBehaviour
{
    public float buffDuration;
    public float damageMultiplier;

    public  void Usar()
    {
        Debug.Log("Aplicando buff");
    }

    IEnumerator DamageBuff(Attributes at, float t, float m)
    {
        int d = at.damage;
        at.damage = (int)(at.damage * m);
        yield return new WaitForSeconds(t);
        at.damage = d;
    }
}
