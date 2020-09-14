using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideWall : MonoBehaviour
{

    public Transform player;

    void Update()
    {
        transform.position = new Vector3(transform.position.x, player.position.y-20, transform.position.z);
    }
}
