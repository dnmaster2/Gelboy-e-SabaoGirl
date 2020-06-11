using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScript : MonoBehaviour
{
    public int damage;
    public float burnTime;

    private void Awake()
    {
        damage = transform.root.gameObject.GetComponent<Attributes>().damage;
        burnTime = transform.root.gameObject.GetComponent<FeverEnemyScript>().burnTime;
    }
}
