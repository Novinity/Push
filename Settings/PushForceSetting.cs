using Unity.Mathematics;
using UnityEngine;
using Zorro.Settings;

namespace Push.Settings;

[ContentWarningSetting]
public class PushForceSetting : FloatSetting, IExposedSetting
{
    public override void ApplyValue() {
        if (Value < 0)
        {
            Value = 0;
        } else if (Value > 500)
        {
            Value = int.MaxValue;
        }
    }

    protected override float GetDefaultValue() => 5;

    protected override float2 GetMinMaxValue() => new float2(0f, 500);

    public SettingCategory GetSettingCategory() => SettingCategory.Mods;

    public string GetDisplayName() => "Push Force";
    
    public override float Clamp(float value) => (float)Mathf.Round(base.Clamp(value));
    public override string Expose(float result) => Mathf.RoundToInt(result).ToString();
}