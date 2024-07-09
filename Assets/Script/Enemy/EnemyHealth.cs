using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public HealthBar healthBar;

    public string type;
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.UpdateBar(currentHealth, maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Death();

        }
        healthBar.UpdateBar(currentHealth, maxHealth);
    }

    void Death()
    {
        if (type == "boss")
        {
            Destroy(gameObject);
            GameObject.Find("GamePlay").GetComponent<GamePlay>().NextSceneGame();
        }
        else
        {
            gameObject.GetComponent<Enemy>().EnemyDieHandle();
        }
    }
}
