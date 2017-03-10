using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using PlayFab;
using PlayFab.ClientModels;

public class S_PlatformPlayFab : MonoBehaviour 
{
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

	private string PlayFabId;
	private string userName;
	private string userId;
	private string userEmail;

	/// <summary>
	/// Class specific functions
	/// </summary>

	/// <summary>
	/// Login functions
	/// </summary>

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

	public void LoginPlayFab() 
	{
		if (Social.localUser.authenticated)
		{
			PlayGamesPlatform.Instance.GetServerAuthCode((code, authToken) => {
				PlayFabClientAPI.LoginWithGoogleAccount(new LoginWithGoogleAccountRequest(){
					TitleId = PlayFabSettings.TitleId,
					ServerAuthCode = authToken,
					CreateAccount = true,
				}, (successLoginResult) => {
					PlayFabId = successLoginResult.PlayFabId;
					ConsoleWrite("PF Google Login : Success");
					ConsoleWrite("PF Login ID \t\t\t : " + successLoginResult.PlayFabId.ToString());
				}, (errorResult) => {
					ConsoleWrite("PF Google Login : Failed");
					ConsoleWrite("Error Report : \n" + errorResult.GenerateErrorReport().ToString());
				});
			});
		} 
		else 
		{
			ConsoleWrite("PF Google Login : Failed");
			ConsoleWrite("GPGS Not Logged In");
		}
	}

	public void UserEmailRead()
	{
		PlayGamesPlatform.Instance.GetUserEmail((status, email) => 
		{
			if (status == CommonStatusCodes.Success) 
			{
				ConsoleWrite("User Email \t\t\t : Success \n" + email);
			} 
			else 
			{
				ConsoleWrite("User Email \t\t\t : Failed");
			}
		});
	}

	public void UserDataWrite()
	{
		UpdateUserDataRequest request = new UpdateUserDataRequest()
		{
			Data = new Dictionary<string, string>()
			{
				{ 
					inputKey.text, inputValue.text
				},
			}
		};

		PlayFabClientAPI.UpdateUserData(request, (result) => 
		{
			ConsoleWrite("User Data Update \t : Success");
			ConsoleWrite("User Key\t : " + inputKey.text + "\t\t\t User Value\t : " + inputValue.text);
		}, 
		(error) =>
		{
				ConsoleWrite("User Data Update \t : Failed");
		});
	}

	public void UserDataRead()
	{
		GetUserDataRequest request = new GetUserDataRequest()
		{
			PlayFabId = PlayFabId,
			Keys = null
		};

		PlayFabClientAPI.GetUserData(request,(result) => 
		{
			if ((result.Data == null) || (result.Data.Count == 0))
			{
				ConsoleWrite("User Data Read \t : Returned Null");
			} 
			else 
			{
				ConsoleWrite("User Data Read \t : Success");
			
				foreach (var item in result.Data) 
				{
					ConsoleWrite("User Key\t : " + item.Key + "\t\t User Value\t : " + item.Value.Value);
				}
			}
		}, (error) => {
			ConsoleWrite("User Data Read \t : Failed");
		});
	}

	public void ServerDataRead()
	{
		var getRequest = new GetTitleDataRequest();

		PlayFabClientAPI.GetTitleData(getRequest, (result) => 
		{
			ConsoleWrite("Server Data Read \t\t : Success");

			foreach (var entry in result.Data)
				{
				ConsoleWrite("Server Key\t : " + entry.Key + "\t\t Server Value\t : " + entry.Value);
			}
		}, 
		(error) => 
		{
			ConsoleWrite("Server Data Read \t\t : Failed");
		});
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

