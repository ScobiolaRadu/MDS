using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Look Radius")]
    [SerializeField] public float lookRadius;

    [Header("Attack Damage")]
    [SerializeField] public int attackDamage;

    [Header("Attack Rate")]
    [SerializeField] public float attackRate;
    float nextAttackTime = 0f;

    Transform target;
    NavMeshAgent agent;

    public Animator animator;

    public int maxHealth = 100;
    private int currentHealth;


    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);
            animator.SetBool("Move", true);

            if (distance <= agent.stoppingDistance)
            {
                FaceTarget();
            }
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        animator.SetTrigger("Hurt");

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetBool("IsDead", true);

        GetComponent<CapsuleCollider>().enabled = false;
        this.enabled = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
   
}
