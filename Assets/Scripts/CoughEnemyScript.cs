using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoughEnemyScript : MonoBehaviour
{
    #region Private Fields

    private EnemyIA eia;
    private Attributes at;
    private Transform target;
    private bool wickStarted;
    private IEnumerator explosiveCoroutine;
    //-----------------Retirar quando existir animação----------------
    private MeshRenderer mr;
    private Color mtColor;
    //-----------------Retirar quando existir animação----------------

    #endregion



    #region Public Fields

    [Tooltip("Raio em que esta IA ira começar a explodir, digamos que ela vai ligar o pavio")]
    public float explosionStartRadius;
    [Tooltip("Raio da explosão quando o pavio acaba")]
    public float explosionRadius;
    [Tooltip("Tempo até a explosão acontecer, se for acertado durante esse tempo é atordoado")]
    public float wickTime;
    [Space]
    [Tooltip("Particula da explosão")]
    public ParticleSystem explosionParticle;

    #endregion



    #region MonoBehaviour Callbakcs

    private void Awake()
    {
        //-----------------Retirar quando existir animação----------------
        mr = GetComponent<MeshRenderer>();
        mtColor = mr.material.color;
        //-----------------Retirar quando existir animação----------------

        //Preenchendo componentes necessários
        eia = GetComponent<EnemyIA>();
        if (eia == null)
        {
            Debug.LogError("Inimigo precisa do script EnemyAI", this);
            return;
        }
        at = GetComponent<Attributes>();
        if (at == null)
        {
            Debug.LogError("Inimigo precisa do script Attributes", this);
            return;
        }
        wickStarted = false;
        explosionParticle.Stop();
        explosionParticle.Clear();
    }

    private void Update()
    {
        if (!eia.dead)
        {
            target = eia.targetPosition;
            if (eia.onCamera)
            {
                RaycastHit[] hits = Physics.SphereCastAll(transform.position, explosionStartRadius, Vector3.forward);
                foreach (RaycastHit hit in hits)
                {
                    if (hit.collider.CompareTag("Player") && !wickStarted && !eia.stunned)
                    {
                        //Iniciando explosao
                        explosiveCoroutine = StartExplosion(wickTime, explosionRadius);
                        StartCoroutine(explosiveCoroutine);
                    }
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionStartRadius);
    }

    #endregion



    #region Custom Callbacks

    public void StopExplosion(float stunTime)
    {
        StartCoroutine(Stun(stunTime));
    }

    #endregion



    #region Ienumerator Callbacks

    IEnumerator StartExplosion(float wt, float er)
    {
        wickStarted = true;
        mr.material.color = new Color
        {
            r = 1,
            g = .15f,
            b = .15f,
            a = 1
        };
        yield return new WaitForSeconds(wt);
        
        //Dá dano na área
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, er, Vector3.forward);
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.CompareTag("Player"))
            {
                //explosao acertou jogador
                hit.collider.GetComponent<CombatScript>().TakeDamage(at.damage);
            }
        }
        //Dá play nas particulas
        explosionParticle.Play();
        mr.enabled = false;
        //Seta vida pra 0
        at.health = 0;
    }

    IEnumerator Stun(float t)
    {
        eia.stunned = true;
        eia.path = null;
        StopCoroutine(explosiveCoroutine);

        //-----------------Retirar quando existir animação----------------
        mr.material.color = mtColor;
        //-----------------Retirar quando existir animação----------------

        yield return new WaitForSeconds(t);
        wickStarted = false;
        eia.stunned = false;
    }

    #endregion
}
