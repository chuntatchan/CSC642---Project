using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private static PlayerData instance = null;

    // Game Instance Singleton
    public static PlayerData Instance {
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

    public int playerLevel;
    public int XPCount;
    public int moneyCount;

    public List<Item> weapons;
    public List<Skill> swordSkills;
    public int swordSkillPrereq;
    public List<Skill> coinSkills;
    public int coinSkillPrereq;
}
