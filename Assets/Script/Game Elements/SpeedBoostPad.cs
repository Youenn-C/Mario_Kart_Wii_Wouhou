using System;
using UnityEngine;

public class SpeedBoostPad : MonoBehaviour
{
    [SerializeField] private CartController _cartController;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var temp = other.GetComponent<CartController>();
            temp.ActiveSpeedBoostByPad();
            temp = null;
        }
    }
}
