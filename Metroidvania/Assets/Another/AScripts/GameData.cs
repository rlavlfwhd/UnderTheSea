using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public bool isStoryWatched;
    public DPlayer dPlayer;
    public DEnemy dEnemy;
    public StageData stageData;
    public List<DEnemy> enemies = new List<DEnemy>();
}
