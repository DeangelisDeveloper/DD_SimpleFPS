using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("General")]
    [SerializeField] int score = 0;
    NavMeshAgent agent;
    bool canDamagePlayer;
    [Header("Enemy Stats")]
    [SerializeField] int damage = 0;
    [SerializeField] float health = 0f;
    [SerializeField] float attackDistance = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        canDamagePlayer = true;
    }

    void Update()
    {
        AiBehaviours();
    }

    void AiBehaviours()
    {
        if (Vector3.Distance(transform.position, Manager.instance.player.transform.position) > attackDistance)
        {
            Vector3 lookAt = agent.steeringTarget;
            lookAt.y = transform.position.y;
            transform.LookAt(lookAt);
            agent.SetDestination(Manager.instance.player.transform.position);
            agent.isStopped = false;
            canDamagePlayer = true;
        }

        else if (Vector3.Distance(transform.position, Manager.instance.player.transform.position) < attackDistance)
        {
            agent.isStopped = true;

            if (canDamagePlayer == true)
            {
                canDamagePlayer = false;
                Manager.instance.player.TakeDamage(damage);
            }
        }
    }

    public void TakeDamage(float amountDamage)
    {
        health -= amountDamage;

        if (health <= 0f)
            Death();
    }

    void Death()
    {
        Manager.instance.SetScore(score);
        Destroy(gameObject);
    }
}
