using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("General"), Space(5)]
    public PowerUp itemData;
    public string itemName;
    public Sprite itemImage;
    public string _itemPowerUpTypeTarget;
    
    [Header("Boost"), Space(5)]
    public int itemBoostDurability; // Nbr utilisation
    public int itemBoostAmount; // Puissance de boost
    [Space(5)]
    public bool infiniteBoostDurability;
    public float timeBoostDurability;
    public bool startToUseItem = false;
    
    [Header("Projectile"), Space(5)]
    public int itemProjectileAmount;
    public bool itemCanTrackPlayer;
    
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
                itemBoostDurability = itemData.powerUpDurability;
                itemBoostAmount = itemData.powerUpBoostAmount;
                infiniteBoostDurability = itemData.infiniteDurability;
                timeBoostDurability = itemData.timeDurability;
                break;
            
            case "Projectile":
                itemProjectileAmount = itemData.projectileAmount;
                itemCanTrackPlayer = itemData.canTrackPlayer;
                break;
            /*
            case "Stun":
                
                break;
            */
        }
    }
    
    public void UseItem(string type)
    {
        switch (type)
        {
            default:
                Debug.Log("");
                break;
            
            case "Boost":
                if (!infiniteBoostDurability)
                {
                    if (itemBoostDurability > 0)
                    {
                        itemBoostDurability--;
                        CartController.Instance.ActiveSpeedBoostByItem();
                    }

                    if (itemBoostAmount == 0)
                    {
                        CartController.Instance.currentItemScript = null;
                    }
                }
                else
                {
                    if (CartController.Instance._player.GetButtonDown("UsePowerUp"))
                    {
                        CartController.Instance.ActiveSpeedBoostByItem();
                        startToUseItem = true;
                    }
                    
                    while (startToUseItem && timeBoostDurability > 0.0f)
                    {
                        timeBoostDurability -= Time.deltaTime;
                        if (CartController.Instance._player.GetButtonDown("UsePowerUp"))
                        {
                            CartController.Instance.ActiveSpeedBoostByItem();
                        }
                        
                        if (timeBoostDurability == 0)
                        {
                            CartController.Instance.currentItemScript = null;
                        }
                    }
                }
                
                break;
            /*
            case "Projectile":
                
                break;
            
            case "Stun":

                break;
            */
        }
    }
}
