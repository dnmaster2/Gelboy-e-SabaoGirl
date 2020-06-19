using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPunch : MonoBehaviour
{
    [Tooltip("Dano do soco")]
    public int punchDamage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Attributes>().health -= punchDamage;
        }
    }
}
