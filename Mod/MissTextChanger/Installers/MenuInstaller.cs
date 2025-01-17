using MissTextChanger.Menu;
using Zenject;

namespace MissTextChanger.Installers;

internal class MenuInstaller : Installer
{
    public override void InstallBindings()
    {
        Container.BindInterfacesTo<MenuButtonManager>().AsSingle();
        Container.Bind<MissTextChangerFlowCoordinator>().FromNewComponentOnNewGameObject().AsSingle();
        Container.Bind<SettingsViewController>().FromNewComponentAsViewController().AsSingle();
    }
}