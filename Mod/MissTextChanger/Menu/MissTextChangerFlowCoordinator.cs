using System;
using HMUI;
using Zenject;

namespace MissTextChanger.Menu;

internal class MissTextChangerFlowCoordinator : FlowCoordinator
{
    [Inject] private readonly SettingsViewController settingsViewController = null!;

    public event Action? DidFinish;
    
    protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
    {
        if (firstActivation)
        {
            showBackButton = true;
            SetTitle(nameof(MissTextChanger));
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