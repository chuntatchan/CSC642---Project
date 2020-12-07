using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryDisplay : MonoBehaviour
{
    private PlayerData playerData;

    public Sprite weaponIcon;
    public GameObject weaponDesc;
    public bool displayWeapons;
    public TMP_Text weaponName;

    public itemSlot[] weaponSlots;

    public void OnEnable() {
        try {
            playerData = GameObject.FindObjectOfType<PlayerData>();
            if (displayWeapons) {
                for (int i = 0; i < playerData.weapons.Count; i++) {
                    weaponSlots[i].icon.sprite = weaponIcon;
                    weaponSlots[i].button.interactable = true;
                }
            }
        } catch {
            Debug.Log("No Player Data");
        }
    }

    public void displayItem() {
        weaponName.text = "Big Knife";
        weaponDesc.SetActive(true);
    }

}

[System.Serializable]
public class itemSlot {
    public Image icon;
    public Button button;
}
