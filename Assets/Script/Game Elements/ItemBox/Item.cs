using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("General"), Space(5)]
    public PowerUp _powerUpData;
    public string _powerUpName;
    public Sprite _itemPowerUpImage;
    public string _itemPowerUpTypeTarget;
    
    [Header("Boost"), Space(5)]
    public int _itemPowerUpDurability;
    public int _itemPowerUpBoostAmount;
    
    [Header("Projectile"), Space(5)]
    public int _itemProjectileAmount;
    public bool _itemCanTrackPlayer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Initialisation(_itemPowerUpTypeTarget);
    }

    void Initialisation(string target)
    {
        switch (target)
        {
            default:
                Debug.Log("Power Up not found");
                break;
            
            case "Boost":
                _itemPowerUpDurability = _powerUpData.powerUpDurability;
                _itemPowerUpBoostAmount = _powerUpData.powerUpBoostAmount;
                break;
            
            case "Projectile":
                _itemProjectileAmount = _powerUpData.projectileAmount;
                _itemCanTrackPlayer = _powerUpData.canTrackPlayer;
                break;
            /*
            case "Stun":
                
                break;
            */
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
