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

    private void OnTaskResult(Task task)
    {
        if (task.IsCanceled || task.IsFaulted)
        {
            Debug.LogError(task.Exception);
        }
    }


    public TMPro.TMP_InputField nickNameInput;

    public void MakeUserData(string newUserName)
    {
        WriteData(MakeNewUserData(nickNameInput.text), "user", "userData", user.UserId);
    }

    public void GuestLogin()
    {
        if (authentication is null) return;

        if (user is not null)
        {
            Debug.LogError($"Login Falled : Already Has Login Data ({user.IsValid()}, {user.UserId})");
            WriteData(MakeNewUserData("고라자니"), "user", "userData", user.UserId);
            return;
        }

        authentication.SignInAnonymouslyAsync().ContinueWithOnMainThread(OnLoginResult);
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

    // params : 몇개를 전달 받든 다 받을 수 있음
    // ex) WriteData(wantData, 1) 이든 WriteData(wantData, 1,2,3) 이든 다 받을 수 있음
    private void WriteData(object wantData, params string[] directory)
    {
        if (rootDB is null || wantData is null) return;

        string jsonData = JsonUtility.ToJson(wantData);

        DatabaseReference currentReference = rootDB;

        foreach (string currentChild in directory)
        {
            currentReference = currentReference.Child(currentChild);
        }
        currentReference.SetRawJsonValueAsync(jsonData).ContinueWithOnMainThread(OnTaskResult);
        // 포이치 부분이 아래 코드랑 같음, 다만 차일드가 몇명일지 모르니 위방식 포이치로 돌리는 형태
        // rootDB.Child("item").Child("Misc").Child("Nature").Child("Stone").UpdateChildrenAsync(item).ContinueWithOnMainThread(OnTaskResult);
    }
}
