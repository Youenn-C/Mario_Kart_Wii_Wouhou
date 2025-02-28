using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemBox : MonoBehaviour
{
    [Header("References"), Space(5)] 
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private BoxCollider _boxCollider;
    [Space(5)]
    [SerializeField] private GameObject[] _powerUp;

    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        _meshRenderer.enabled = false;
        _boxCollider.enabled = false;
        TriggerItemBox();
    }

    void TriggerItemBox()
    {
        int randomPowerUp = Random.Range(0, _powerUp.Length);
        if (CartController.Instance.currentItemScript == null)
        {
            CartController.Instance.currentItemScript = _powerUp[randomPowerUp].GetComponent<Item>();
        }
        StartCoroutine(ItemBoxRespwan());
    }

    public IEnumerator ItemBoxRespwan()
    {
        yield return new WaitForSeconds(3f);
        _meshRenderer.enabled = true;
        _boxCollider.enabled = true;
    }
}