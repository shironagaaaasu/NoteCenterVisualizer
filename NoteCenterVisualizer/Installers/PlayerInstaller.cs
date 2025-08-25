using NoteCenterVisualizer.SphereModule;
using Zenject;

namespace NoteCenterVisualizer.Installers
{
    internal class PlayerInstaller : Installer
    {
        public override void InstallBindings()
        {
            SphereController.Instance.RefreshSpheres(isMenuScreen: false);
        }
    }
}
