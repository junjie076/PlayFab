using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFabDb : MonoBehaviour {

	public void UpdatePlayerStat()
    {
        PlayFabManager.Instance.UpdateUserStat(100);
    }

    public void UpdateHeroCurrency()
    {
        PlayFabManager.Instance.UpdateUserData("TestHeroCurrency");
    }
}
