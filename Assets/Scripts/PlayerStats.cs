using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class PlayerStats : NetworkBehaviour
{
    [Header("Base Stats Reference")]
    public PlayerStatsData baseStats;

    [Header("Runtime Stats")]
    [Networked] public float CurrentHP { get; set; }

    public float FinalArmor { get; private set; }
    public float FinalMoveSpeed { get; private set; }
    public float FinalCritChance { get; private set; }

    private readonly List<StatModifier> _modifiers = new List<StatModifier>();

    public override void Spawned()
    {
        if (Object.HasStateAuthority)
        {
            InitializeStats();
        }
    }

    private void InitializeStats()
    {
        CurrentHP = baseStats.baseHP;
        RecalculateStats();
    }

    public void ApplyModifier(StatModifier mod)
    {
        if (!Object.HasStateAuthority)
            return;

        _modifiers.Add(mod);
        RecalculateStats();
    }

    public void RemoveModifier(StatModifier mod)
    {
        if (!Object.HasStateAuthority)
            return;

        _modifiers.Remove(mod);
        RecalculateStats();
    }

    private void RecalculateStats()
    {
        float hp = baseStats.baseHP;
        float armor = baseStats.baseArmor;
        float moveSpeed = baseStats.baseMoveSpeed;
        float critChance = baseStats.baseCritChance;

        float hpMult = 1f;
        float armorMult = 1f;
        float moveSpeedMult = 1f;
        float critMult = 1f;

        foreach (var mod in _modifiers)
        {
            switch (mod.statType)
            {
                case StatType.HP:
                    if (mod.modifierType == ModifierType.Additive) hp += mod.value;
                    else hpMult *= mod.value;
                    break;

                case StatType.Armor:
                    if (mod.modifierType == ModifierType.Additive) armor += mod.value;
                    else armorMult *= mod.value;
                    break;

                case StatType.MoveSpeed:
                    if (mod.modifierType == ModifierType.Additive) moveSpeed += mod.value;
                    else moveSpeedMult *= mod.value;
                    break;

                case StatType.CritChance:
                    if (mod.modifierType == ModifierType.Additive) critChance += mod.value;
                    else critMult *= mod.value;
                    break;
            }
        }

        // HP max can be tracked later if you want; for now we just compute final stat values.
        FinalArmor = armor * armorMult;
        FinalMoveSpeed = moveSpeed * moveSpeedMult;
        FinalCritChance = critChance * critMult;
    }

    public void TakeDamage(float amount)
    {
        if (!Object.HasStateAuthority)
            return;

        float reduced = Mathf.Max(amount - FinalArmor, 1f);
        CurrentHP -= reduced;

        if (CurrentHP <= 0f)
        {
            CurrentHP = 0f;
            // TODO: trigger downed state here in a later card
        }
    }
}