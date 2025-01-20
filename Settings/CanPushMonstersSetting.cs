using System.Collections.Generic;
using Zorro.Settings;

namespace Push.Settings;

[ContentWarningSetting]
public class CanPushMonstersSetting : EnumSetting, IExposedSetting
{
    public override void ApplyValue() {}

    public override int GetDefaultValue() => 0;

    public SettingCategory GetSettingCategory() => SettingCategory.Mods;

    public string GetDisplayName() => "Can Push Monsters";
    public override List<string> GetChoices() {
        return new List<string>() {
            "Off",
            "On"
        };
    }
}