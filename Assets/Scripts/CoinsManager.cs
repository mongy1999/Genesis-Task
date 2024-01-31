using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CoinsManager : MonoBehaviour
{
    public static CoinsManager instance;
    public List<PlayerData> allPlayersData;

    [SerializeField]
    TMP_Text playerCoinsTxt, bankCoinsTxt;
    private void Awake()
    {
        //singletone
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnEnable()
    {
        allPlayersData = new List<PlayerData>();

        PlayerData player1 = new PlayerData();
        player1.currentCoins = 1000;
        player1.bankCoinsBalance = 3000;
        allPlayersData.Add(player1);

        playerCoinsTxt.text = "Coins: " + player1.currentCoins.ToString();
        bankCoinsTxt.text = "Balance: " + player1.bankCoinsBalance.ToString();
    }
    public void ModifyPlayerCurrentCoins(PlayerData player,int amount)
    {
        player.currentCoins += amount;
        playerCoinsTxt.text = "Coins: " + player.currentCoins.ToString();
    }
    public void ModifyPlayerBankCoins(PlayerData player, int amount, bool changeByPercent)
    {
        if (changeByPercent)
        {
            //add 10%
            player.bankCoinsBalance += (int)(player.bankCoinsBalance * .1f);
        }
        else
        {
            player.bankCoinsBalance += amount;
        }
        bankCoinsTxt.text = "Balance: " + player.bankCoinsBalance.ToString();
    }

    public void DepositToBank(PlayerData player, int amount)
    {
        if (player.currentCoins >= amount)
        {
            player.bankCoinsBalance += amount;
            ModifyPlayerCurrentCoins(player, -amount);
            bankCoinsTxt.text = "Balance: " + player.bankCoinsBalance.ToString();
        }
    }
    public void WithdrewFromBank(PlayerData player, int amount)
    {
        if (player.bankCoinsBalance >= amount)
        {
            player.bankCoinsBalance -= amount;
            ModifyPlayerCurrentCoins(player, amount);
            bankCoinsTxt.text = "Balance: " + player.bankCoinsBalance.ToString();
        }
    }
}


