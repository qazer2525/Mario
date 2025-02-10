using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "EnemyState", menuName = "Scriptable Objects/EnemyState")]
public class EnemyState : ScriptableObject
{
    Dictionary<int, bool> enemies;
    public void ResetState()
    {
        var keys = new List<int>(enemies.Keys);
        foreach (int enemy in keys)
        {
            enemies[enemy] = true;
        }

    }

    public void RmbChild(int id)
    {

        if (enemies == null)
        {
            enemies = new Dictionary<int, bool>();
        }
        enemies.Add(id, true);
    }

    public void killEnemy(GameObject enemy)
    {
        enemies[enemy.GetInstanceID()] = false;
    }
}
