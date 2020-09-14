using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreText : MonoBehaviour
{
    
    private SaveData saveData;
    private GoldData goldData;
    public Text highscoreText, goldText;

    void Awake(){
        saveData = SaveHandler.LoadData();
        goldData = SaveHandler.LoadGoldData();
    }

    void Start(){
        if(saveData == null){
            highscoreText.text = "HighScore: 0";
        }else{
            highscoreText.text = "HighScore: " + saveData.highScore;
        }
        if(goldData == null){
            goldText.text = "Gold: 0";
        }else{
            goldText.text = "Gold: " + goldData.gold;
        }
    }
}
