using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour
{
    PlayerData player;
    //main buttons
    [SerializeField]
    Button groceriesShopBtn, vegtableShopBtn, atmBtn, sleepBtn;
    [SerializeField]
    GameObject groceriesPanel, vegetablePanel, atmPanel;
    //atm
    [SerializeField]
    Button depositBtn, withdrawBtn;
    [SerializeField]
    TMP_InputField bankAmountField;

    GameObject prevPanel;

    [SerializeField]
    GameObject itemPrefab;
    [SerializeField]
    Transform inventoryPanel;
    private void Awake()
    {
        groceriesShopBtn.onClick.AddListener(delegate { LoadPanel(groceriesPanel); });
        vegtableShopBtn.onClick.AddListener(delegate { LoadPanel(vegetablePanel); });
        atmBtn.onClick.AddListener(delegate { LoadPanel(atmPanel); });
        sleepBtn.onClick.AddListener(Sleep);

        depositBtn.onClick.AddListener(delegate { CoinsManager.instance.DepositToBank(player, TextIntValue(bankAmountField.text)); });
        withdrawBtn.onClick.AddListener(delegate { CoinsManager.instance.WithdrewFromBank(player, TextIntValue(bankAmountField.text)); });
    }
    
    private void Start()
    {
        player = CoinsManager.instance.allPlayersData[0];
        player.inventoryItems = new List<ItemData>();
    }
    int TextIntValue(string txt)
    {
        if (txt.Length > 0)
        {
            return int.Parse(txt);
        }
        else
        {
            return 0;
        }
    }
    void LoadPanel(GameObject newPanel)
    {
        if (prevPanel != null)
        {
            prevPanel.SetActive(false);
        }
        newPanel.SetActive(true);
        prevPanel = newPanel;
    }
    void Sleep()
    {
        //add 10% when sleeping
        CoinsManager.instance.ModifyPlayerBankCoins(player, 10, true);
    }
    public Button AddToInventory(ItemData itemToAdd)
    {
        GameObject newItem = Instantiate(itemPrefab, inventoryPanel);
        newItem.GetComponent<ItemData>().itemPrice = itemToAdd.itemPrice;
        newItem.GetComponent<ItemData>().itemName = itemToAdd.itemName;
        newItem.transform.GetChild(0).GetComponent<TMP_Text>().text = itemToAdd.itemName + " " + itemToAdd.itemPrice + "$";
        player.inventoryItems.Add(newItem.GetComponent<ItemData>());
        return newItem.GetComponent<Button>();
    }
    public void RemoveFromInventory(ItemData itemToRemove)
    {
        for (int i = 0; i < player.inventoryItems.Count; i++)
        {
            ItemData item = player.inventoryItems[i];
            if (item.itemName == itemToRemove.itemName)
            {
                Destroy(item.gameObject);
                player.inventoryItems.RemoveAt(i);
            }
        }
    }
}

public class PlayerData
{
    public int currentCoins;
    public int bankCoinsBalance;
    public List<ItemData> inventoryItems;
}


