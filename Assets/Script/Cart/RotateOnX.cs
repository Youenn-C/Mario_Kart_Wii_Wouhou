using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class RotateOnX : MonoBehaviour
{
    [Header("References"), Space(5)]
    [SerializeField] private GameObject _cart;
    [SerializeField] private GameObject _frontCheck;
    [SerializeField] private GameObject _backCheck;

    void Update()
    {
        if (_frontCheck.transform.position.y > _backCheck.transform.position.y)
        {
            _cart.transform.Rotate(new Vector2(1, 0), Space.World);
        }
        
        else if (_frontCheck.transform.position.y < _backCheck.transform.position.y)
        {
            _cart.transform.Rotate(new Vector2(-1, 0), Space.World);
        }

        else
        {
            _cart.transform.Rotate(new Vector2(0, 0), Space.World);
        }
    }
}
