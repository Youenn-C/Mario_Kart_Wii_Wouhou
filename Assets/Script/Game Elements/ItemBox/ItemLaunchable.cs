using UnityEngine;

[CreateAssetMenu(fileName = "ItemLaunchable", menuName = "Create New Item/ItemLaunchable")]
public class ItemLaunchable : Item
{
    public GameObject objectToLaunch;

    public override void Activation(PlayerItemManager player)
    {
        GameObject launched = Instantiate(objectToLaunch, player.cartController.frontPosition.position, player.transform.rotation);
        
        Rigidbody rbObjectToLaunch = launched.GetComponent<Rigidbody>();
        
        Vector3 direction = player.cartController.frontPosition.position - player.cartController.transform.position;
        rbObjectToLaunch.AddForce(direction.normalized * 1000);
    }
}
