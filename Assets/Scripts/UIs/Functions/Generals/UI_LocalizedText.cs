using System;
using TMPro;
using UnityEngine;

using static LanguageManager;

public class UI_LocalizedText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TargetText;

    string originText;

    private void Awake()
    {
        originText = TargetText.text;
        AutoTranslate();
    }

    private void OnEnable()
    {
        OnLanguageTextChange -= AutoTranslate;
        OnLanguageTextChange += AutoTranslate;
    }

    private void OnDisable()
    {
        OnLanguageTextChange -= AutoTranslate;
    }

    private void AutoTranslate()
    {
        TargetText.text = GetText(originText);
    }



}
