using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class LoginManager : MonoBehaviour {

    public GameObject successLogin;
    public GameObject failedLogin;
    public Text failtext;

	void OnEnable()
    {
        PlayFabManager.LoginCompleted += LoginComplete;
        PlayFabManager.LoginFailed += LoginFailed;
    }

    void OnDisable()
    {
        PlayFabManager.LoginCompleted -= LoginComplete;
        PlayFabManager.LoginFailed -= LoginFailed;
    }

    public void LoginAsGuest()
    {
        PlayFabManager.Instance.GuestLogin();
    }

    public void LoginWithFacebook()
    {
        FacebookManager.Instance.FBLogin();
    }

    public void LoginWithGoogle()
    {
        GooglePlayManager.Instance.LoginGPGS();
    }

    void LoginComplete()
    {
        successLogin.SetActive(true);
        Debug.Log("Successfully login to PlayFab!");

        SceneManager.LoadScene(1);
    }

    void LoginFailed(string _error)
    {
        failedLogin.SetActive(true);
        failtext.text = "Login Failed = " + _error;
        Debug.LogAssertion("Login Failed...");
    }
}
