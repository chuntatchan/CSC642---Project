using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButtons : MonoBehaviour {
    public Shop shop;

    public void OnEnable() {
        shop = GameObject.FindObjectOfType<Shop>();
    }

    public void itemBought(int i) {
        shop.itemBought(i);
    }

}
