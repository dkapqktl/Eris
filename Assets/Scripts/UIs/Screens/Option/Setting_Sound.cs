using TMPro;
using UnityEngine;

public class Setting_Sound : MonoBehaviour
{
    [SerializeField] TMP_Dropdown SoundDropdown;
    
    private void Awake()
    {
        GameManager.OnInitializeManager += Initialize;
    }

    private void Initialize()
    {

        SoundDropdown.value = SettingManager.CurrentLanguage;
    }
}
