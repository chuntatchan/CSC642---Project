using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    private static Shop instance = null;

    // Game Instance Singleton
    public static Shop Instance {
        get {
            return instance;
        }
    }

    private void Awake() {
        // if the singleton hasn't been initialized yet
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public PlayerData playerData;
    public TMP_Text moneyCounter;
    public shopItems[] itemsForSale;

    void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (scene.name == "Shop") {
            playerData = GameObject.FindObjectOfType<PlayerData>();

            moneyCounter = GameObject.FindGameObjectWithTag("PlayerMoneyTbox").GetComponent<TMP_Text>();
            moneyCounter.text = playerData.moneyCount.ToString();

            for (int i = 0; i < itemsForSale.Length; i++) {
                itemsForSale[i].itemSoldTextbox = GameObject.FindGameObjectWithTag("Item" + (i + 1) + "Sold").GetComponent<TMP_Text>();
                if (itemsForSale[i].itemSold) {
                    itemsForSale[i].itemSoldTextbox.text = "SOLD";
                }
            }
        }
    }

    public void itemBought(int i) {
        if (itemsForSale[i].itemSold == false) {
            itemsForSale[i].itemSold = true;
            playerData.weapons.Add(itemsForSale[i].item);
            playerData.moneyCount -= 100;
            moneyCounter.text = playerData.moneyCount.ToString();
        }
        itemsForSale[i].itemSoldTextbox.text = "SOLD";

    }
}

[System.Serializable]
public class shopItems {
    public bool itemSold = false;
    public TMP_Text itemSoldTextbox;
    public Item item;
}
