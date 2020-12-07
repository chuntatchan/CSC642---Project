using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop_Sell : MonoBehaviour
{
    public PlayerData playerData;
    public TMP_Text moneyCounter;
    public TMP_Text nothingToSellText;
    public sellItems[] itemsToSell;

    public void OnEnable() {
        playerData = GameObject.FindObjectOfType<PlayerData>();
        moneyCounter = GameObject.FindGameObjectWithTag("PlayerMoneyTbox").GetComponent<TMP_Text>();
        moneyCounter.text = playerData.moneyCount.ToString();

        if (playerData.weapons.Count == 0) {
            foreach (sellItems item in itemsToSell) {
                item.itemGameObject.SetActive(false);
            }
            nothingToSellText.gameObject.SetActive(true);
        } else {
            for (int i = 0; i < itemsToSell.Length; i++) {
                if (playerData.weapons.Count - 1 < i) {
                    //Debug.Log("Set Active False");
                    itemsToSell[i].itemGameObject.SetActive(false);
                } else {
                    itemsToSell[i].itemGameObject.SetActive(true);
                }
            }
        }
    }

    public void itemSold(int i) {
        if (itemsToSell[i].itemSold == false) {
            itemsToSell[i].itemSold = true;
            playerData.weapons.RemoveAt(0);
            playerData.moneyCount += 50;
            moneyCounter.text = playerData.moneyCount.ToString();
        }
        itemsToSell[i].itemSoldTextbox.text = "SOLD";

    }
}

[System.Serializable]
public class sellItems {
    public bool itemSold = false;
    public GameObject itemGameObject;
    public TMP_Text itemSoldTextbox;
}
