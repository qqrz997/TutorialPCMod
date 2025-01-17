using BeatSaberMarkupLanguage.MenuButtons;
using Zenject;

namespace MissTextChanger.Menu;

internal class MenuButtonManager : IInitializable
{
    private readonly MainFlowCoordinator mainFlowCoordinator;
    private readonly MissTextChangerFlowCoordinator missTextChangerFlowCoordinator;
    private readonly MenuButtons menuButtons;
    private readonly MenuButton menuButton;

    public MenuButtonManager(
        MainFlowCoordinator mainFlowCoordinator,
        MissTextChangerFlowCoordinator missTextChangerFlowCoordinator, 
        MenuButtons menuButtons)
    {
        this.mainFlowCoordinator = mainFlowCoordinator;
        this.missTextChangerFlowCoordinator = missTextChangerFlowCoordinator;
        this.menuButtons = menuButtons;
        menuButton = new(nameof(MissTextChanger), PresentFlowCoordinator);
    }

    public void Initialize()
    {
        menuButtons.RegisterButton(menuButton);
    }

    private void PresentFlowCoordinator()
    {
        missTextChangerFlowCoordinator.DidFinish += DismissFlowCoordinator;
        mainFlowCoordinator.PresentFlowCoordinator(missTextChangerFlowCoordinator);
    }

    private void DismissFlowCoordinator()
    {
        missTextChangerFlowCoordinator.DidFinish -= DismissFlowCoordinator;
        mainFlowCoordinator.DismissFlowCoordinator(missTextChangerFlowCoordinator);
    }
}