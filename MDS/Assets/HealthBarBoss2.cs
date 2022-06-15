using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarBoss2 : MonoBehaviour
{
    public Image healthBarImage;
    public Boss2 boss2;

    public void UpdateHealthBar(Boss2 boss2, Image healthBarImage)
    {
        float hp = (float)boss2.currentHealth;
        float maxhp = (float)boss2.maxHealth;
        healthBarImage.fillAmount = hp / maxhp;
    }
}
