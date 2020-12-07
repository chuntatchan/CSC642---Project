using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToScene : MonoBehaviour
{
    public void moveToScene(string name) {
        SceneManager.LoadScene(name, LoadSceneMode.Single);
    }

    public void QuitApplication() {
        Application.Quit();
    }
}
