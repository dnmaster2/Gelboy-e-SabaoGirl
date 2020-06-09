using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDamageSkill : MonoBehaviour
{
    public bool startDamage;
    public float areaSize;

    public void CallAreaDamage()
    {
        print("chamando corrotina");
        StartCoroutine(AreaDamage());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            print("botao pressionado");
            CallAreaDamage();
        }
    }

    IEnumerator AreaDamage()
    {
        yield return new WaitForSeconds(.2f);
        print("Castando raycasts");
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, areaSize, Vector3.forward);
        foreach(RaycastHit hit in hits)
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                print(hit.collider.tag + " receberia dano!");
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, areaSize);
    }
}
