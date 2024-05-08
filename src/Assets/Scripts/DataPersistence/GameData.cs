using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    // Save data
    public string saveName;
    public long lastUpdated;
    
    // Game data
    public int difficultyId;
    public string username;
    public int petId;
    public int money;
    public int level;

    public GameData()
    {
        difficultyId = 0;
        username = "Lex Starwalker";
        petId = -1;
        money = 0;
        level = 1;
    }
}
