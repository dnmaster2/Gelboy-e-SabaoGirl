using System.Collections;
using UnityEngine;

public class BossFire : MonoBehaviour
{
    [Tooltip("Dano ao ter contato com o fogo")]
    public int fireDamage;
    [Tooltip("Dano que será recebido durante o tempo que estiver queimando")]
    public int burnDamage;
    [Tooltip("Quantas vezes o burnDamage será dado")]
    public int burnTicks;
    [Tooltip("Tempo entre ticks")]
    public float tickInterval;
    private Attributes _playerAttributes;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerAttributes = other.GetComponent<Attributes>();
            _playerAttributes.health -= fireDamage;
            StartCoroutine(BurnTick(burnTicks, tickInterval, burnDamage, _playerAttributes));
        }
    }

    IEnumerator BurnTick(int bT, float tI, int bD, Attributes pA)
    {
        for (int i = 0; i < bT; i++)
        {
            yield return new WaitForSeconds(tI);
            pA.health -= bD;
        }
    }
}
