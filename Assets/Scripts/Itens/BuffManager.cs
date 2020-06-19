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
    private Transform _playerRef;
    private GameObject respawnGO;
    [Tooltip("Vida que a máscara vai adicionar a vida do player")]
    public int lifeFromMask;
    [Tooltip("Indica se o canhão está esquipado")]
    public bool cannonIsActive;

    public GameObject particleFX;

    private void Start()
    {
        _playerRef = GameObject.FindGameObjectWithTag("Player").transform;
        respawnActive = false;
        cannonIsActive = false;
    }

    #region Canhao
    public void Cannon()
    {
        if (cannonIsActive)
        {
            //desativa o canhao
            cannonIsActive = false;
        }
        else
        {
            //ativa o canhao
            cannonIsActive = true;
        }
    }
    #endregion

    #region Mascara
    public void Mask()
    {
        //adiciona vida ao jogador
        _playerRef.GetComponent<Attributes>().health += lifeFromMask;
        FindObjectOfType<AudioManager>().Play("PowerUp");
        GameObject particle = Instantiate(particleFX, _playerRef.position, _playerRef.rotation);
        Destroy(particle, 3f);
    }
    #endregion

    #region Respawn

    public void ActivateRespawn(GameObject itemRespawn)
    {
        respawnPosition = _playerRef.position;
        Physics.Raycast(_playerRef.position, Vector3.down, out RaycastHit hit, 25f);
        //instancia objeto representando lugar de spawn
        respawnGO = Instantiate(itemRespawn, hit.point, Quaternion.identity);
        respawnActive = true;
    }

    public void Respawn()
    {
        Destroy(respawnGO);
        //ativa particula para respawn
        _playerRef.position = respawnPosition;
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
