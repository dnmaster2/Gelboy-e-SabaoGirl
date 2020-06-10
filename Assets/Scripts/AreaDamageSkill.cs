using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDamageSkill : MonoBehaviour
{
    public bool startDamage;
    public float areaSize;
    public Attributes playerAttributes;

    private void Awake()
    {
        playerAttributes = GetComponent<Attributes>();
    }

    public void CallAreaDamage()
    {
        StartCoroutine(AreaDamage());
    }

    IEnumerator AreaDamage()
    {
        yield return new WaitForSeconds(.2f);
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, areaSize, Vector3.forward);
        foreach(RaycastHit hit in hits)
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                print("inimigo atingido");
                hit.collider.gameObject.GetComponent<Attributes>().health -= playerAttributes.damage;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, areaSize);
    }
}
