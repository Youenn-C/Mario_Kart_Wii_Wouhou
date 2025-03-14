using System;
using UnityEngine;

public class StartLine: MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        LapsManager.Instance.Check();
    }
}