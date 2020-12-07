using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayPlayerData : MonoBehaviour
{
    public TMP_Text levelBox;
    public TMP_Text XPBox;
    public TMP_Text moneyBox;

    private PlayerData playerData;

    // Start is called before the first frame update
    void Start()
    {
        playerData = GameObject.FindObjectOfType<PlayerData>();

        levelBox.text = "Level " + playerData.playerLevel.ToString();
        XPBox.text = playerData.XPCount.ToString();
        moneyBox.text = playerData.moneyCount.ToString();
    }

}
