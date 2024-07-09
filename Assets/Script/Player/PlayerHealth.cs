using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerHealth : MonoBehaviour

{
    [SerializeField] int maxHealth;
    int currentHealth;
    public HealthBar healthBar;
    public AudioSource soundOverGame;
   

    // Start is called before the first frame update
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
    public void Death()
    {
        soundOverGame.Play();
        GameObject.Find("GamePlay").GetComponent<GamePlay>().GameOver();
        Time.timeScale=0;
    }
    public void IncreasedBlood(int blood){
        currentHealth += blood;
        if (currentHealth >100)
        {
            currentHealth =100;
        }
        healthBar.UpdateBar(currentHealth, maxHealth);
    }
    

}
