using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBarImage;
    public PlayerCombat player;

    public void UpdateHealthBar(PlayerCombat player, Image healthBarImage)
    {
        float hp = (float)player.currentHealth;
        float maxhp = (float)player.maxHealth;
        healthBarImage.fillAmount = hp/maxhp;
    }
}
