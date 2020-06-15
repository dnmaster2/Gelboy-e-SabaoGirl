using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attributes : MonoBehaviour
{
    [Tooltip("Vida desta entidade")]
    public int health;
    [Tooltip("Dano desta entidade")]
    public int damage;
    [Tooltip("Quantos pontos ele tem")]
    public int points;
    [Tooltip("Armazena o maior combo do jogador, para stats")]
    public int bestCombo;
    [Tooltip("Estágio atual")]
    public int actualStage;
    [Space]
    public int id;
}
