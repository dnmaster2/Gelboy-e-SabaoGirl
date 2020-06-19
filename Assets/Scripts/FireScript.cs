using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScript : MonoBehaviour
{
    public int damage;
    public float burnTime;

    private void Awake()
    {
        damage = transform.parent.gameObject.GetComponent<Attributes>().damage;
        burnTime = transform.parent.gameObject.GetComponent<FeverEnemyScript>().burnTime;
    }
}
