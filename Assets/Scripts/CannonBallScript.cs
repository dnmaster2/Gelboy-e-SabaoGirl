using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallScript : MonoBehaviour
{
    public int damage;
    private void Awake()
    {
        Destroy(gameObject, 3);
        GetComponent<Rigidbody>().velocity = transform.forward * 20;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Attributes>().health -= damage;
            Destroy(gameObject);
        }
    }
}
