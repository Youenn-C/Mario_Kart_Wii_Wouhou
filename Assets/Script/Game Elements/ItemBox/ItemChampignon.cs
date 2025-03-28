using UnityEngine;

[CreateAssetMenu(fileName = "ItemChampignon", menuName = "Create New Item/ItemChampignon")]
public class ItemChampignon : Item
{
    public override void Activation(PlayerItemManager player)
    {
        player.cartController.Turbo();
    }
}
