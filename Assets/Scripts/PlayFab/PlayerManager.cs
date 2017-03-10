using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    private static PlayerManager _instance;
    public PlayerData playerData;
    public static PlayerManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<PlayerManager>();

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
        InitPlayer();
    }

    void InitPlayer()
    {
        playerData = new PlayerData();
    }
}
