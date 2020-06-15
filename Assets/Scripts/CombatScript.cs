﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatScript : MonoBehaviour
{
    #region Custom Script Variable
    Attributes playerAttributes;
    PlayerPathScript pathScript;
    #endregion
    #region Public Variables
    [Tooltip("Alvo do ataque")]
    public GameObject target;
    [Tooltip("tag do alvo")]
    public string targetTag;
    [Tooltip("booleana de controle para o combate, também afeta animação")]
    public bool startCombat;
    [Tooltip("Tempo entre as somas de +1 da regeneração")]
    public float regenerationRate = 1.2f;
    [Tooltip("Tempo sem regenerar depois de um dano")]
    public float damageCooldown = 3f;
    #endregion
    #region Private Variables
    bool damageTaken;
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
        //Check de colisão não intencional com outras entidades
        if (targetTag != "" && target)
        {
            //Checa se o target é o chamado
            if (hit.collider.CompareTag(targetTag))
            {
                //Checa se o combate foi iniciado
                if (startCombat)
                {
                    //Da dano
                    hit.collider.gameObject.GetComponent<Attributes>().health -= playerAttributes.damage;
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
            other.transform.root.gameObject.GetComponent<TiredAndPainAttackScript>().StartPunch(this);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        //Saiu da area de soco, chama a função para parar a rotina
        if (other.CompareTag("Punch"))
        {
            other.transform.root.gameObject.GetComponent<TiredAndPainAttackScript>().StopPunch();
        }
    }
    #endregion

    #region Custom Callbacks
    public void DashTarget(GameObject newTarget, string tag)
    {
        //Cadastra um novo alvo para iniciar o dash, validando a próxima colisão com ele
        startCombat = true;
        target = newTarget;
        targetTag = tag;
    }

    public void TakeDamage(int damage)
    {
        //Tira vida baseado no dano recebido na função
        playerAttributes.health -= damage;
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
            playerAttributes.health -= damage;
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
