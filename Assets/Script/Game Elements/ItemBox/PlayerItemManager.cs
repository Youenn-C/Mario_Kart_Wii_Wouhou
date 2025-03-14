using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItemManager : MonoBehaviour
{
    [SerializeField]
    private List<Item> _itemList;
    
    [SerializeField]
    private Item _currentItem;

    [SerializeField]
    private Image _itemImage;

    [SerializeField]
    private int _numberOfItemUse;

    public CartController cartController;


    private void Update()
    {
        if(cartController._player.GetButtonDown("UseItem"))
        {
            UseItem();
        }
    }
    public void GenerateItem()
    {
        if(_currentItem == null)
        {
            _currentItem = _itemList[Random.Range(0, _itemList.Count)];
            _itemImage.sprite = _currentItem.itemSprites[0];
            _numberOfItemUse = _currentItem.itemUseCount;
        }
    }

    public void UseItem()
    {
        if (_currentItem != null)
        {
            _currentItem.Activation(this);
            _numberOfItemUse--;
            
            // Défillement des différent sprites si besoins
            if (_currentItem.itemSprites.Length > _numberOfItemUse)
            {
                _itemImage.sprite = _currentItem.itemSprites[_numberOfItemUse];
            }
            
            if (_numberOfItemUse <= 0 )
            {
                _currentItem = null;
                _itemImage.sprite=null;
            }
        }
    }

}
