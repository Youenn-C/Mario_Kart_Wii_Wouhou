using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemBox : MonoBehaviour
{
    [Header("References"), Space(5)] 
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private BoxCollider _boxCollider;
    [Space(5)]
    [SerializeField] private float _waitBeForeRespawn;

    private void OnTriggerEnter(Collider other)
    {
        PlayerItemManager playerItemManagerInContact = other.gameObject.GetComponent<PlayerItemManager>();

        if (playerItemManagerInContact != null)
        {
            playerItemManagerInContact.GenerateItem();
            StartCoroutine(Respawn());
        }
    }

    private IEnumerator Respawn()
    {
        _meshRenderer.enabled = false;
        _boxCollider.enabled = false;
        yield return new WaitForSeconds(_waitBeForeRespawn);
        _meshRenderer.enabled = true;
        _boxCollider.enabled = true;
    }
}