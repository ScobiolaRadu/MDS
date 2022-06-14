using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System;

public class Enemy : MonoBehaviour
{
    [Header("Look Radius")]
    [SerializeField] public float lookRadius;

    [Header("Attack Damage")]
    [SerializeField] public float attackDamage;

    [Header("Attack Rate")]
    [SerializeField] public float attackRate;
    float nextAttackTime = 0f;

    NavMeshAgent agent;

    public Animator animator;

    [Header("Max Health")]
    [SerializeField]public float maxHealth;
    public float currentHealth;

    public float attackRange = 0.5f;
    public Transform attackPoint;
    public LayerMask playerLayers;

    public Transform target;

    public float speed;

    [Header("Enemy")]
    public Enemy enemy;

    public HealthBarEnemy healthBar;

    public Image hpBar;

    public GameManager enemiesNr;

    void Start()
    {
        int localDifficulty = PlayerPrefs.GetInt("masterDifficulty");
        float multiplier;
        if (localDifficulty == 0)
            multiplier = 0.9f;
        else if (localDifficulty == 1)
            multiplier = 1;
        else
            multiplier = 1.1f;

        maxHealth = maxHealth * multiplier;
        attackDamage = attackDamage * multiplier;

        agent = GetComponent<NavMeshAgent>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (Time.timeScale == 1)
        { 
            float distance = Vector3.Distance(target.position, transform.position);
            if (distance <= lookRadius)
            {
                FaceTarget();
                animator.SetBool("Move", true);
                transform.position = Vector3.MoveTowards(transform.position, target.position, speed);
            }

            if (distance <= attackRange)
            {
                if (Time.time >= nextAttackTime)
                {
                    Attack();
                    nextAttackTime = Time.time + 1f / attackRate;
                }
            }
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void Attack()
    {
        animator.SetTrigger("Attack");

        Collider[] hitPlayers = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayers);
        foreach (Collider player in hitPlayers)
        {
            player.GetComponent<PlayerCombat>().TakeDamage(attackDamage);
        }

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.UpdateHealthBar(enemy, hpBar);

        animator.SetTrigger("Hurt");

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetBool("Move", false);
        animator.SetBool("IsDead", true);

        GetComponent<CapsuleCollider>().enabled = false;
        this.enabled = false;

        healthBar.gameObject.SetActive(false);

        enemy.gameObject.tag = "Untagged";

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
