using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using Firebase;
using Firebase.Auth;

public class S_PlatformFirebase : MonoBehaviour {

	/// <summary>
	/// Public variables
	/// </summary>

	public Text consoleText;
	public Text inputKey;
	public Text inputValue;
	public Image userImageSprite;

	/// <summary>
	/// Private variables
	/// </summary>

	private Firebase.Auth.FirebaseAuth auth;
	private Firebase.Auth.FirebaseUser user;
	private Firebase.Auth.Credential cred;
	private Firebase.DependencyStatus dependencyStatus;

	/// <summary>
	/// Class specific functions
	/// </summary>

	/// <summary>
	/// Login functions
	/// </summary>

	// Login to Google Play Games Services

	public void LoginGPGS()
	{
		// Configure and init GPGS, activate platform
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().AddOauthScope("profile").Build();
		ConsoleWrite ("GPGS Config \t\t : Success");
		PlayGamesPlatform.InitializeInstance(config);
		ConsoleWrite ("GPGS Init \t\t\t : Success");
		PlayGamesPlatform.Activate();
		ConsoleWrite ("GPGS Activate \t : Success");

		// Authenticate Using GPGS and get user information
		Social.localUser.Authenticate (success => {
			if (success) {
				ConsoleWrite ("LocalUser Auth \t : Success");
				ConsoleWrite ("User Name      \t : " + Social.localUser.userName);
				ConsoleWrite ("User ID    \t\t\t : " + Social.localUser.id);

				if(Social.localUser.image != null){
					SetUserImage ();
				} else{
					ConsoleWrite ("User Image     \t\t : Failed");
				}
			} else {
				ConsoleWrite ("LocalUser Auth \t : Failed");
			}
		});
	}

	// Get and display GPGS image
	public void SetUserImage(){
		userImageSprite.GetComponent<Image> ().sprite = Sprite.Create (Social.localUser.image, new Rect (0, 0, 200, 200), new Vector2 (0f,0f));
	}
		
	public void LoginFirebase() 
	{
		Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
		ConsoleWrite ("Firebase Instance   : Success");

		PlayGamesPlatform.Instance.GetIdToken ((token) => {
			if(token != null)
			{
				Firebase.Auth.Credential credential = Firebase.Auth.GoogleAuthProvider.GetCredential(token, PlayGamesPlatform.Instance.GetAccessToken ());
				ConsoleWrite ("GAccess & GID Token : Success");

				auth.SignInWithCredentialAsync(credential).ContinueWith(task => {
					if (task.IsCanceled) {
						ConsoleWrite ("Sign In Async  \t : Canceled");
						return;
					}
					if (task.IsFaulted) {
						ConsoleWrite ("Sign In Async  \t : Failed");
						ConsoleWrite ("" + task.Exception.ToString());
						return;
					}

					Firebase.Auth.FirebaseUser newUser = task.Result;
					ConsoleWrite ("Sign In Async  \t : Success");
					ConsoleWrite ("Name   \t : " + newUser.DisplayName);
					ConsoleWrite ("UserID \t : " + newUser.UserId);
					ConsoleWrite ("Email  \t : " + newUser.Email);
					ConsoleWrite ("Photo  \t : " +newUser.PhotoUrl);
				});
			}
			else
			{
				ConsoleWrite ("GAccess & GID Token : Failed");
			}
		});
	}

	public void UserEmailRead()
	{
	}

	public void UserDataWrite()
	{
	}

	public void UserDataRead()
	{
	}

	public void ServerDataRead()
	{
	}

	/// <summary>
	/// Console functions
	/// </summary>

	// Write to screen console
	public void ConsoleWrite(string s)
	{
		consoleText.text += "\n" + s;
		Debug.Log (s);
	}

	// Clear screen console
	public void ConsoleClear()
	{
		consoleText.text = "";
	}
}
