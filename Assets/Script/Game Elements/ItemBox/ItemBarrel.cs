using UnityEngine;

[CreateAssetMenu(fileName = "ItemBarrel", menuName = "Create New Item/ItemBarrel")]
public class ItemBarrel : Item
{
    public GameObject objectToSpawn;
    
    public override void Activation(PlayerItemManager player)
    {
        GameObject spawned = Instantiate(objectToSpawn, player.cartController.frontPosition.position, player.transform.rotation);
        
        spawned.transform.position = new Vector3(player.cartController.backPosition.position.x, 0, player.cartController.backPosition.position.z);
    }
}
