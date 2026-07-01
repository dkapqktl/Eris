using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class DBManager : ManagerBase
{
    FirebaseAuth authentication;
    private FirebaseUser user;
    private DatabaseReference rootDB;

    protected override IEnumerator OnConnected(GameManager newManager)
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(InitializeFireBase);
        yield return null;
    }

    protected override void OnDisconnected()
    {

    }

    void InitializeFireBase(Task<DependencyStatus> task)
    {
        if (task.Result == DependencyStatus.Available)
        {
            authentication = FirebaseAuth.DefaultInstance;

            user = authentication.CurrentUser;

            rootDB = FirebaseDatabase.DefaultInstance.RootReference;

            GuestLogin();

            Debug.Log("FireBase Initialized");
        }

        else
        {
            Debug.Log($"Fail to Initialize Firebase : {task.Exception}");
        }
    }


    public TMPro.TMP_InputField nickNameInput;

    public void MakeUserData(string newUserName)
    {
        WriteData(MakeNewUserData(nickNameInput.text), "user", "userData", user.UserId);
    }

    public async void GuestLogin()
    {
        if (authentication is null) return;

        if (user is not null)
        {
            Debug.LogError($"Login Falled : Already Has Login Data ({user.IsValid()}, {user.UserId})");
            UserData resultData = await ReadDataAsync<UserData>("user", "userData", user.UserId);
            if (resultData is not null)
            {
                Debug.Log(resultData.nickname);
            }
            else
            {
                WriteData(MakeNewUserData("NoNamed"), "users", "userData", user.UserId);
            }

                return;
        }

        await authentication.SignInAnonymouslyAsync().ContinueWithOnMainThread(OnLoginResult);
    }

    private void OnLoginResult(Task<AuthResult> task)
    {
        if (task.IsCanceled || task.IsFaulted)
        {
            Debug.LogError($"Fail to Sign in : {task.Exception}");
            return;
        }

        user = task.Result.User;
        WriteData(MakeNewUserData("고라자니"), "user", "userData");
        Debug.Log($"Sign in Succesed :{user.UserId}");
    }


    [Serializable]
    public class UserData
    {
        public string nickname;
        public DateTime assignData;
        public int userLevel;
        public int money;
        public int attendtime;
    }

    public UserData MakeNewUserData(string wantNickname) => new()
    {
        nickname    = wantNickname,
        assignData  = DateTime.Now,
        userLevel   = 1,
        money       = 9999,
        attendtime  = 0
    };


    public DatabaseReference GetFinalDirectory(DatabaseReference root, params string[] directory)
    {
        if (directory is null || directory.Length == 0) return root;
        DatabaseReference currentReference = root;
        foreach (string currentChild in directory)
        {
            currentReference = currentReference.Child(currentChild);
        }
        return currentReference;
    }


    // params : 몇개를 전달 받든 다 받을 수 있음
    // ex) WriteData(wantData, 1) 이든 WriteData(wantData, 1,2,3) 이든 다 받을 수 있음
    private void WriteData(object wantData, params string[] directory)
    {
        if (rootDB is null || wantData is null) return;

        string jsonData = JsonUtility.ToJson(wantData);

        GetFinalDirectory(rootDB, directory).SetRawJsonValueAsync(jsonData).ContinueWithOnMainThread(OnTaskResult);
        // 포이치 부분이 아래 코드랑 같음, 다만 차일드가 몇명일지 모르니 위방식 포이치로 돌리는 형태
        // rootDB.Child("item").Child("Misc").Child("Nature").Child("Stone").UpdateChildrenAsync(item).ContinueWithOnMainThread(OnTaskResult);
    }

    public void WriteData(Dictionary<string, object> changes, params string[] directory)
    {
        if (rootDB is null || changes is null) return;

        GetFinalDirectory(rootDB, directory).UpdateChildrenAsync(changes).ContinueWithOnMainThread(OnTaskResult);
    }

    public void ReadData(Action<Task<DataSnapshot>> OnReadData, params string[] directory)
    {
        GetFinalDirectory(rootDB, directory).GetValueAsync().ContinueWithOnMainThread(OnReadData);
    }

    public IEnumerator ReadDataCoroutine(Action<Task<DataSnapshot>> OnReadData, params string[] directory)
    {
        Task<DataSnapshot> readTask = GetFinalDirectory(rootDB, directory).GetValueAsync();
        yield return readTask.WaitForTask();
        OnReadData?.Invoke(readTask);
    }

    public async Task<T> ReadDataAsync<T>(params string[] directory)
    {
        DataSnapshot currentTask = await GetFinalDirectory(rootDB, directory).GetValueAsync();
        if (currentTask is null) return default;
        if (!currentTask.Exists) return default;


        // 1. 복합타입
        try
        {
            if (currentTask.HasChildren)
            {
                return JsonUtility.FromJson<T>(currentTask.GetRawJsonValue());
            }

            // 2. 단일타입
            return (T)System.Convert.ChangeType(currentTask.Value, typeof(T));
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            return default;
        }
    }


    private void OnTaskResult(Task task)
    {
        if (task.IsCanceled || task.IsFaulted)
        {
            Debug.LogError(task.Exception);
        }
    }
}
