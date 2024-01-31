using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField]
    List<Button> ItemsBtns;

    private void Awake()
    {
        //add functions to the shop items
        foreach(Button itemBtn in ItemsBtns)
        {
            itemBtn.onClick.AddListener(delegate { BuyItem(itemBtn.GetComponent<ItemData>()); });
        }
    }

    void BuyItem(ItemData item)
    {
        CoinsManager.instance.ModifyPlayerCurrentCoins(CoinsManager.instance.allPlayersData[0], -item.itemPrice);
        item.GetComponent<Button>().interactable = false;
        FindObjectOfType<PlayerManager>().AddToInventory(item).onClick.AddListener(delegate { SellItem(item); });
    }
    void SellItem(ItemData item)
    {
        CoinsManager.instance.ModifyPlayerCurrentCoins(CoinsManager.instance.allPlayersData[0], item.itemPrice);
        item.GetComponent<Button>().interactable = true;
        FindObjectOfType<PlayerManager>().RemoveFromInventory(item);
    }
}
