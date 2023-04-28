using ModularMotion;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject settingsMenu;

    [SerializeField]
    private UIMotion uiMotion;

    private void Start()
    {
        DisableSettingsPage();
    }

    public void PlayGame()
    {

    }

    public void OpenSettings()
    {
        settingsMenu.SetActive(true);
        uiMotion.PlayFromStartTillEnd();
    }

    public void CloseSettings()
    {
        uiMotion.OnEnd.AddListener(DisableSettingsPage);
        uiMotion.PlayAllBackward();
    }

    private void DisableSettingsPage()
    {
        settingsMenu.SetActive(false);
        uiMotion.OnEnd.RemoveListener(DisableSettingsPage);
    }
}