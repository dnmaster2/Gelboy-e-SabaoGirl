using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControler : MonoBehaviour
{
    public Button skillButton;
    public GameObject player;
    public Attributes playerAttributes;
    public float paddingBetweenButtons, comboCooldown, nextCooldownDifference;
    public int multiplierCombo = 1, pontos;
    public Text pontosTXT, tempo, combo;
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
    }

    private void Update()
    {
        pontosTXT.text = "Pontos: " + playerAttributes.points.ToString();
        tempo.text = Mathf.RoundToInt(Time.time).ToString();
        combo.text = multiplierCombo.ToString() + "X";
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
