using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControler : MonoBehaviour
{
    #region Script Variables
    [Header("Referencias ao jogador")]
    [Tooltip("Objeto aonde está os scripts do jogador")]
    public GameObject player;
    [Tooltip("Script de atributos do jogador")]
    public Attributes playerAttributes;
    #endregion
    #region Public Variables
    [Space]
    [Header("Atributos")]
    [Tooltip("Tempo para reiniciar o multiplicador do combo")]
    public float comboCooldown;
    [Tooltip("Modificador que diminui o tempo para reiniciar o multiplicador de combo")]
    public float nextCooldownDifference;
    [Tooltip("Tempo para encerrar o estágio")]
    public int timeToEndStage;
    [Tooltip("Atual fator de multiplicação de pontos")]
    public int multiplierCombo = 1;
    #endregion
    #region UI Components
    [Space]
    [Header("Referencias do Canvas")]
    [Tooltip("Texto para exibir pontos")]
    public Text pontosTXT;
    [Tooltip("Texto para exibir o tempo restante")]
    public Text tempo;
    [Tooltip("Texto para exibir fator atual de combo")]
    public Text combo;
    [Tooltip("Texto para exibir saude em numero")]
    public Text healthTXT;
    [Tooltip("Texto com inimigos restantes")]
    public Text enemyCounterTXT;
    [Tooltip("Botão de habilidade, inciado no awake depois de buscar o script de habilidade do player")]
    public Button skillButton;
    [Tooltip("Componente Slider da barra de vida")]
    public Slider healthBar;
    [Tooltip("Imagem fill do slider")]
    public Image fillHealthBar;
    [Tooltip("Cor da barra de acordo com a porcentagem de vida")]
    public Gradient colorBar;
    #endregion
    #region Private Variables
    float cooldownFirstValue;
    #endregion

    #region MonoBehaviour Callbacks
    private void Awake()
    {
        //Busca pelos scripts no player, e habilita o listener do botão de acordo com o script encontrado
        TeleportSkill teleport = player.GetComponent<TeleportSkill>();
        AreaDamageSkill areaDamage = player.GetComponent<AreaDamageSkill>();
        if (teleport)
        {
            skillButton.onClick.AddListener(teleport.CallTeleport);
        }
        if (areaDamage)
        {
            skillButton.onClick.AddListener(areaDamage.CallAreaDamage);
        }
        //Load do script de atributos
        playerAttributes = player.GetComponent<Attributes>();
        //Salvando primeiro valor de reinicio do combo
        cooldownFirstValue = comboCooldown;
        //Definindo o valor maximo da barra
        healthBar.maxValue = playerAttributes.health;
    }

    private void Update()
    {
        //Atualização de texto da UI
        pontosTXT.text = "Pontos: " + playerAttributes.points.ToString();
        tempo.text = (timeToEndStage - Mathf.RoundToInt(Time.time)).ToString();
        combo.text = multiplierCombo.ToString() + "X";
        healthTXT.text = playerAttributes.health.ToString();
        enemyCounterTXT.text = "Inimigos restantes: " + GameManager.enemies.ToString();
        //Define que o valor da barra é igual a vida do player
        healthBar.value = playerAttributes.health;
        //Usa Evaluate para mudar a cor da barra dinamicamente
        fillHealthBar.color = colorBar.Evaluate(healthBar.normalizedValue);
    }
    #endregion
    #region Custom Callbacks
    public void HitCombo(int points)
    {
        //Encerra a corrotina para reiniciar o timer
        StopCoroutine(ComboTimer());
        //Soma os pontos dentro do player
        playerAttributes.points += points * multiplierCombo;
        //Aumenta o multiplicador
        multiplierCombo++;
        //Diminui o tempo maximo para o proximo combo
        comboCooldown -= nextCooldownDifference;
        StartCoroutine(ComboTimer());
    }

    IEnumerator ComboTimer()
    {
        //Conta até o reinicio
        yield return new WaitForSeconds(comboCooldown);
        //Reset
        multiplierCombo = 1;
        comboCooldown = cooldownFirstValue;
    }
    #endregion
}
