using UnityEngine;
using UnityEngine.AI;

public class PlaneAI : MonoBehaviour
{
    [SerializeField] private EnemyController enemyController;

    [SerializeField] private Transform[] PointToMove;
    [SerializeField] private float DistanceToClose = 5;

    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float health;

    private int Counter = 0;

    private NavMeshAgent agent;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        health = maxHealth;
    }

    private void FixedUpdate()
    {
        Moving();
    }

    private void Moving()
    {
        agent.SetDestination(PointToMove[Counter].position);
        float distance = Vector3.Distance(PointToMove[Counter].position, this.transform.position);

        if (distance <= DistanceToClose)
            Counter++;

        if (Counter == PointToMove.Length)
            Counter = 1;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            Kill();
        }
    }

    private void Kill()
    {
        enemyController.DecreaseNumberOfEnemies();
        enemyController.CheckNumberOfEnemies();
        Destroy(gameObject);
    }
}
