using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Vector2 Line;

    void Start()
    {
        
    }
    void Update()
    {
        if (player == null)
        {
            return;
        }

        float playerPositionX = player.position.x;

        if (playerPositionX >= 9.5f)
        {
            player.position = new Vector2(0, -1.8f);
        }        
    }
}
