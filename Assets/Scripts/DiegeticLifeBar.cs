using UnityEngine;
using UnityEngine.UI;

public class DiegeticLifeBar : MonoBehaviour
{
    [SerializeField] Attributes enemyAttributes;
    [SerializeField] Slider healthBar;
    [SerializeField] Image fillHealthBar;
    [SerializeField] Gradient colorBar;
    [SerializeField] Transform cameraPos;
    private void Awake()
    {
        enemyAttributes = transform.root.gameObject.GetComponent<Attributes>();
        healthBar.maxValue = enemyAttributes.health;
        cameraPos = Camera.main.transform;
    }
    private void Update()
    {
        transform.LookAt(cameraPos);
        healthBar.value = enemyAttributes.health;
        fillHealthBar.color = colorBar.Evaluate(healthBar.normalizedValue);
    }
}
