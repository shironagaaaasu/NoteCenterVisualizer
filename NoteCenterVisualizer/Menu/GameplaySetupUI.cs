using BeatSaberMarkupLanguage.GameplaySetup;
using System;
using Zenject;

namespace NoteCenterVisualizer.Menu
{
    internal class GameplaySetupUI : IInitializable, IDisposable
    {
        private readonly SettingsViewController _controller;

        public GameplaySetupUI(SettingsViewController controller)
        {
            _controller = controller;
        }

        public void Initialize()
        {
            Plugin.Log?.Debug("GameplaySetupUI Initialize()");
            GameplaySetup.Instance.AddTab(
                "NoteCenterVisualizer",
                "NoteCenterVisualizer.Menu.settingsView.bsml",
                _controller
            );
        }

        public void Dispose()
        {
            GameplaySetup.Instance.RemoveTab("NoteCenterVisualizer");
        }
    }
}
