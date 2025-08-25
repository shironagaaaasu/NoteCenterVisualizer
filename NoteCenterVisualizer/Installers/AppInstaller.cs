﻿using NoteCenterVisualizer.Menu;
using Zenject;

namespace NoteCenterVisualizer.Installers
{
    internal class AppInstaller : Installer
    {
        private readonly PluginConfig _pluginConfig;

        public AppInstaller(PluginConfig pluginConfig)
        {
            _pluginConfig = pluginConfig;
        }

        public override void InstallBindings()
        {
            Container.BindInstance(_pluginConfig).AsSingle().NonLazy();
        }
    }
}
