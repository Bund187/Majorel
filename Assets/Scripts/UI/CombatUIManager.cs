using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatUIManager : MonoBehaviour {

    public Text playerHealth, enemyHealth;
    public Image healthBar, enemyHealthBar;

    private GameObject enemy, player;
    private RectTransform rectBar, rectBarEnemy;

    private void Awake()
    {
        rectBar = healthBar.GetComponent<RectTransform>();
        rectBarEnemy = enemyHealthBar.GetComponent<RectTransform>();
    }


    void Update () {
        ShowHealth();
    }

    public void ShowHealth()
    {
        OnOffUI(true);
        rectBar.sizeDelta = new Vector2(player.GetComponent<PlayerStats>().Stats1.Health, rectBar.rect.height);
        rectBarEnemy.sizeDelta = new Vector2(enemy.GetComponent<PlayerStats>().Stats1.Health, rectBarEnemy.rect.height);
        if (player.GetComponent<PlayerStats>().Stats1.Health > 0)
        {
            playerHealth.text = player.GetComponent<PlayerStats>().Stats1.Health.ToString();
        }
        else
        {
            playerHealth.text = "0";
        }
        if (enemy.GetComponent<PlayerStats>().Stats1.Health > 0)
        {
            enemyHealth.text = enemy.GetComponent<PlayerStats>().Stats1.Health.ToString();
        }
        else
        {
            enemyHealth.text = "0";
        }
    }

    public void OnOffUI(bool active)
    {
        playerHealth.gameObject.SetActive(active);
        enemyHealth.gameObject.SetActive(active);
        healthBar.gameObject.SetActive(active);
        enemyHealthBar.gameObject.SetActive(active);
    }

    public GameObject Enemy
    {
        get
        {
            return enemy;
        }

        set
        {
            enemy = value;
        }
    }

    public GameObject Player
    {
        get
        {
            return player;
        }

        set
        {
            player = value;
        }
    }
}
