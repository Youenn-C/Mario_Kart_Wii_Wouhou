using System;
using UnityEngine;

public class LapsCheckers : MonoBehaviour
{
    private Collider _collider;

    void Start()
    {
        _collider = GetComponent<Collider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LapsManager.Instance.DecreaseCheckersCountRemaining();
            _collider.enabled = false;
        }
    }

    public void Reset()
    {
        _collider.enabled = true;
    }
    
}