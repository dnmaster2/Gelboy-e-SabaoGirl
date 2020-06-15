using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallScript : MonoBehaviour
{
    #region Public Variables
    [Tooltip("Dano do tiro de canhão")]
    public int damage;
    #endregion

    #region MonoBehaviour Callbacks
    private void Awake()
    {
        //Define tempo de destruição e velocidade final do disparo
        Destroy(gameObject, 3);
        GetComponent<Rigidbody>().velocity = transform.forward * 20;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Busca por qualquer inimigo
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //Da dano e se autodestroi
            collision.gameObject.GetComponent<Attributes>().health -= damage;
            Destroy(gameObject);
        }
    }
    #endregion 
}
