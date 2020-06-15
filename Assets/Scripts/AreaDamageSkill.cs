using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDamageSkill : MonoBehaviour
{
    #region Public Variables
    [Tooltip("tamanho da area do Spherecast, representado na cena por um gizmo vermelho")]
    public float areaSize;
    [Tooltip("Referência aos Atributos do jogador")]
    public Attributes playerAttributes;
    #endregion
    #region MonoBehaviour Callbacks
    private void Awake()
    {
        //Load
        playerAttributes = GetComponent<Attributes>();
    }


    private void OnDrawGizmosSelected()
    {
        //Desenha uma esfera do tamanho do raycast
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, areaSize);
    }
    #endregion
    #region Custom Callbacks
    public void CallAreaDamage()
    {
        //Função publica para chamar a corotina de ataque
        StartCoroutine(AreaDamage());
    }

    IEnumerator AreaDamage()
    {
        //Substituir com a animação depois
        yield return new WaitForSeconds(.2f);
        //Casta um spherecastall, buscando por inimigos
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, areaSize, Vector3.forward);
        foreach(RaycastHit hit in hits)
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                //Tira vida do inimigo
                print("inimigo atingido");
                hit.collider.gameObject.GetComponent<Attributes>().health -= playerAttributes.damage;
            }
        }
    }
    #endregion

}
