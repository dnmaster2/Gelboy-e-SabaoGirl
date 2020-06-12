using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    #region instanciaBuffs

    public static BuffManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Mais de uma instancia",this);
        }
        instance = this;
    }

    #endregion

    [Tooltip("Multiplicador do dano que a luva irá dar")]
    public float damageBuffMultiplier = 1.5f;
    [Tooltip("Controle para respawnar jogador quando morrer")]
    public bool respawnActive;
    private Vector3 respawnPosition;
    private GameObject _playerRef;
    [Tooltip("Vida que a máscara vai adicionar a vida do player")]
    public int lifeFromMask;

    private void Start()
    {
        _playerRef = GameObject.FindGameObjectWithTag("Player");
        respawnActive = false;
    }

    #region Mascara
    public void Mask()
    {
        _playerRef.GetComponent<Attributes>().health += lifeFromMask;
    }
    #endregion

    #region Respawn

    public void ActivateRespawn()
    {
        respawnPosition = _playerRef.transform.position;
        //instancia objeto representando lugar de spawn
        respawnActive = true;
    }

    public void Respawn()
    {
        //ativa particula para respawn
        _playerRef.transform.position = respawnPosition;
    }

    #endregion
    
    #region Damage

    public IEnumerator DamageBuff(float t)
    {
        Attributes a = _playerRef.GetComponent<Attributes>();
        int d = a.damage;
        Debug.Log("Aplicando buff");
        a.damage = (int)(a.damage * damageBuffMultiplier);
        yield return new WaitForSeconds(t);
        a.damage = d;
    }

    #endregion
}
