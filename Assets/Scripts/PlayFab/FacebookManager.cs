using System;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
using UnityEngine;

public class FacebookManager : MonoBehaviour {

    private static FacebookManager _instance;
    public static FacebookManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = GameObject.FindObjectOfType<FacebookManager>();

                if (_instance)
                    DontDestroyOnLoad(_instance);
            }
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
        InitFB();
    }

    #region InitFB

    public void InitFB()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(SetInit, OnHideUnity);
        }
        else
        {
            return;
        }
    }

    void SetInit()
    {
        if (FB.IsLoggedIn)
        {
            Debug.Log("FB is logged in");
        }
        else
        {
            Debug.Log("FB is not logged in");
        }
    }

    void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    #endregion

    #region FBLogin

    public void FBLogin()
    {
        List<string> permissions = new List<string>();
        permissions.Add("public_profile");

        FB.LogInWithReadPermissions(permissions, OnLoginFBCompleted);
    }

    void OnLoginFBCompleted(IResult _result)
    {
        if(_result.Error != null || _result.Cancelled)
        {
            //Handle login to facebook failed error here
            Debug.LogAssertion("Failed to login to facebook");
        }
        else
        {
            Debug.Log("Login success!");
            PlayFabManager.Instance.FacebookLogin(AccessToken.CurrentAccessToken.TokenString);
        }
    }

    #endregion
}
