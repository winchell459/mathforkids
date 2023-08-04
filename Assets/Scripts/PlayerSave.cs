using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerSave 
{
    private static string GetLevelKey(int level)
    {
        return $"level_{level}";
    }
    public static void SaveLevel(int level, int score)
    {
        string levelKey = GetLevelKey(level);
        if(!PlayerPrefs.HasKey(levelKey) || PlayerPrefs.GetInt(levelKey) < score)
        {
            PlayerPrefs.SetInt(levelKey, score);
        }
    }

    public static int GetSavedLevel(int level)
    {
        string levelKey = GetLevelKey(level);
        if (PlayerPrefs.HasKey(levelKey)) return PlayerPrefs.GetInt(levelKey);
        else return -1;
    }

    public static void ResetPlayerSave()
    {
        PlayerPrefs.DeleteAll();
    }
}
