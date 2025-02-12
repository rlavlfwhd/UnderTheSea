using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DEnemy
{
    public Vector2 ePos;
    public float eHP;

    public DEnemy(Vector2 ePos, float eHP)
    {
        this.ePos = ePos;
        this.eHP = eHP;
    }
}
