using UnityEngine;

public class OpenPauseUI : OpenableUIBase
{
    private void OnEnable()
    {
        GameManager.Pause();
    }

    private void OnDisable()
    {
        GameManager.UnPause();
    }
}
