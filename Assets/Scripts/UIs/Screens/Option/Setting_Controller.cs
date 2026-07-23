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

}

