using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HugsLib;
using HugsLib.Utils;
using MorePlanning.Designators;
using MorePlanning.Plan;
using MorePlanning.Settings;
using MorePlanning.Utility;
using UnityEngine;
using Verse;
using ModSettings = MorePlanning.Settings.ModSettings;
using Resources = MorePlanning.Common.Resources;

namespace MorePlanning;

internal class MorePlanningMod : ModBase
{
    public const string Identifier = "com.github.alandariva.moreplanning";

    private static MorePlanningMod _instance;

    public ModSettings ModSettings;

    public int SelectedColor;

    private WorldSettings WorldSettings;

    private MorePlanningMod()
    {
        _instance = this;
    }

    public static MorePlanningMod Instance => _instance ?? throw new InvalidOperationException();

    public PlanInfoSet ClipboardPlan { get; set; }

    public bool PlanningVisibility
    {
        get => WorldSettings.PlanningVisibility;
        set
        {
            if (WorldSettings.PlanningVisibility == value)
            {
                return;
            }

            WorldSettings.PlanningVisibility = value;
            MenuUtility.GetPlanningDesignator<VisibilityCommand>()?.UpdateIcon(value);
        }
    }

    public override string ModIdentifier => "com.github.alandariva.moreplanning";

    public bool OverrideColors => (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) ==
                                  ModSettings.ShiftKeyForOverride;

    public static void AddDesignators()
    {
        var named = DefDatabase<DesignationCategoryDef>.GetNamed("Planning");
        if (named == null)
        {
            throw new Exception("Planning designation category not found");
        }

        var list = typeof(DesignationCategoryDef)
            .GetField("resolvedDesignators", BindingFlags.Instance | BindingFlags.NonPublic)
            ?.GetValue(named) as List<Designator>;
        if (list.OfType<SelectColorDesignator>().Any())
        {
            return;
        }

        for (var i = 0; i < 10; i++)
        {
            list.Add(new SelectColorDesignator(i));
        }
    }

    public override void DefsLoaded()
    {
        Resources.PlanDesignationDef = DefDatabase<DesignationDef>.GetNamed("MP_Plan");
        ModSettings = ModSettings.CreateModSettings(Settings);
        PlanColorManager.Load(Settings);
        SettingsChanged();
    }

    public static void LogError(string text)
    {
        Instance.Logger.Error(text);
    }

    public static void LogMessage(string text)
    {
        Instance.Logger.Message(text);
    }

    private void UpdatePlanningDefsSetting()
    {
        DefDatabase<DesignationDef>.GetNamed("MP_Plan").removeIfBuildingDespawned =
            ModSettings.RemoveIfBuildingDespawned;
    }

    public override void SettingsChanged()
    {
        UpdatePlanningDefsSetting();
        UpdatePlanOpacity();
    }

    private static void UpdatePlanOpacity()
    {
        PlanColorManager.InvalidateColors();
    }

    public override void WorldLoaded()
    {
        WorldSettings = UtilityWorldObjectManager.GetUtilityWorldObject<WorldSettings>();
        MenuUtility.GetPlanningDesignator<VisibilityCommand>().SelectedUpdate();
        MenuUtility.GetPlanningDesignator<OpacityCommand>().SelectedUpdate();
    }

    public class PlanningDataStore : UtilityWorldObject
    {
        private bool PlanningVisibility;

        public override void PostAdd()
        {
            base.PostAdd();
            PlanningVisibility = true;
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref PlanningVisibility, "planningVisibility", true);
        }
    }
}