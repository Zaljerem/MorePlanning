using System.Linq;
using MorePlanning.Settings;
using RimWorld.Planet;
using Verse;

namespace MorePlanning.Legacy;

internal class UpdateLegacy
{
	public static void Update()
	{
		UpdateTo_4_1_0();
	}

	private static void UpdateTo_4_1_0()
	{
		MorePlanningMod.PlanningDataStore planningDataStore = (MorePlanningMod.PlanningDataStore)Find.WorldObjects.ObjectsAt(0).FirstOrDefault((WorldObject o) => o is MorePlanningMod.PlanningDataStore);
		if (planningDataStore != null)
		{
			WorldSettings worldSettings = (WorldSettings)Find.WorldObjects.ObjectsAt(0).FirstOrDefault((WorldObject o) => o is WorldSettings);
			if (worldSettings != null)
			{
				worldSettings.PlanningVisibility = planningDataStore.PlanningVisibility;
			}
			Find.WorldObjects.Remove(planningDataStore);
		}
	}
}
