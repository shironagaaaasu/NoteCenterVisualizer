using NoteCenterVisualizer.SphereModule;
using HarmonyLib;

namespace NoteCenterVisualizer.Patches
{
    [HarmonyPatch(typeof(SoloFreePlayFlowCoordinator), "SinglePlayerLevelSelectionFlowCoordinatorDidActivate")]
    internal class SoloFreePlayFlowCoordinatorPatch
    {
        private static void Postfix(bool firstActivation, bool addedToHierarchy)
        {
            SphereController.Instance.RefreshSpheres(isMenuScreen: true);
        }
    }
}
