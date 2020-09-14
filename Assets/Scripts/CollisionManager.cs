using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionManager : MonoBehaviour
{

    public GameController gameController;
    public Text levelCompletedScore, levelCompleted, gameOverScore;
    public GameObject levelCompletePanel, gameUI, gameOver, brokenBlock;
    public Movement playerMovement;
    public AudioSource goldSource, emptySource, lostSource, wonSource, backgroundSource;
    public ParticleSystem breakParticles;

    private SaveData saveData;
    private GoldData goldData;
    
    void Awake(){
        saveData = SaveHandler.LoadData();
        goldData = SaveHandler.LoadGoldData();
    }

    void OnCollisionEnter(Collision collision){
        if(collision.collider.tag == "finish"){
            wonSource.Play(0);
            backgroundSource.Stop();
            gameUI.SetActive(false);
            levelCompletePanel.SetActive(true);
            levelCompletedScore.text = "Score: " + gameController.scoreText.text;
            levelCompleted.text = "LEVEL " + gameController.level.ToString() + " \nCOMPLETED";
            playerMovement.canMove = false;
            
            if(saveData == null){
                gameController.SaveAll();
            }else if(gameController.score > saveData.highScore) gameController.SaveAll();
            
            gameController.SaveGold();
            Invoke("loadNextLevel", 2.0F);
        }

        if(collision.collider.tag == "platform"){
            gameController.lives -= 1;
            if(gameController.lives <= 0){
                lostSource.Play(0);
                backgroundSource.Stop();
                gameUI.SetActive(false);
                gameOver.SetActive(true);
                gameOverScore.text = "Score: " + gameController.scoreText.text;
                playerMovement.canMove = false;
            
                if(saveData == null){
                    gameController.SaveAll();
                }else if(gameController.score > saveData.highScore) gameController.SaveAll();
            
                gameController.SaveGold();
        
            }else{
                gameController.player.transform.SetPositionAndRotation(new Vector3(gameController.player.transform.position.x, gameController.player.transform.position.y + 10, gameController.player.transform.position.z)
                , Quaternion.identity);
            }
        }

    }

    void OnTriggerEnter(Collider collider){
        if(collider.tag == "gold"){
            goldSource.Play(0);
            gameController.gold += 10;
            Destroy(collider.gameObject);
        }else if(collider.tag == "empty"){
            emptySource.Play(0);
            gameController.score += 100 * gameController.level;
            Collider[] sphere = Physics.OverlapSphere(gameObject.transform.position, 10.0f);
            foreach(Collider col in sphere){
                if(col.tag == "platform"){
                    Transform transform = col.GetComponent<Transform>();
                    if(transform != null){
                        Instantiate(breakParticles, 
                        new Vector3(transform.position.x, transform.position.y-3, transform.position.z), 
                        Quaternion.identity);
                    }
                    Destroy(col.gameObject);
                }
            }
        }
    }

    void loadNextLevel(){
        gameController.destroyLevel();
        gameController.level++;
        gameController.levelGenerated = false;
        levelCompletePanel.SetActive(false);
        gameUI.SetActive(true);
    }

}
