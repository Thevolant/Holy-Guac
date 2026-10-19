using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Scriptable Objects/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    //Player Stats
    public int maxHealth = 10;
    public float walkSpeed = 5.0f;
    public float playerGravity = -9.0f;
    public float jumpHeight = 10.0f;

    //Item Stats
    public int smallPotionHealAmount = 2;
    public int mediumPotionHealAmount = 5;
    public int largePotionHealAmount = 10;
    public int goldCoinScore = 50;

    //Enemy Stats
}
