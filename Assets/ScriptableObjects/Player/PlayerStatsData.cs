using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatsData", menuName = "Game/PlayerStatsData")]
public class PlayerStatsData : ScriptableObject
{

    [Header("Base Stats")]
    public float baseHP = 100f;
    public float baseArmor = 0f;
    public float baseMoveSpeed = 5f;
    public float baseCritChance = 5f;

}
