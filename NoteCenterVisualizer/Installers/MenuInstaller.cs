using NoteCenterVisualizer.Menu;
using NoteCenterVisualizer.SphereModule;
using Zenject;

namespace NoteCenterVisualizer.Installers
{
    internal class MenuInstaller : Installer
    {

        public override void InstallBindings()
        {
            
            Container.Bind<SettingsViewController>().FromNewComponentAsViewController().AsSingle();
            
            // メインメニューにボタン、設定画面登録
            //Container.BindInterfacesTo<MenuButtonManager>().AsSingle();
            //Container.Bind<NoteCenterVisualizerFlowCoordinator>().FromNewComponentOnNewGameObject().AsSingle();


            // 曲選択画面左のMODタブに設定登録
            Container.BindInterfacesTo<GameplaySetupUI>().AsSingle();

            Container.Bind<SphereController>()
                .FromNewComponentOnNewGameObject()
                .AsSingle()
                .NonLazy();

            /*
            Container.Bind<SphereOnlyFlowCoordinator>()
                .FromNewComponentOnNewGameObject() // FlowCoordinator は GameObject にアタッチする
                .AsSingle()
                .NonLazy();
            */
        }
    }
}
