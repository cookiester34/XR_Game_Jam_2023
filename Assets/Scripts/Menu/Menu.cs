using ModularMotion;
using System.Threading.Tasks;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public static Menu Instance;

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
    private GameObject MenuCrab;

    [SerializeField]
    private UIMotion[] startEffects;

    [SerializeField]
    private HealthBar playerHealthBar;

    [SerializeField]
    private HealthBar aiHealthBar;

    [SerializeField]
    private Animator animatorRef;

    private bool moveToGamePos;

    private bool moveToMenuPos;

    private void Awake()
    {
        Instance ??= this;
    }

    private void Start()
    {
        DisableSettingsPage();

        Invoke(nameof(TriggerUIAnims), 0.5f);

        cameraTransform.position = menuCameraPosition.position;

        MenuCrab.SetActive(true);
        aiCrab.SetActive(false);
        playerCrab.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (moveToGamePos)
        {
            cameraTransform.position = CookieUtils.Utils.Smootherstep(cameraTransform.position, GameCameraPosition.position, 0.23f);
            if (Vector3.Distance(cameraTransform.position, GameCameraPosition.position) <= 0.1f)
            {
                moveToGamePos = false;
            }
        }

        if (moveToMenuPos)
        {
            cameraTransform.position = CookieUtils.Utils.Smootherstep(cameraTransform.position, menuCameraPosition.position, 0.23f);
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

    public async void PlayGame()
    {
        moveToMenuPos = false;
        playerCrab.transform.position = playerCrabStartPosition.position;
        aiCrab.transform.position = aiCrabStartPosition.position;
        moveToGamePos = true;

        MenuCrab.SetActive(false);
        aiCrab.SetActive(true);
        playerCrab.SetActive(true);

        playerCrab.GetComponent<Animator>().SetTrigger("Reset");
        aiCrab.GetComponent<Animator>().SetTrigger("Reset");

        playerHealthBar.Reset();
        aiHealthBar.Reset();

        Invoke(nameof(ActivateAI), 2f);
    }

    private void ActivateAI()
    {
        aiCrab.GetComponent<AiController>().Active = true;
    }

    public void ReturnToMainMenu()
    {
        moveToMenuPos = true;
        MenuCrab.SetActive(true);
        aiCrab.SetActive(false);
        playerCrab.SetActive(false);

        aiCrab.GetComponent<AiController>().Active = false;
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