using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatScript : MonoBehaviour
{
    #region Custom Script Variable
    Attributes playerAttributes;
    PlayerPathScript pathScript;
    #endregion
    #region Public Variables
    [Tooltip("booleana de controle para o combate, também afeta animação")]
    public bool startCombat;
    public bool runningCombat;
    [Tooltip("Tempo entre as somas de +1 da regeneração")]
    public float regenerationRate = 1.2f;
    [Tooltip("Tempo sem regenerar depois de um dano")]
    public float damageCooldown = 3f;
    #endregion
    #region Private Variables
    bool damageTaken;
    bool dead = false;
    #endregion
    #region MonoBehaviour Callbacks
    private void Awake()
    {
        //Load
        playerAttributes = GetComponent<Attributes>();
        pathScript = GetComponent<PlayerPathScript>();
        //Chama a corotina recursiva
        StartCoroutine(RegenerationRotine());
    }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

        //Checa se o target é o chamado
        if (hit.collider.CompareTag("Enemy"))
        {
            if (startCombat)
            {
                //Da dano
                hit.collider.gameObject.GetComponent<Attributes>().health -= playerAttributes.damage;
                FindObjectOfType<AudioManager>().Play("Hit" + Random.Range(1, 4));
                //Caso seja o Tosse, chama o stun
                if (hit.collider.GetComponent<Attributes>().id == 4)
                {
                    hit.collider.GetComponent<CoughEnemyScript>().StopExplosion(3f);
                }
                //Reseta a velocidade para o padrão e desliga a bool de controle
                pathScript.ResetSpeed();
                startCombat = false;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        //Dano de fogo, chama duas funções
        if (other.CompareTag("Fire"))
        {
            TakeDamage(other.gameObject.GetComponent<FireScript>().damage);
            StartCoroutine(FireDamage(other.GetComponent<FireScript>().burnTime, 1));
        }
        //Dano de soco, chama uma função no script TiredAndPainAttackScript
        if (other.CompareTag("Punch"))
        {
            other.transform.parent.gameObject.GetComponent<TiredAndPainAttackScript>().StartPunch(this);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        //Saiu da area de soco, chama a função para parar a rotina
        if (other.CompareTag("Punch"))
        {
            other.transform.parent.gameObject.GetComponent<TiredAndPainAttackScript>().StopPunch();
        }
    }
    #endregion

    #region Custom Callbacks

    public void DamagePlayer(int d)
    {
        playerAttributes.health -= d;
        if (playerAttributes.health <= 0 && !dead)
        {
            if (BuffManager.instance.respawnActive)
            {
                Respawn();
            }
            else
            {
                Debug.Log("Player is dead");
                FindObjectOfType<GameManager>().PlayerDeath(playerAttributes.points);
                dead = true;
            }
        }
    }

    public void Respawn()
    {
        playerAttributes.health = 100;
        BuffManager.instance.Respawn();
    }

    public void TakeDamage(int damage)
    {
        //Tira vida baseado no dano recebido na função
        DamagePlayer(damage);
        //Reinicia a Corotina
        StopCoroutine(RegenerationCooldown());
        StartCoroutine(RegenerationCooldown());
    }


    IEnumerator FireDamage(float burnTime, int damage)
    {
        //Enquanto i for menor que o tempo de queima, retire o dano
        //Variaveis locais, enviadas do FeverScript
        float i = 0;
        while (i < burnTime)
        {
            DamagePlayer(damage);
            i += .5f;
            yield return new WaitForSeconds(.5f);
        }
        StopCoroutine(RegenerationCooldown());
        StartCoroutine(RegenerationCooldown());
    }

    IEnumerator RegenerationCooldown()
    {
        //Trava a booleana de controle impedindo a soma de regeneração
        damageTaken = true;
        yield return new WaitForSeconds(damageCooldown);
        //retorna após o fim da contagem
        damageTaken = false;
    }

    IEnumerator RegenerationRotine()
    {
        //Check da booleana de controle
        if (!damageTaken)
        {
            //Se a vida for menor que 100 (trocar por uma variavel que guarda o primeiro valor no awake)
            if (playerAttributes.health < 100)
            {
                playerAttributes.health++;
            }
        }
        yield return new WaitForSeconds(regenerationRate);
        //Chama novamente após o fim da contagem
        StartCoroutine(RegenerationRotine());
    }
    #endregion
}
