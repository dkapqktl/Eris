using UnityEngine;

public class UI_OptionUI : OpenableUIBase
{
    [SerializeField] private GameObject setGraphics;
    [SerializeField] private GameObject setController;
    [SerializeField] private GameObject setGameSet;
    [SerializeField] private GameObject setUISet;

    [SerializeField] GameObject[] Tabs;


    public int currentTab;


    private void OnEnable()
    {
        InputManager.OnCancel -= CancelMenu;
        InputManager.OnCancel += CancelMenu;
    }

    private void OnDisable()
    {
        InputManager.OnCancel -= CancelMenu;
    }

    void CancelMenu(bool value)
    {
        if (!value)
            return;

        setGraphics.SetActive(false);
        setController.SetActive(false);
        setGameSet.SetActive(false);
        setUISet.SetActive(false);

        if (UIManager.ClaimGetUI(UIType.Option).isActiveAndEnabled)
        {
            UIManager.ClaimCloseUI(UIType.Option);
        }
    }

    public void SettingTab(int index)
    {
        currentTab = index;
        for (int i = 0; i < Tabs.Length; i++)
        {
            Tabs[i].SetActive(index == i);
        }
    }

    public void CurrentTabReset()
    {
        switch (currentTab)
        {
            case 0: SettingManager.GraphicSettingReset(); break;
        }
    }
}
