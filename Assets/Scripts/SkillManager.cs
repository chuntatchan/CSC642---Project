using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillManager : MonoBehaviour {
    private PlayerData playerData;

    public skillDisplay[] swordSkills;
    public skillDisplay[] coinSkills;

    public TMP_Text XPTbox;
    public TMP_Text SkillName;
    public TMP_Text SkillLevel;
    public TMP_Text DescriptionTbox;

    public GameObject hideableDesc;

    public Slider levelBar;
    public Image levelFillBar;

    public Color buyColor;
    public Color cannotBuyColor;

    public int swordSkillCost(int i, Skill skill) {
        return (skill.currentLevel * 100 * swordSkills[i].skillPrereq * 2) + 100;
    }

    public int coinSkillCost(int i, Skill skill) {
        return (skill.currentLevel * 100 * coinSkills[i].skillPrereq * 2) + 100;
    }

    public void OnEnable() {
        playerData = GameObject.FindObjectOfType<PlayerData>();
        XPTbox.text = playerData.XPCount.ToString();

        foreach (Skill skill in playerData.swordSkills) {
            if (skill.currentLevel > 0) {
                playerData.swordSkillPrereq++;
            }
        }

        foreach (Skill skill in playerData.coinSkills) {
            if (skill.currentLevel > 0) {
                playerData.coinSkillPrereq++;
            }
        }

        showSkillShader();
    }

    private void showSkillShader() {
        int i = 0;
        //Debug.Log("ShowShaders");
        foreach (skillDisplay skill in swordSkills) {
            if (skill.skillPrereq <= playerData.swordSkillPrereq) {
                if (skill.shader != null) {
                    skill.shader.SetActive(false);
                    //Debug.Log(i.ToString() + " : hide");
                    i++;
                }
            } else {
                skill.shader.SetActive(true);
                //Debug.Log(i.ToString() + " : show");
                i++;
            }
        }

        foreach (skillDisplay skill in coinSkills) {
            if (skill.skillPrereq <= playerData.coinSkillPrereq) {
                if (skill.shader != null) {
                    skill.shader.SetActive(false);
                }
            } else {
                skill.shader.SetActive(true);
            }
        }
    }

    public void UpgradeSwordSkill(int i) {
        Skill skill = playerData.swordSkills[i];
        if (skill.currentLevel == 0) {
            playerData.swordSkillPrereq += 1;
            showSkillShader();
        }
        playerData.XPCount -= swordSkillCost(i, skill);
        XPTbox.text = playerData.XPCount.ToString();
        playerData.swordSkills[i].currentLevel += 1;
        displaySwordSkill(i);
    }

    public void UpgradeCoinSkill(int i) {
        Skill skill = playerData.coinSkills[i];
        if (skill.currentLevel == 0) {
            playerData.coinSkillPrereq += 1;
            showSkillShader();
        }
        playerData.XPCount -= coinSkillCost(i, skill);
        XPTbox.text = playerData.XPCount.ToString();
        playerData.coinSkills[i].currentLevel += 1;
        displayCoinSkill(i);
    }

    public void displaySwordSkill(int i) {
        hideAllButtons();
        hideableDesc.SetActive(true);
        Skill skill = playerData.swordSkills[i];
        Button buyButton = swordSkills[i].button.GetComponent<Button>();
        Image buyButtonImg = swordSkills[i].button.GetComponent<Image>();
        TMP_Text Cost = buyButton.gameObject.GetComponentInChildren<TMP_Text>();
        DescriptionTbox.text = skill.skill_description;

        SkillName.text = skill.skill_name;
        SkillLevel.text = "Level: " + skill.currentLevel.ToString() + "/" + skill.maxLevel.ToString();
        Cost.text = swordSkillCost(i, skill).ToString() + "XP";

        levelFillBar.color = Color.red;
        levelBar.maxValue = skill.maxLevel;
        levelBar.value = skill.currentLevel;

        buyButton.gameObject.SetActive(true);

        if (swordSkillCost(i, skill) > playerData.XPCount || skill.currentLevel == skill.maxLevel || swordSkills[i].skillPrereq > playerData.swordSkillPrereq) {
            buyButton.interactable = false;
            buyButtonImg.color = cannotBuyColor;
        } else {
            buyButton.interactable = true;
            buyButtonImg.color = buyColor;
        }
    }

    public void displayCoinSkill(int i) {
        hideAllButtons();
        hideableDesc.SetActive(true);
        Skill skill = playerData.coinSkills[i];
        Button buyButton = coinSkills[i].button.GetComponent<Button>();
        Image buyButtonImg = coinSkills[i].button.GetComponent<Image>();
        TMP_Text Cost = buyButton.gameObject.GetComponentInChildren<TMP_Text>();

        SkillName.text = skill.skill_name;
        SkillLevel.text = "Level: " + skill.currentLevel.ToString() + "/" + skill.maxLevel.ToString();
        Cost.text = coinSkillCost(i, skill).ToString() + "XP";
        DescriptionTbox.text = skill.skill_description;

        levelFillBar.color = Color.yellow;
        levelBar.maxValue = skill.maxLevel;
        levelBar.value = skill.currentLevel;

        buyButton.gameObject.SetActive(true);

        if (coinSkillCost(i, skill) > playerData.XPCount || skill.currentLevel == skill.maxLevel || coinSkills[i].skillPrereq > playerData.coinSkillPrereq) {
            buyButton.interactable = false;
            buyButtonImg.color = cannotBuyColor;
        } else {
            buyButton.interactable = true;
            buyButtonImg.color = buyColor;
        }
    }

    private void hideAllButtons() {
        foreach (skillDisplay skill in swordSkills) {
            skill.button.SetActive(false);
        }
        foreach (skillDisplay skill in coinSkills) {
            skill.button.SetActive(false);
        }

    }
}

[System.Serializable]
public class skillDisplay {
    public GameObject shader;
    public int skillPrereq;
    public GameObject button;
}
