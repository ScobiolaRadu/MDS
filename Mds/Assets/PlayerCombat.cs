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
    public LayerMask bossLayers1;
    public LayerMask bossLayers2;

    [Header("Attack Damage")]
    [SerializeField] public int attackDamage;

    [Header("Attack Rate")]
    [SerializeField] public float attackRate;
    float nextAttackTime = 0f;

    [Header("Health")]
    [SerializeField] public float maxHealth;
    public float currentHealth;

    [Header("Character")]
    public PlayerCombat player;

    public HealthBar healthBar;

    public Image hpBar;

    public AudioSource attackSound;
    public AudioSource missSound;


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
        if (Time.timeScale == 1)
            missSound.Play();

        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
        foreach(Collider enemy in hitEnemies)
        {
            attackSound.Play();
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
  
        }

        Collider[] hitBoss = Physics.OverlapSphere(attackPoint.position, attackRange, bossLayers1);
        foreach (Collider boss in hitBoss)
        {
            attackSound.Play();
            boss.GetComponent<Boss1>().TakeDamage(attackDamage);
        }

        Collider[] hitBoss2 = Physics.OverlapSphere(attackPoint.position, attackRange, bossLayers2);
        foreach (Collider boss2 in hitBoss2)
        {
            attackSound.Play();
            boss2.GetComponent<Boss2>().TakeDamage(attackDamage);
        }


    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void TakeDamage(float damage)
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

        StartCoroutine(WaitForRespawn());
    }

    IEnumerator WaitForRespawn()
    { 
        yield return new WaitForSeconds(0.5f);
        Screen.lockCursor = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Screen.lockCursor = false;
    }

}
