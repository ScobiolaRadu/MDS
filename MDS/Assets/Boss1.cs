using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class Boss1 : MonoBehaviour
{
    [Header("Look Radius")]
    [SerializeField] public float lookRadius;

    [Header("Attack Damage")]
    [SerializeField] public float attackDamage;

    [Header("AOE Damage")]
    [SerializeField] public float aoeDamage;

    [Header("Attack Rate")]
    [SerializeField] public float attackRate;
    float nextAttackTime = 0f;

    [Header("AOE Rate")]
    [SerializeField] public float aoeRate;
    float nextAoeTime = 0f;


    NavMeshAgent agent;

    public Animator animator;

    [Header("Max Health")]
    [SerializeField] public float maxHealth;
    public float currentHealth;

    public float attackRange = 0.5f;

    public float aoeRange = 2f;

    public Transform attackPoint;
    public LayerMask playerLayers;

    public Transform target;

    public float speed;

    [Header("Boss1")]
    public Boss1 boss1;

    public HealthBarBoss1 healthBar;

    public Image hpBar;

    public GameManager enemiesNr;

    public AudioSource attackSound;
    public AudioSource missSound;
    public AudioSource aoeSound;

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
        aoeDamage = aoeDamage * multiplier;

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
                agent.SetDestination(target.position);
            }

            if(distance > attackRange && distance < aoeRange)
            {
                if (Time.time >= nextAoeTime)
                {
                    AOE();
                    nextAoeTime = Time.time + 1f / aoeRate;
                }
            }

            else if (distance <= attackRange)
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
        animator.SetTrigger("Stab Attack");
        missSound.Play();

        Collider[] hitPlayers = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayers);
        foreach (Collider player in hitPlayers)
        {
            player.GetComponent<PlayerCombat>().TakeDamage(attackDamage);
            attackSound.Play();
        }

    }

    void AOE()
    {
        animator.SetTrigger("Smash Attack");
        aoeSound.Play();
        Collider[] aoeHit = Physics.OverlapSphere(transform.position, aoeRange, playerLayers);
        foreach (Collider player in aoeHit)
        {
            player.GetComponent<PlayerCombat>().TakeDamage(aoeDamage);
        }


    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.UpdateHealthBar(boss1, hpBar);

        animator.SetTrigger("Take Damage");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetTrigger("Die");

        GetComponent<CapsuleCollider>().enabled = false;
        this.enabled = false;

        healthBar.gameObject.SetActive(false);

        boss1.gameObject.tag = "Untagged";

        if (GlobalAchievements.triggerAch04 == false)
        {
            GlobalAchievements.triggerAch04 = true;
        }
        StartCoroutine(WaitForEndGame());

    }

    IEnumerator WaitForEndGame()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("EndGame");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, aoeRange);
    }
       
}
