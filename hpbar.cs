using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public PlayerMovement player;
    public Image hpFill;

    void Update()
    {
        hpFill.fillAmount = (float)player.currentHealth / player.maxHealth;
    }
}
