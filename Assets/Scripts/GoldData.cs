using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GoldData
{
    public int gold;

    public GoldData(GameController gameController){
        gold = gameController.gold;
    }
}
