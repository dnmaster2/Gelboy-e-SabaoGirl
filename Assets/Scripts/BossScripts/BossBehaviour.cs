using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    public bool fire, melee;
    Animator anim;
    private Transform _player;
    [Tooltip("Referencia ao collider de dano de fogo")]
    public GameObject fireCollider;
    [Tooltip("Particulas de fogo")]
    public ParticleSystem fireParticle;
    [Tooltip("Referencia ao collider de soco")]
    public GameObject punchCollider;
    private Attributes at;

    [Header("Controle de tempo")]

    [Tooltip("Tempo que este objeto ficará esperando para ir para o próximo ataque")]
    public float waitDuration;
    [Space]
    [Tooltip("Tempo ate o fogo começar, após espera. Use para ficar mais certinho com a animação")]
    public float fireWait;
    [Tooltip("Tempo em que o fogo ficará ativo")]
    public float fireDuration;
    [Space]
    [Tooltip("Tempo ate o soco começar, após espera. Use para ficar mais certinho com a animação")]
    public float punchWait;
    [Tooltip("Tempo em que o soco ficará ativo")]
    public float punchDuration;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        if (anim == null)
        {
            Debug.LogError("Este objeto precisa de um Animator", this);
            return;
        }
        at = GetComponent<Attributes>();
        if (at == null)
        {
            Debug.LogError("Este objeto precisa do script Attributes", this);
            return;
        }
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        //desligar collider controle para não começár atacando
        fireCollider.SetActive(false);
        punchCollider.SetActive(false);
        //parar completamente as particulas (controle)
        fireParticle.Stop();
        fireParticle.Clear();
        //começa corrotina de ataque
        StartCoroutine(WaitForAtack(waitDuration, fireWait, fireDuration, punchWait, punchDuration));
    }

    private void Update()
    {
        //look at player
        transform.LookAt(new Vector3(_player.position.x, transform.position.y, _player.position.z));

        //Animação
        anim.SetBool("Combo", melee);
        anim.SetBool("Fire", fire);
    }

    private void LateUpdate()
    {
        if (at.health <= 0)
        {
            Attributes pAt = _player.GetComponent<Attributes>();
            pAt.points += at.points;
            FindObjectOfType<GameManager>().EndLevel(pAt.points);
        }
    }

    #region Particle Methods

    private void StartFireParticle()
    {
        fireParticle.Play();
    }

    private void StopFireParticle()
    {
        fireParticle.Stop();
        fireParticle.Clear();
    }

    #endregion



    #region IEnumerator Attacks

    IEnumerator WaitForAtack(float waitTime, float fireWait, float fireDuration, float punchWait, float punchDuration)
    {
        melee = false;
        fire = false;
        yield return new WaitForSeconds(waitTime);
        //escolher ataque aleatorio
        if (Random.Range(0, 100) > 50)
        {
            StartCoroutine(FireAttack(waitTime, fireWait, fireDuration, punchWait, punchDuration));
        }
        else
        {
            StartCoroutine(PunchAttack(waitTime, fireWait, fireDuration, punchWait, punchDuration));
        }
    }

    IEnumerator FireAttack(float waitTime, float fireWait, float fireDuration, float punchWait, float punchDuration)
    {
        fire = true;
        //espera para ficar certinho com a animação
        yield return new WaitForSeconds(fireWait);
        //ligar colllider de fogo
        fireCollider.SetActive(true);
        StartFireParticle();
        yield return new WaitForSeconds(fireDuration);
        fireCollider.SetActive(false);
        StopFireParticle();
        fire = false;
        StartCoroutine(WaitForAtack(waitTime, fireWait, fireDuration, punchWait, punchDuration));
    }

    IEnumerator PunchAttack(float waitTime, float fireWait, float fireDuration, float punchWait, float punchDuration)
    {
        melee = true;
        //espera para ficar certinho com a animação
        yield return new WaitForSeconds(punchWait);
        //ligar collider do soco
        punchCollider.SetActive(true);
        yield return new WaitForSeconds(punchDuration);
        punchCollider.SetActive(false);
        melee = false;
        StartCoroutine(WaitForAtack(waitTime, fireWait, fireDuration, punchWait, punchDuration));
    }

    #endregion
}
