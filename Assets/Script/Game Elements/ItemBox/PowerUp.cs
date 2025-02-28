using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "PU_", menuName = "Create New Power UP")]
public class PowerUp : ScriptableObject
{
    [Header("General"), Space(5)]
    public string powerUpName;
    public Sprite powerUpImage;
    public PowerUpType powerUpType;
    
    [Header("Boost"), Space(5)]
    public int powerUpDurability;
    public int powerUpBoostAmount;
    
    [Header("Projectile"), Space(5)]
    public int projectileAmount;
    public bool canTrackPlayer;
}