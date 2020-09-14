using UnityEngine;

public class BombAbility : ISpecialAbility
{

    GameObject player;

    public BombAbility(GameObject player){
        this.player = player;
    }

    public void activateAbility(){
        player.tag = "PlayerBomb";
    }
}
