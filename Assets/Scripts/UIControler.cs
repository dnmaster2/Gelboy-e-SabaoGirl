using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControler : MonoBehaviour
{
    [Header("Referencias ao jogador")]
    public GameObject player;
    public Attributes playerAttributes;
    [Space]
    [Header("Atributos")]
    public float paddingBetweenButtons;
    public float comboCooldown;
    public float nextCooldownDifference;
    public int timeToEndStage;
    public int multiplierCombo = 1;
    public int pontos;
    [Space]
    [Header("Referencias do Canvas")]
    public Text pontosTXT;
    public Text tempo;
    public Text combo;
    public Text healthTXT;
    public Button skillButton;
    public Slider healthBar;
    public Image fillHealthBar;
    public Gradient colorBar;

    float cooldownFirstValue;
    private void Awake()
    {
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

        playerAttributes = player.GetComponent<Attributes>();
        cooldownFirstValue = comboCooldown;
        healthBar.maxValue = playerAttributes.health;
    }

    private void Update()
    {
        pontosTXT.text = "Pontos: " + playerAttributes.points.ToString();
        tempo.text = (timeToEndStage - Mathf.RoundToInt(Time.time)).ToString();
        combo.text = multiplierCombo.ToString() + "X";
        healthBar.value = playerAttributes.health;
        healthTXT.text = playerAttributes.health.ToString();
        fillHealthBar.color = colorBar.Evaluate(healthBar.normalizedValue);
    }

    public void HitCombo(int points)
    {
        StopCoroutine(ComboTimer());
        playerAttributes.points += points * multiplierCombo;
        multiplierCombo++;
        comboCooldown -= nextCooldownDifference;
        StartCoroutine(ComboTimer());
    }

    IEnumerator ComboTimer()
    {
        yield return new WaitForSeconds(comboCooldown);
        multiplierCombo = 1;
        comboCooldown = cooldownFirstValue;
    }
}
