using System;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public delegate void StatusEvent();

public class UI_Button_StatusUI : MonoBehaviour
{
    public event StatusEvent OnStatusEvent;

    private StatusModule status;

    [SerializeField] private Button strButton;
    [SerializeField] private Button dexButton;
    [SerializeField] private Button intButton;
    [SerializeField] private Button resetButton;


    private void Awake()
    {
        //매니저가 로딩 끝나고 나서 초기화 할 거 써놓기!
        GameManager.OnInitializeManager += CharacterStart;
    }

    void CharacterStart()
    {
        status = CharacterBase.localPlayer.GetModule<StatusModule>();
        status.OnStatusChanged += UpdateButtons;
    }



    public void UpdateButtons()
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

        if (InputManager.IsShift) { status.FiveIncreaseStrength(); }
        else { status.IncreaseStrength(); }

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
