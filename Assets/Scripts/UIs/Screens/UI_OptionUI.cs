using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_OptionUI : OpenableUIBase
{
    [SerializeField] private GameObject setGraphics;
    [SerializeField] private GameObject setController;
    [SerializeField] private GameObject setGameSet;
    [SerializeField] private GameObject setUISet;
    [SerializeField] private GameObject ConfirmUI;
    [SerializeField] private Button ResetButton;

    [SerializeField] GameObject[] Tabs;

    public TextMeshProUGUI ResetButtonTexts;
    public TextMeshProUGUI YNResetButtonTexts;

    public static int defaultTab = 999;
    public static int currentTab = 999;


    private void OnEnable()
    {
        InputManager.OnCancel -= CancelMenu;
        InputManager.OnCancel += CancelMenu;
    }

    private void OnDisable()
    {
        InputManager.OnCancel -= CancelMenu;
        SettingTab(defaultTab);
    }

    void CancelMenu(bool value)
    {
        if (!value) return;

        setGraphics.SetActive(false);
        setController.SetActive(false);
        setGameSet.SetActive(false);
        setUISet.SetActive(false);

        if (UIManager.ClaimGetUI(UIType.Option).isActiveAndEnabled)
        {
            UIManager.ClaimCloseUI(UIType.Option);
        }
    }

    public void OpenConfirmBox()
    {
        if (currentTab == defaultTab) return;
        ConfirmUI.SetActive(true);

        
        switch (currentTab)
        {
            case 0: YNResetButtonTexts.text = "그래픽 설정을 초기화 하시겠습니까?"; break;
            case 1: YNResetButtonTexts.text = "컨트롤러 설정을 초기화 하시겠습니까?"; break;
            case 2: YNResetButtonTexts.text = "게임설정을 초기화 하시겠습니까?"; break;
            case 3: YNResetButtonTexts.text = "사운드를 초기화 하시겠습니까?"; break;
        }
    }

    public void CloseConfirmBox()
    {
        ConfirmUI.SetActive(false);
    }

    public void SettingTab(int index)
    {
        currentTab = index;
        for (int i = 0; i < Tabs.Length; i++)
        {
            Tabs[i].SetActive(index == i);
        }

        switch (index)
        {
            case 0: ResetButtonTexts.text = "그래픽 전체 초기화"; ResetButton.interactable = true; break;
            case 1: ResetButtonTexts.text = "컨트롤러 전체 초기화"; ResetButton.interactable = true; break;
            case 2: ResetButtonTexts.text = "게임설정 전체 초기화"; ResetButton.interactable = true; break;
            case 3: ResetButtonTexts.text = "사운드 전체 초기화"; ResetButton.interactable = true; break;
            case 999: ResetButtonTexts.text = "초기화"; ResetButton.interactable = false; break;
        }
    }

    public void CurrentTabReset()
    {
        switch (currentTab)
        {
            case 0: SettingManager.GraphicSettingReset(); break;
            case 1: SettingManager.ControllerSettingReset(); break;
            case 2: SettingManager.GameplaySettingReset(); break;
            case 3: SettingManager.SoundSettingReset(); break;
        }

        CloseConfirmBox();
    }
}
