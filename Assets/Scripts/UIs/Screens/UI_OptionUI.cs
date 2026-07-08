using UnityEngine;

public class UI_OptionUI : OpenableUIBase
{
    [SerializeField] private GameObject setGraphics;
    [SerializeField] private GameObject setController;
    [SerializeField] private GameObject setGameSet;
    [SerializeField] private GameObject setUISet;
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
}
