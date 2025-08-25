using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;
using NoteCenterVisualizer.SphereModule;
using Zenject;

namespace NoteCenterVisualizer.Menu
{
    [HotReload(RelativePathToLayout = @".\settingsView.bsml")]
    [ViewDefinition("NoteCenterVisualizer.Menu.settingsView.bsml")]
    internal class SettingsViewController : BSMLAutomaticViewController
    {
        [UIValue("Enabled")]
        private bool Enabled
        {
            get => PluginConfig.Instance.Enabled;
            set => PluginConfig.Instance.Enabled = value;
        }

        [UIValue("InGame")]
        private bool InGame
        {
            get => PluginConfig.Instance.InGame;
            set => PluginConfig.Instance.InGame = value;
        }

        [UIValue("InMenu")]
        private bool InMenu
        {
            get => PluginConfig.Instance.InMenu;
            set => PluginConfig.Instance.InMenu = value;
        }

        [UIValue("MyHeight")]
        public float MyHeight
        {
            get => PluginConfig.Instance.MyHeight;
            set => PluginConfig.Instance.MyHeight = value;
        }

        [UIValue("ZPosition")]
        public float ZPosition
        {
            get => PluginConfig.Instance.ZPosition;
            set => PluginConfig.Instance.ZPosition = value;
        }

        [UIValue("SphereSize")]
        public float SphereSize
        {
            get => PluginConfig.Instance.SphereSize;
            set => PluginConfig.Instance.SphereSize = value;
        }

        [UIValue("ShowPanel")]
        private bool ShowPanel
        {
            get => PluginConfig.Instance.ShowPanel;
            set => PluginConfig.Instance.ShowPanel = value;
        }

        [UIAction("ChangeEnabled")]
        protected void ChangeEnabled(bool value)
        {
            PluginConfig.Instance.Enabled = value;

            SphereController.Instance.RefreshSpheres(isMenuScreen: true);
            SphereController.Instance.RefreshPlane(isMenuScreen: true);
        }

        [UIAction("ChangeInMenu")]
        protected void ChangeInMenu(bool value)
        {
            PluginConfig.Instance.InMenu = value;

            SphereController.Instance.RefreshSpheres(isMenuScreen: true);
            SphereController.Instance.RefreshPlane(isMenuScreen: true);
        }

        [UIAction("ChangeHeight")]
        protected void ChangeHeight(float value)
        {
            PluginConfig.Instance.MyHeight = value;

            SphereController.Instance.RefreshSpheres(isMenuScreen: true);
        }

        [UIAction("ChangeZPosition")]
        protected void ChangeZPosition(float value)
        {
            PluginConfig.Instance.ZPosition = value;

            SphereController.Instance.RefreshSpheres(isMenuScreen: true);
        }

        [UIAction("ChangeSphereSize")]
        protected void ChangeSphereSize(float value)
        {
            PluginConfig.Instance.SphereSize = value;

            SphereController.Instance.RefreshSpheres(isMenuScreen: true);
        }

        [UIAction("ChangeShowPanel")]
        protected void ChangeShowPanel(bool value)
        {
            PluginConfig.Instance.ShowPanel = value;

            SphereController.Instance.RefreshPlane(isMenuScreen: true);
        }
    }
}
