using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlaythroughManager
{
    public static int currentLevelIndex = 0;
    public static List<int> playedLevels = new List<int>();

    public static bool hasPlayedLevel(int levelBuildIndex)
    {
        return playedLevels.Contains(levelBuildIndex);
    }
}
