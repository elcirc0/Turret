using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private static int numberOfEnemies;
    private List<GameObject> enemies;

    private void Awake()
    {
        enemies = new List<GameObject>();
        GameObject.FindGameObjectsWithTag("Enemy", enemies);
        numberOfEnemies = enemies.Count;
    }

    public void DecreaseNumberOfEnemies()
    {
        numberOfEnemies--;
    }

    public void CheckNumberOfEnemies()
    {
        if (numberOfEnemies <= 0)
        {
            UIController.instance.ShowUI();
            UIController.instance.DeactivateInputSystem();
        }
    }

}
