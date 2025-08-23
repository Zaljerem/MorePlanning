using HugsLib;
using HugsLib.Settings;
using Verse;

namespace MorePlanning.Settings;

internal class ModSettings
{
    private SettingHandle<int> _planOpacity;
    private SettingHandle<bool> _removeIfBuildingDespawned;

    private SettingHandle<bool> _shiftKeyForOverride;

    private ModSettings()
    {
    }

    public bool RemoveIfBuildingDespawned => _removeIfBuildingDespawned;

    public bool ShiftKeyForOverride => _shiftKeyForOverride;

    public int PlanOpacity
    {
        get => _planOpacity.Value;
        set
        {
            if (_planOpacity.Value == value)
            {
                return;
            }

            _planOpacity.Value = value;
            HugsLibController.SettingsManager.SaveChanges();
        }
    }

    public int DefaultPlanOpacity => _planOpacity.DefaultValue;

    public static ModSettings CreateModSettings(ModSettingsPack settingsPack)
    {
        var modSettings = new ModSettings
        {
            _removeIfBuildingDespawned = settingsPack.GetHandle("removeIfBuildingDespawned",
                "MorePlanning.SettingRemoveIfBuildingDespawned.label".Translate(),
                "MorePlanning.SettingRemoveIfBuildingDespawned.desc".Translate(), false),
            _shiftKeyForOverride = settingsPack.GetHandle("shiftKeyForOverride",
                "MorePlanning.SettingShiftKeyForOverride.label".Translate(),
                "MorePlanning.SettingShiftKeyForOverride.desc".Translate(), false),
            _planOpacity = settingsPack.GetHandle("opacity",
                "MorePlanning.SettingPlanOpacity.label".Translate(), "MorePlanning.SettingPlanOpacity.desc".Translate(),
                25)
        };
        modSettings._planOpacity.NeverVisible = true;
        return modSettings;
    }
}