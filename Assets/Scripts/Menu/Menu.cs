using ModularMotion;
using System;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject settingsMenu;

    [SerializeField]
    private UIMotion uiMotion;

    [SerializeField]
    private Transform cameraTransform;

    [SerializeField]
    private Transform menuCameraPosition;

    [SerializeField]
    private Transform GameCameraPosition;

    [SerializeField]
    private GameObject playerCrab;

    [SerializeField]
    private Transform playerCrabStartPosition;

    [SerializeField]
    private GameObject aiCrab;

    [SerializeField]
    private Transform aiCrabStartPosition;

    [SerializeField]
    private UIMotion[] startEffects;

    private bool moveToGamePos;

    private bool moveToMenuPos;

    private void Start()
    {
        DisableSettingsPage();

        Invoke(nameof(TriggerUIAnims), 0.5f);

        cameraTransform.position = menuCameraPosition.position;
    }

    private void Update()
    {
        if (moveToGamePos)
        {
            cameraTransform.position = CookieUtils.Utils.Smootherstep(cameraTransform.position, GameCameraPosition.position, 0.13f);
            if (Vector3.Distance(cameraTransform.position, GameCameraPosition.position) <= 0.1f)
            {
                moveToGamePos = false;
            }
        }

        if (moveToMenuPos)
        {
            cameraTransform.position = CookieUtils.Utils.Smootherstep(cameraTransform.position, menuCameraPosition.position, 0.13f);
            if (Vector3.Distance(cameraTransform.position, menuCameraPosition.position) <= 0.1f)
            {
                moveToMenuPos = false;
            }
        }
    }

    private void TriggerUIAnims()
    {
        foreach (var startEffect in startEffects)
        {
            startEffect.PlayFromStartTillEnd();
        }
    }

    public void PlayGame()
    {
        playerCrab.transform.position = playerCrabStartPosition.position;
        aiCrab.transform.position = aiCrabStartPosition.position;
        moveToGamePos = true;
    }

    public void ReturnToMainMenu()
    {
        moveToMenuPos = true;
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