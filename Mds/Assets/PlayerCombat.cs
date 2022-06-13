using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    [Header("Attack Damage")]
    [SerializeField] public int attackDamage;

    [Header("Attack Rate")]
    [SerializeField] public float attackRate;
    float nextAttackTime = 0f;

    [Header("Health")]
    [SerializeField] public int maxHealth;
    public int currentHealth;

    [Header("Character")]
    public PlayerCombat player;

    public HealthBar healthBar;

    public Image hpBar;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if(Time.time >= nextAttackTime)
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");

        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
        foreach(Collider enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }

    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.UpdateHealthBar(player, hpBar);

        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetBool("IsDead", true);

        GetComponent<CapsuleCollider>().enabled = false;
        this.enabled = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
