using Unity.VisualScripting;
using UnityEngine;

public class UI_LoadingScreen : UIBase, IOpenable, IProgress<int>, IStatus<string>
{
    // 프로퍼티를 만들때 원본이 되는 변수를 만들어줬었으나 get; set; 만 있는 경우 그냥 변수처럼 사용가능 // set만 protected 인 변수처럼 활용
    public bool IsOpen => gameObject.activeSelf;

    public int Current { get; set; }

    public int Max { get; set; }

    

    public float Progress => Max != 0 ? (float)Current / Max : 0.0f;

    public int AddCurrent(int value) => Set(Current + value, Max);

    public int AddMax(int value) => Set(Current, Max + value);

    public void Open() => gameObject.SetActive(true);


    public void Close() => gameObject.SetActive(false);

    // 함수는 함수끼리 식으로 프로퍼티, 변수도 마찬가지 끼리끼리 있어야함, 변수는 크기가 큰 순서가 앞
    public UnityEngine.UI.Slider progressBar;
    public TMPro.TextMeshProUGUI progressText;
    public TMPro.TextMeshProUGUI statusText;

    public string SetCurrentStatus(string newText)
    {
        
        statusText.SetText(newText);
        
        return newText;

        
    }

    // IStatus
    public int Set(int newCurrent)
    {
        Current = Mathf.Min(newCurrent, Max);
        progressBar.value = Progress;
        progressText.SetText($"{Progress*100.0f : 0.00}%"); // 0.00 => C언어에서 .2f 같은것.
        return Current;
    }
    public int Set(int newCurrent, int newMax)
    {
        Max = newMax;
        return Set(newCurrent);
    }

   


    public void Toggle() => gameObject.SetActive(!IsOpen);


}
