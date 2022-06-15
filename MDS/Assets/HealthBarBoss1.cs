using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarBoss1 : MonoBehaviour
{
    public Image healthBarImage;
    public Boss1 boss1;

    public void UpdateHealthBar(Boss1 boss1, Image healthBarImage)
    {
        float hp = (float)boss1.currentHealth;
        float maxhp = (float)boss1.maxHealth;
        healthBarImage.fillAmount = hp / maxhp;
    }
}
