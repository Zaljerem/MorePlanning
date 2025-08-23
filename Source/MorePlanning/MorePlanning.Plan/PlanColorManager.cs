using HugsLib.Settings;
using MorePlanning.Utility;
using Multiplayer.API;
using UnityEngine;
using Resources = MorePlanning.Common.Resources;

namespace MorePlanning.Plan;

public class PlanColorManager
{
    public const int NumPlans = 10;

    private static readonly Color[] PlanColor = new Color[10];

    private static readonly bool[] PlanColorChanged = new bool[10];

    private static readonly SettingHandle<string>[] _planColorSetting = new SettingHandle<string>[10];

    public static readonly string[] DefaultColors =
    [
        "a9a9a9", "2095f2", "4bae4f", "f34235", "feea3a", "ff00f0", "00fffc", "8400ff", "ffa200", "000000"
    ];

    private static readonly int color = Shader.PropertyToID("_Color");

    private static string GetDefaultColor(int i)
    {
        return DefaultColors[i];
    }

    public static void Load(ModSettingsPack settings)
    {
        for (var i = 0; i < 10; i++)
        {
            _planColorSetting[i] =
                settings.GetHandle($"planColor{i}", $"planColor{i}", $"planColor{i}", GetDefaultColor(i));
            _planColorSetting[i].NeverVisible = true;
        }

        for (var j = 0; j < 10; j++)
        {
            OnColorChanged(j);
        }
    }

    [SyncMethod]
    public static void ChangeColor(int colorNum, string hexColor)
    {
        _planColorSetting[colorNum].Value = hexColor;
        OnColorChanged(colorNum);
    }

    private static void OnColorChanged(int numColor = -1)
    {
        PlanColor[numColor] = _planColorSetting[numColor].Value.HexToColor();
        PlanColorChanged[numColor] = true;
    }

    public static void InvalidateColors()
    {
        for (var i = 0; i < PlanColorChanged.Length; i++)
        {
            PlanColorChanged[i] = true;
        }
    }

    public static Color GetColor(int col = -1)
    {
        if (col < 0)
        {
            col = MorePlanningMod.Instance.SelectedColor;
        }

        return PlanColor[col];
    }

    public static Material GetMaterial(int numColor)
    {
        if (!PlanColorChanged[numColor])
        {
            return Resources.PlanMatColor[numColor];
        }

        PlanColorChanged[numColor] = false;
        var value = PlanColor[numColor];
        value.a = MorePlanningMod.Instance.ModSettings.PlanOpacity / 100f;
        Resources.PlanMatColor[numColor].SetColor(color, value);

        return Resources.PlanMatColor[numColor];
    }
}