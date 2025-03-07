using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerItemManager : MonoBehaviour
{
    [SerializeField] private List<Item> _itemList;
    [SerializeField] private Item _currentItem;
    [SerializeField] private Image _itemImage;
    [SerializeField] private int _numberOfItemUse;

    private void Update()
    {
        if (CartController.Instance._player.GetButtonDown("UseItem"))
        {
            UseItem();
        }
    }

    public void GenerateItem()
    {
        if (_currentItem == null)
        {
            _currentItem = _itemList[Random.Range(0, _itemList.Count)];
            _itemImage.sprite = _currentItem.itemSprite;
            _numberOfItemUse = _currentItem.itemUseCount;
        }
    }

    public void UseItem()
    {
        if (_currentItem != null)
        {
            _currentItem.Activation(this);
            _numberOfItemUse--;
            if (_numberOfItemUse <= 0)
            {
                _currentItem = null;
            }
        }
    }
}