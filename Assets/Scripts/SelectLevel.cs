using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectLevel : MonoBehaviour
{
    public int myLevel;

    public void gameStart()
    {
        GameManager.level = myLevel;
        SceneManager.LoadScene("MainScene");
    }
}
