using HugsLib.Utils;
using Verse;

namespace MorePlanning.Settings;

internal class WorldSettings : UtilityWorldObject
{
    public bool PlanningVisibility;

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