using BeatSaberMarkupLanguage.MenuButtons;
using NoteCenterVisualizer.SphereModule;
using System;
using Zenject;

namespace NoteCenterVisualizer.Menu
{
    internal class MenuButtonManager : IInitializable, IDisposable
    {
        private readonly MainFlowCoordinator mainFlowCoordinator;
        private readonly NoteCenterVisualizerFlowCoordinator noteCenterVisualizerrFlowCoordinator;
        private readonly MenuButtons menuButtons;
        private readonly MenuButton menuButton;

        public MenuButtonManager(
        MainFlowCoordinator mainFlowCoordinator,
        NoteCenterVisualizerFlowCoordinator noteCenterVisualizerrFlowCoordinator,
        MenuButtons menuButtons)
        {
            this.mainFlowCoordinator = mainFlowCoordinator;
            this.noteCenterVisualizerrFlowCoordinator = noteCenterVisualizerrFlowCoordinator;
            this.menuButtons = menuButtons;
            menuButton = new(nameof(NoteCenterVisualizer), PresentFlowCoordinator);            
        }

        public void Initialize()
        {
            menuButtons.RegisterButton(menuButton);
            noteCenterVisualizerrFlowCoordinator.DidFinish += DismissFlowCoordinator;
        }

        public void Dispose()
        {
            noteCenterVisualizerrFlowCoordinator.DidFinish -= DismissFlowCoordinator;
        }

        private void PresentFlowCoordinator()
        {
            mainFlowCoordinator.PresentFlowCoordinator(noteCenterVisualizerrFlowCoordinator);
        }

        private void DismissFlowCoordinator()
        {
            mainFlowCoordinator.DismissFlowCoordinator(noteCenterVisualizerrFlowCoordinator);

            SphereController.Instance.RefreshSpheres(isMenuScreen: true);
        }
    }
}
