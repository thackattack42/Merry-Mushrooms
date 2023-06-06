using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(0)]
public class enemyManager : MonoBehaviour
{
    private static enemyManager _instance;

    public static enemyManager Instance
    {
        get { return _instance; }
        private set { _instance = value; }
    }

    public Transform playerPos;
    public float radiusFromPlayer;
    public List<Enemy_Scpt> enemies = new List<Enemy_Scpt>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            return;
        }

        Destroy(gameObject);
    }

    public void MakeEnemiesCircleTarget()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].MoveTo(new Vector3(
                playerPos.position.x + radiusFromPlayer * Mathf.Cos(2 * Mathf.PI * i / enemies.Count),
                playerPos.position.y,
                playerPos.position.z + radiusFromPlayer * Mathf.Sin(2 * Mathf.PI * i / enemies.Count)
                ));
        }
    }
}
