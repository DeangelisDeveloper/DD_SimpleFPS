using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("General")]
    [SerializeField] Weapon weapon = null;
    [SerializeField] float respawnTime = 0f;
    [Header("Player Stats")]
    [SerializeField] float damageTime = 0f;
    [SerializeField] int maxHealth = 0;
    int currentHealth;
    [Header("GUI")]
    [SerializeField] Text healthText = null;
    [SerializeField] GameObject damagePanel = null;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        SetHealth();
    }

    void SetHealth()
    {
        healthText.text = "HEALTH: " + currentHealth.ToString();

        if (currentHealth < 0)
            currentHealth = 0;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        StartCoroutine(ShowDamage());

        if (currentHealth <= 0)
            Death();
    }

    void Death()
    {
        StartCoroutine(Respawn());
    }

    IEnumerator ShowDamage()
    {
        damagePanel.SetActive(true);
        yield return new WaitForSeconds(damageTime);
        damagePanel.SetActive(false);
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}