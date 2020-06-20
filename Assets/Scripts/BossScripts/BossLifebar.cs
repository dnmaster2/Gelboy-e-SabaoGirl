using UnityEngine;
using UnityEngine.UI;

public class BossLifebar : MonoBehaviour
{
    [SerializeField] Attributes enemyAttributes;
    [SerializeField] Slider healthBar;
    [SerializeField] Image fillHealthBar;
    [SerializeField] Gradient colorBar;

    private void Awake()
    {
        healthBar.maxValue = enemyAttributes.health;
    }

    private void Update()
    {
        healthBar.value = enemyAttributes.health;
        fillHealthBar.color = colorBar.Evaluate(healthBar.normalizedValue);
    }
}
