using System.Collections.Generic;
using Zorro.Settings;

namespace Push.Settings;

[ContentWarningSetting]
public class CanPushPlayersSetting : EnumSetting, IExposedSetting
{
    public override void ApplyValue() {}

    public override int GetDefaultValue() => 1;

    public SettingCategory GetSettingCategory() => SettingCategory.Mods;

    public string GetDisplayName() => "Can Push Players";
    public override List<string> GetChoices() {
        return new List<string>() {
            "Off",
            "On"
        };
    }
}