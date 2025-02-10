using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public EnemyState enemyState;
    public void Awake()
    {
        // subscribe to Game Restart event
        GameManager.instance.gameRestart.AddListener(GameRestart);
    }
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            enemyState.RmbChild(child.GetInstanceID());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameRestart()
    {
        enemyState.ResetState();
        foreach (Transform child in transform)
        {
            child.GetComponent<EnemyMovement>().GameRestart();
        }
    }

    public void killEnemy(GameObject enemy)
    {
        enemyState.killEnemy(enemy);
        enemy.GetComponent<EnemyMovement>().EnemyDeath();
    }
}
