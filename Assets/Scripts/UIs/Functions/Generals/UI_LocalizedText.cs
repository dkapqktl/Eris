using System;
using System.Collections;
using TMPro;
using UnityEngine;

using static LanguageManager;

public class UI_LocalizedText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TargetText;

    string originText;

    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        originText = TargetText.text;
        AutoTranslate();
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

    private void AutoTranslate()
    {
        if (string.IsNullOrEmpty(originText)) return;

        TargetText.text = GetText(originText);
    }



}
