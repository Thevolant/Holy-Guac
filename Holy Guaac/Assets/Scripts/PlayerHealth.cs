using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public PlayerStats stats;
    [SerializeField] public int playerHealth;
    public GameObject player;
    public CheckpointSystem checkpoint;


    private bool touchingHostile = false;

    private Scene currentScene;

    
    void Start()
    {
        playerHealth = stats.maxHealth;
    }

    void Update()
    {
        currentScene = SceneManager.GetActiveScene();
        HealthCheck();
    }

    void HealthCheck()
    {
        if (playerHealth > stats.maxHealth) {
            playerHealth = stats.maxHealth;
        }
        else if (playerHealth <= 0) {
            Fail();
        }
    }

    void Damage(int damage)
    {
        playerHealth -= damage;
        HealthCheck();
    }

    void Fail()
    {
        player.SetActive(false);
        playerHealth = 5;
        Debug.Log("fail");
        checkpoint.RespawnAtLastCheckpoint();
    }

    void OnTriggerEnter(Collider collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Lava":
                Debug.Log("touched lava");
                Damage(10); break;

            case "Enemy":
                Damage(2); break;
            case "Spike":
                Damage(2); break;

        }
    }
}
