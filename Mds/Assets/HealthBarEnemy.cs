using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarEnemy : MonoBehaviour
{
    public Image healthBarImage;
    public Enemy enemy;

    public void UpdateHealthBar(Enemy enemy, Image healthBarImage)
    {
        float hp = (float)enemy.currentHealth;
        float maxhp = (float)enemy.maxHealth;
        healthBarImage.fillAmount = hp / maxhp;
    }
}
