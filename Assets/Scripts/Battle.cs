using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Battle : MonoBehaviour {
    public GameObject player;
    public GameObject enemy;

    public GameObject damageIndicator;
    public GameObject winPopUp;

    private PlayerData playerData;

    private bool isPlayerTurn = true;
    private bool isEnemyTurn = false;

    public void OnEnable() {
        try {
            playerData = GameObject.FindObjectOfType<PlayerData>();
        } catch {
            Debug.Log("No Player Data");
        }
    }

    public void playerAttack() {
        if (isPlayerTurn) {
            StartCoroutine(playerAttackAnimation());
        }
    }

    IEnumerator playerAttackAnimation() {
        isPlayerTurn = false;
        int steps = 5;
        //move player forward
        for (int i = 0; i < steps; i++) {
            player.transform.position = new Vector3(player.transform.position.x + 1f, player.transform.position.y, player.transform.position.z);
            yield return new WaitForEndOfFrame();
        }

        //Display HitFlash
        enemy.GetComponent<Image>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        enemy.GetComponent<Image>().color = Color.white;
        yield return new WaitForSeconds(0.1f);

        //Display Damage
        damageIndicator.GetComponent<TMP_Text>().text = Random.Range(10, 20).ToString();
        damageIndicator.transform.position = new Vector3(enemy.transform.position.x, enemy.transform.position.y + 2.5f, enemy.transform.position.z);

        //move player back
        for (int i = 0; i < steps; i++) {
            player.transform.position = new Vector3(player.transform.position.x - 1f, player.transform.position.y, player.transform.position.z);
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(0.2f);
        damageIndicator.transform.position = new Vector3(10f, 10f, 10f);
        StartCoroutine(enemyDeath());
    }

    IEnumerator enemyDeath() {
        float alpha = enemy.GetComponent<Image>().color.a;
        while (enemy.GetComponent<Image>().color.a > 0) {
            enemy.GetComponent<Image>().color = new Color(1,1,1, alpha);
            alpha -= 0.1f;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(0.3f);
        winPopUp.SetActive(true);
        if (playerData) {
            playerData.moneyCount += 25;
            playerData.XPCount += 5;
        }
    }
}
