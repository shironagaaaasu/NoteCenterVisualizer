using System;
using HMUI;
using Zenject;

namespace NoteCenterVisualizer.Menu
{
    internal class NoteCenterVisualizerFlowCoordinator : FlowCoordinator
    {
        [Inject] private readonly SettingsViewController settingsViewController = null!;

        public event Action? DidFinish;

        protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            if (firstActivation)
            {
                showBackButton = true;
                SetTitle(nameof(NoteCenterVisualizer));
            }

            if (addedToHierarchy)
            {
                ProvideInitialViewControllers(settingsViewController);
            }
        }

        protected override void BackButtonWasPressed(ViewController topViewController)
        {
            DidFinish?.Invoke();
        }
    }
}
