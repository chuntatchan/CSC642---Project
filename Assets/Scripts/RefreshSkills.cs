using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RefreshSkills : MonoBehaviour
{
    public Skill[] allSkills;

    public void Start() {
        foreach (Skill skill in allSkills) {
            skill.currentLevel = 0;
        }
        SceneManager.LoadScene("Hub Screen");
    }
}
