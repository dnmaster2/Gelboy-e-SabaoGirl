using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControler : MonoBehaviour
{
    public GameObject skillButton;
    public GameObject player;
    public Attributes playerAttributes;
    public float paddingBetweenButtons;
    public Text pontos;
    private void Awake()
    {
        int control = 0;
        TeleportSkill teleport = player.GetComponent<TeleportSkill>();
        AreaDamageSkill areaDamage = player.GetComponent<AreaDamageSkill>();
        if (teleport)
        {
            control++;
            GameObject TeleportButton = Instantiate(skillButton, transform);
            Button button = TeleportButton.GetComponent<Button>();
            button.onClick.AddListener(teleport.CallTeleport);
        }
        if (areaDamage)
        {
            GameObject AreaButton = Instantiate(skillButton, transform);
            Button button = AreaButton.GetComponent<Button>();
            button.onClick.AddListener(areaDamage.CallAreaDamage);
            if (control == 1)
            {
                AreaButton.transform.position += Vector3.right * paddingBetweenButtons;
            }
        }

        playerAttributes = player.GetComponent<Attributes>();
    }

    private void Update()
    {
        pontos.text = "Pontos: " + playerAttributes.points.ToString();
    }
}
