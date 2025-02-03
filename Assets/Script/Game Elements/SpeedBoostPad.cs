using System;
using UnityEngine;

public class SpeedBoostPad : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        CartController.Instance.ActiveSpeedBoostByPad();
    }
}
