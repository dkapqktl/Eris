using System;
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

    [SerializeField] public TextMeshProUGUI GraphicButtonText;
    [SerializeField] public TextMeshProUGUI ControllerButtonText;
    [SerializeField] public TextMeshProUGUI GameplayButtonText;
    [SerializeField] public TextMeshProUGUI SoundButtonText;
    [SerializeField] public TextMeshProUGUI ResetButtonText;

    [SerializeField] GameObject[] Tabs;

    public TextMeshProUGUI YNResetButtonTexts;

    public static int defaultTab = 999;
    public static int currentTab = 999;


    private void OnEnable()
    {
        InputManager.OnCancel -= CancelMenu;
        InputManager.OnCancel += CancelMenu;

        LanguageManager.OnLanguageTextChange -= ChangeText;
        LanguageManager.OnLanguageTextChange += ChangeText;



        ChangeText();
        AllResetTextChange(currentTab);
    }

    private void OnDisable()
    {
        InputManager.OnCancel -= CancelMenu;
        
        LanguageManager.OnLanguageTextChange -= ChangeText;

        SettingTab(defaultTab);
    }

    public void ChangeText()
    {
        GraphicButtonText.text = LanguageManager.GetText    ("GraphicButton");
        ControllerButtonText.text = LanguageManager.GetText ("ControllerButton");
        GameplayButtonText.text = LanguageManager.GetText   ("GameplayButton");
        SoundButtonText.text = LanguageManager.GetText      ("SoundButton");
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

        int index = currentTab;

        YNResetText(index);
    }

    public void YNResetText(int index)
    {
        switch (index)
        {
            case 0: YNResetButtonTexts.text = LanguageManager.GetText("Initialize Graphic Confirm"); break;
            case 1: YNResetButtonTexts.text = LanguageManager.GetText("Initialize Controller Confirm"); break;
            case 2: YNResetButtonTexts.text = LanguageManager.GetText("Initialize Gameplay Confirm"); break;
            case 3: YNResetButtonTexts.text = LanguageManager.GetText("Initialize Sound Confirm"); break;
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

        AllResetTextChange(currentTab);
    }

    public void AllResetTextChange(int index)
    {
        switch (index)
        {
            case 0: ResetButtonText.text = LanguageManager.GetText("All Reset Graphic Button"); ResetButton.interactable = true; break;
            case 1: ResetButtonText.text = LanguageManager.GetText("All Reset Controller Button"); ResetButton.interactable = true; break;
            case 2: ResetButtonText.text = LanguageManager.GetText("All Reset Gameplay Button"); ResetButton.interactable = true; break;
            case 3: ResetButtonText.text = LanguageManager.GetText("All Reset Sound Button"); ResetButton.interactable = true; break;
            default : ResetButtonText.text = LanguageManager.GetText("Reset"); ResetButton.interactable = false; break;
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
