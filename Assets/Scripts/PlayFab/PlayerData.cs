using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData {

    int _monsterKilled;

    string _heroCurrency;

    public PlayerData()
    {
        _monsterKilled = 0;
        _heroCurrency = "Null";
    }

    public int MonsterKilled
    {
        get { return _monsterKilled; }
        set { _monsterKilled = value; }
    }

    public string HeroCurrency
    {
        get { return _heroCurrency; }
        set { _heroCurrency = value; }
    }
}
