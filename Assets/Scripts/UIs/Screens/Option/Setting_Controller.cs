using System.Collections;
using UnityEngine;

public class Setting_Controller : MonoBehaviour
{
    /*
    public InputActionReference InventoryAction;
    public TMP_Text InventoryText;

    public InputActionReference UpAction;
    public TMP_Text UpText;

    public InputActionReference DownAction;
    public TMP_Text DownText;

    public InputActionReference LeftAction;
    public TMP_Text LeftText;

    public InputActionReference RightAction;
    public TMP_Text RightText;
    */
    
    public Transform Content;

    public enum InputType
    {
        Up, Down, Left, Right, Inventory
    }

    [System.Serializable]public struct ActionSetter
    {
        [SerializeField]public string DisplayName;
        [SerializeField]public string ActionName;
        [SerializeField]public InputType SettingType;
    }

    public ActionSetter[] Setter;

    public void ControllerSettingUIReset()
    {
        Setting_GameSet.LanguageDropdown.value = 0;
        Setting_GameSet.AutoSaveToggle.isOn = true;
        Setting_GameSet.AutoSaveIntervalDropdown.value = 1;
        Setting_GameSet.MiniMapToggle.isOn = true;
        Setting_GameSet.TimeToggle.isOn = true;
        Setting_GameSet.AutoLootToggle.isOn = true;
        Setting_GameSet.CameraShakeToggle.isOn = true;
    }

    private IEnumerator Start()
    {
        while (GameManager.Input == null)
            yield return null;

        foreach (var current in Setter)
        {
            GameObject instance = ObjectManager.CreateObject("Set_Key", Content);

            KeySetter currentSetter = instance.GetComponent<KeySetter>();

            if (currentSetter == null)
                continue;

            currentSetter.Initialized(current);
        }
    }



    // public void ChangeInvetoryKey()
    // {
    //     ChangeKey(InventoryAction, InventoryText);
    // }

    /* 이렇게 하면 나중에 수정할때 가독성 이슈로 매우 하드코어가 되니 가급적 하지말도록
    public InputActionReference[] CurrentAction;
    public TMP_Text[] CurrentText;
    
    public int currentKey;
    
    public void CurrentChangeKey(int index)
    {
        ChangeKey(CurrentAction[index], CurrentText[index]);
    }
    */

}

