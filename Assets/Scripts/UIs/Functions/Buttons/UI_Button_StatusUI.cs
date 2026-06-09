using System;
using UnityEngine;
using UnityEngine.UI;


public class UI_Button_StatusUI : MonoBehaviour
{
    private StatusModule status;

    [SerializeField] private Button strButton;
    [SerializeField] private Button dexButton;
    [SerializeField] private Button intButton;
    [SerializeField] private Button resetButton;


    private void UpdateButtons()
    {
        bool canUseStatusButton = status.CanUseStatusPoint;
        bool canUseResetButton = status.CanUseReset;

        strButton.interactable = canUseStatusButton; // SP가 없을때 버튼 비활성화
        dexButton.interactable = canUseStatusButton;
        intButton.interactable = canUseStatusButton;
        resetButton.interactable = canUseResetButton;
    }


    public void STRButton()
    {
        status.IncreaseStrength();

        UpdateButtons();
    }

    public void INTButton()
    {
        status.IncreaseDexterity();

        UpdateButtons();
    }

    public void DEXButton()
    {
        status.IncreaseIntelligence();

        UpdateButtons();
    }

    public void ResetButton()
    {
        status.ResetStatusPoint();

        UpdateButtons();
    }


}
