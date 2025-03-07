using System;
using UnityEngine;

public class SpeedBoostPad : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CartController.Instance.ActiveSpeedBoostByPad();
        }
    }
}
