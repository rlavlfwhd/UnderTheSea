using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Vector3 nextMapPosition;
    public BoxCollider2D mapBoundary;

    void Update()
    {
        float playerPositionX = player.position.x;

        if (playerPositionX > mapBoundary.bounds.max.x)
        {
            transform.position = nextMapPosition;
        }
    }
}
