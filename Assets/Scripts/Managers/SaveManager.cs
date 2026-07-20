using System.Collections;
using System.IO;
using UnityEngine;

public class SaveManager : ManagerBase
{
    
    private bool autoSaveEnabled;
    private float autoSaveInterval;
    private float autoSaveTimer;

    private const int AotoSaveOn = 0;
    private const int AotoSaveOff = 1;

    private string SavePath => Path.Combine(Application.persistentDataPath, "save.json");

    [System.Serializable] public class SaveData { public Vector3 playerPosition; }

    protected override IEnumerator OnConnected(GameManager newManager)
    {
        SettingManager.AutoSaveChanged -= OnAutoSaveChanged;
        SettingManager.AutoSaveChanged += OnAutoSaveChanged;

        SettingManager.AutoSaveIntervalChanged -= OnAutoSaveIntervalChanged;
        SettingManager.AutoSaveIntervalChanged += OnAutoSaveIntervalChanged;

        GameManager.OnUpdateManager -= AutoSaveUpdate;
        GameManager.OnUpdateManager += AutoSaveUpdate;

        yield return null;
    }

    protected override void OnDisconnected()
    {
        SettingManager.AutoSaveChanged -= OnAutoSaveChanged;
        SettingManager.AutoSaveIntervalChanged -= OnAutoSaveIntervalChanged;
        
        GameManager.OnUpdateManager -= AutoSaveUpdate;
    }

    private void OnAutoSaveChanged(int index)
    {
        autoSaveEnabled = index == AotoSaveOn;
    }

    private void OnAutoSaveIntervalChanged(int index)
    {
        switch (index)
        {
            case 0: autoSaveInterval = 60f; break;
            case 1: autoSaveInterval = 300f; break;
            case 2: autoSaveInterval = 600f; break;
            case 3: autoSaveInterval = 1200f; break;
            case 4: autoSaveInterval = 1800f; break;
        }

        autoSaveTimer = 0f;
    }
    public void SaveGame()
    {
        SaveData data = new SaveData();

        // data.playerPosition = PlayerController.Instance.transform.position;

        string json = JsonUtility.ToJson(data, true);

        File.WriteAllText(SavePath, json);

        Debug.Log($"저장 완료 : {SavePath}");
    }

    public void LoadGame()
    {
        if (!File.Exists(SavePath))
            return;

        string json = File.ReadAllText(SavePath);

        SaveData data = JsonUtility.FromJson<SaveData>(json);

        // PlayerController.Instance.transform.position =
        // data.playerPosition;

        Debug.Log("로드 완료");
    }

    private void AutoSaveUpdate(float deltaTime)
    {
        if (!autoSaveEnabled)
            return;

        autoSaveTimer += deltaTime;

        if (autoSaveTimer >= autoSaveInterval)
        {
            autoSaveTimer = 0f;

            SaveGame();
        }
    }
    
}
