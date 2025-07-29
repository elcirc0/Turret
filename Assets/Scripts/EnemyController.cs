using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    private static int numberOfEnemiesEnemy;
    private List<GameObject> enemies;

    public static EnemyController instance;

    private void Awake()
    {
        enemies = new List<GameObject>();
        GameObject.FindGameObjectsWithTag("Enemy", enemies);
        numberOfEnemiesEnemy = enemies.Count;
    }

    public void DecreaseNumberOfEnemiesEnemy()
    {
        numberOfEnemiesEnemy--;
    }

    public void CheckNumberOfEnemiesEnemy()
    {
        if (numberOfEnemiesEnemy <= 0)
        {
            UIController.instance.ActivateUI();
            UIController.instance.DeactivateInputSystem();
        }
    }

}
