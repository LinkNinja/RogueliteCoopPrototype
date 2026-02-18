using UnityEngine;

[System.Serializable]
public struct StatModifier
{
    public StatType statType;
    public ModifierType modifierType;
    public float value;

    public StatModifier(StatType statType, ModifierType modifierType, float value)
    {
        this.statType = statType;
        this.modifierType = modifierType;
        this.value = value;
    }
}
