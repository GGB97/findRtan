using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager I;
    public Text time_Text;
    public GameObject end_Text;

    float time = 30f;

    public Card firstCard;
    public Card secondCard;

    private void Awake()
    {
        I = this;
    }

    private void Start()
    {
        Game_init();
    }

    private void Game_init()
    {
        Time.timeScale = 1f;
        time = 30f;
    }
    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        time_Text.text = time.ToString("N2");
    }

    public void isMatched()
    {
        int firstKey = firstCard.myKey;
        int secondKey = secondCard.myKey;

        if (firstKey == secondKey)
        {
            firstCard.destroyCard();
            secondCard.destroyCard();

            if(GameObject.Find("Cards").transform.childCount == 2)
            {
                Time.timeScale = 0f;
                end_Text.gameObject.SetActive(true);
            }
            else
            {
                time += 5f;
            }
        }
        else
        {
            firstCard.closeCard();
            secondCard.closeCard();
        }

        firstCard = null; secondCard = null;
    }

    public void reStart()
    {
        SceneManager.LoadScene("MainScene");
    }
}
