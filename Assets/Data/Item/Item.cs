using UnityEngine;

public class Item : ScriptableObject
{
    public string itemName; // [protected] Donne accès a la variable pour les scripts enfants
    public Sprite itemSprite;
    public int itemUseCount;

    public virtual void Activation(PlayerItemManager player)
    {
        
    }
}