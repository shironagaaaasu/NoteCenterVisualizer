using HarmonyLib;
using NoteCenterVisualizer.SphereModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteCenterVisualizer.Patches
{
	[HarmonyPatch(typeof(PlayerHeightSettingsController), "RefreshUI")]
	internal class PlayerHeightSettingsControllerPatch
	{
		static void Postfix(PlayerHeightSettingsController __instance)
		{
			SphereController.Instance.GameHeight = __instance.value;
			if (PluginConfig.Instance.AutoSetHeight)
			{
				SphereController.Instance.RefreshSpheres(isMenuScreen: true);
			}
		}
	}
}
