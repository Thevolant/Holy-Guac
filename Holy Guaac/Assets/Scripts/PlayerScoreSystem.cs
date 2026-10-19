using UnityEngine;
using TMPro;

public class PlayerScoreSystem : MonoBehaviour
{
    [SerializeField] public int playerScore;
    public PlayerStats stats;

    public TMP_Text scoreText;

    void Start()
    {
        
    }

    void Update()
    {
        UpdateScoreUI();
    }

    void OnTriggerEnter(Collider collider)
    {
        switch (collider.gameObject.tag)
        {
            case "GoldCoin":
                playerScore += stats.goldCoinScore;
                UpdateScoreUI();  break;
        }

    }

    void UpdateScoreUI()
    {
        scoreText.text = "SCORE: " + playerScore;
    }
}
