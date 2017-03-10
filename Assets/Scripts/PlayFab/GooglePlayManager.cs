using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.UI;
using UnityEngine;

public class GooglePlayManager : MonoBehaviour {

    private static GooglePlayManager _instance;
    public static GooglePlayManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GooglePlayManager>();

                if (_instance)
                    DontDestroyOnLoad(_instance);
            }
            return _instance;
        }
    }

    public Text consoleText;
    //public Image userImageSprite;

    void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void LoginGPGS()
    {
        // Configure and init GPGS, activate platform
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().AddOauthScope("profile").Build();
        ConsoleWrite("GPGS Config \t\t : Success");
        PlayGamesPlatform.InitializeInstance(config);
        ConsoleWrite("GPGS Init \t\t\t : Success");
        PlayGamesPlatform.Activate();
        ConsoleWrite("GPGS Activate \t : Success");

        // Authenticate Using GPGS and get user information
        Social.localUser.Authenticate(success => {
            if (success)
            {
                PlayGamesPlatform.Instance.GetServerAuthCode((code, authToken) =>
                {
                    PlayFabManager.Instance.GooglePlayLogin(authToken);
                });
                ConsoleWrite("LocalUser Auth \t : Success");
                ConsoleWrite("User Name      \t : " + Social.localUser.userName);
                ConsoleWrite("User ID    \t\t\t : " + Social.localUser.id);

                //if (Social.localUser.image != null)
                //{
                //    SetUserImage();
                //}
                //else {
                //    ConsoleWrite("User Image     \t\t : Failed");
                //}
            }
            else {
                ConsoleWrite("LocalUser Auth \t : Failed");
            }
        });
    }

    //// Get and display GPGS image
    //public void SetUserImage()
    //{
    //    userImageSprite.GetComponent<Image>().sprite = Sprite.Create(Social.localUser.image, new Rect(0, 0, 200, 200), new Vector2(0f, 0f));
    //}

    /// <summary>
    /// Console functions
    /// </summary>

    // Write to screen console
    public void ConsoleWrite(string s)
    {
        consoleText.text += "\n" + s;
        Debug.Log(s);
    }

    // Clear screen console
    public void ConsoleClear()
    {
        consoleText.text = "";
    }
}
