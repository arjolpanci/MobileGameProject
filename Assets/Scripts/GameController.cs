using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public int level = 1, gold = 0, score = 0, lives=2;
    public Transform player, mainCamera;
    public Text levelText, goldText, scoreText;
    System.Random rnd = new System.Random();
    public bool levelGenerated = false;
    public GameObject platform, goldPrefab, emptyCube;
    public Movement playerMovement;
    public AudioSource backgroundSource;

    private GoldData goldData;
    private Vector3 initialPos;

    void Awake(){
        goldData = SaveHandler.LoadGoldData();
        if(goldData != null){
            gold = goldData.gold;
        }
    }

    void Update()
    {
        if(!levelGenerated){
            generateLevel(level);
            levelGenerated = true;
        }

        scoreText.text = score.ToString("0");
        goldText.text = "Gold: " + gold.ToString();
    }

    void generateLevel(int level){
        backgroundSource.Play(0);
        bool goldGenerated = false;
        for(int k=1; k<=5*generatePlatformsPerLevel(level); k++)
        {
            int l = rnd.Next(1,5);
            int m = rnd.Next(1,5);
            for (int i = 0; i <= 6; i++)
            {
                for (int j = 0; j <= 6; j++)
                {
                    if(i==l && j==m){
                        Instantiate(emptyCube, new Vector3(i-3, 50*k, j-3), Quaternion.identity);
                        continue;
                    }

                    Instantiate(platform, new Vector3(i-3, 50*k, j-3), Quaternion.identity);
                    
                    if(!goldGenerated){
                        Instantiate(goldPrefab, new Vector3(i - rnd.Next(0, 3), 50*k - 25, j- rnd.Next(0,3)), Quaternion.identity);
                        goldGenerated = true;
                    }
                    
                    player.position = new Vector3(0, 50*k + 25, 0);
                    mainCamera.position = new Vector3(0, 50*k + 10, 0);
                    initialPos = player.position;
                }
            }
            goldGenerated = false;
        }
        levelText.text = "Level: " + level.ToString();
        playerMovement.canMove = true;
    }

    int generatePlatformsPerLevel(int level){
        if(level < 3){
            return 1;
        }else if(level < 7){
            return 2;
        }else if(level < 13){
            return 3;
        }else{
            return 4;
        }
    }

    public void destroyLevel(){
        GameObject[] objectsInGame;
        objectsInGame = GameObject.FindGameObjectsWithTag("platform");
        for(int i = 0; i<objectsInGame.Length; i++){
            Destroy(objectsInGame[i]);
        }
        objectsInGame = GameObject.FindGameObjectsWithTag("gold");
        for(int i = 0; i<objectsInGame.Length; i++){
            Destroy(objectsInGame[i]);
        }
        objectsInGame = GameObject.FindGameObjectsWithTag("particle");
        for(int i = 0; i<objectsInGame.Length; i++){
            Destroy(objectsInGame[i]);
        }
    }

    public void SaveAll(){
        SaveHandler.SaveData(this);
    }

    public void SaveGold(){
        SaveHandler.SaveGold(this);
    }

}