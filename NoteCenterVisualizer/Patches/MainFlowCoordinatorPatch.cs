using HarmonyLib;
using NoteCenterVisualizer.SphereModule;

namespace NoteCenterVisualizer.Patches
{

    [HarmonyPatch(typeof(MainFlowCoordinator), "DidActivate")]
    internal class MainFlowCoordinatorPatch
    {

        //static void Postfix(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        static void Postfix(bool firstActivation, bool addedToHierarchy, MainFlowCoordinator __instance)
        {
            SphereController.Instance.RefreshSpheres(isMenuScreen: true);
        }
    }
}
