using System;
using System.Collections;
using TMPro;
using UnityEngine;

using static LanguageManager;

public class UI_LocalizedReceivedText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TargetText;
    [SerializeField] TMP_Dropdown from;

    string originText;
    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        originText = TargetText.text;
        // 게임을 시작하면 타겟 텍스(라벨)은 내가 적어둔 언어로 적혀있음
        AutoTranslate();
        if (from)
        {
            from.onValueChanged.AddListener(OnFromChanged);
            // from.onValueChanged.AddListener(Setting_GameSet.OnlanguageUIChange);
            // OnlanguageUIChange에 static을 쓰면 이것도 가능함
        }
    }

    private void OnEnable()
    {
        AutoTranslate();

        OnLanguageTextChange -= AutoTranslate;
        OnLanguageTextChange += AutoTranslate;
    }

    private void OnDisable()
    {
        OnLanguageTextChange -= AutoTranslate;
    }

    void OnFromChanged(int value)
    {
        originText = TargetText.text;
        AutoTranslate();
    }

    private void AutoTranslate()
    {
        if (string.IsNullOrEmpty(originText)) return;

        TargetText.text = GetText(originText);

        Debug.Log($"originText = {originText}");
    }
}
