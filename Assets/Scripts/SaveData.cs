using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int highScore;
    public int level;

    public SaveData(GameController gameController){
        highScore = gameController.score;
        level = gameController.level;
    }

}
