using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class PlaythroughManager
{
    public static int currentLevelIndex = 0;
    public static List<int> playedLevels = new List<int>();

    public static bool hasPlayedLevel(int levelBuildIndex)
    {
        string path = Application.persistentDataPath + "/PlayHistory.txt";
        Debug.Log("Playthrough Manager");
        Debug.Log("Path: " + path);
        // Path doesn't exist => first play session
        if(!File.Exists(path))
        {
            return false;
        }

        // Else read from file and copy contents into temp list
        string textLevels = File.ReadAllText(path);
        Debug.Log("levels as text: " + textLevels);
        string[] levels = textLevels.Split('-');
        Debug.Log("num levels played: " + levels.Length);
        List<int> tempList = new List<int>();
        for(int i = 0; i < levels.Length; i++)
        {
            if(string.Equals(levels[i], "-") || string.Equals(levels[i], " ") || string.Equals(levels[i], ""))
            {
                continue;
            }
            tempList.Add(int.Parse(levels[i]));
        }

        tempList.ForEach(Print);

        // Set actual list to temp list to prevent adding multiple of the same level
        // when checking for play history
        playedLevels = tempList;
        Debug.Log("Played this level? " + (playedLevels.Contains(levelBuildIndex) ? "y" : "n"));
        return playedLevels.Contains(levelBuildIndex);
    }

    public static bool hasPlayedLevel(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            var scene = SceneManager.GetSceneByBuildIndex(i);
            string testedScreen = scene.name;
            //print("sceneIndexFromName: i: " + i + " sceneName = " + testedScreen);
            if (testedScreen == sceneName)
                return hasPlayedLevel(i);
        }
        return false;
    }

    // Check if player hasPlayedLevel before saving level history
    public static void saveLevelHistory(int levelBuildIndex)
    {
        playedLevels.Add(levelBuildIndex);

        string path = Application.persistentDataPath + "/PlayHistory.txt";
        Debug.Log("Playthrough Manager");
        Debug.Log("Path: " + path);
        string textLevels = "";
        foreach (int level in playedLevels)
        {
            textLevels += level + "-";
        }
        Debug.Log("levels as text: " + textLevels);
        File.WriteAllText(path, textLevels);
        Debug.Log("Played this level? " + (playedLevels.Contains(levelBuildIndex) ? "y" : "n"));
    }

    private static void Print(int n)
    {
        Debug.Log("Played Level " + n);
    }
}
