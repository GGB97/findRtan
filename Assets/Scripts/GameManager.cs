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
    public GameObject end_Canvas;
    public Text name_Text;
    public Card firstCard;
    public Card secondCard;


    float time = 30f;
    bool is_noTime = false;
    int matchCnt = 0;

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
        is_noTime = false;
        matchCnt = 0;

        end_Canvas.SetActive(false);
        name_Text.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        time_Text.text = time.ToString("N2");

        if(!is_noTime && time < 10f) // ���� �ð����� time_Text ���� ����
        {
            is_noTime = true;
            time_Text.color = Color.red;
        }
        else if(is_noTime && time > 10f)
        {
            is_noTime = false;
            time_Text.color = Color.white;
        }

        if (time <= 0)
            GameOver();
    }

    public void isMatched()
    {
        int firstKey = firstCard.myKey;
        int secondKey = secondCard.myKey;

        if (firstKey == secondKey)
        {
            name_Text.text = firstCard.myName;

            firstCard.destroyCard();
            secondCard.destroyCard();

            if (GameObject.Find("Cards").transform.childCount == 2)
            {
                GameOver();
            }
            else
            {
                time += 5f; // �׳� ���߸� �ð� �þ�� ������ ���Ƽ� ����.
            }
        }
        else
        {
            name_Text.text = "����!!";

            firstCard.closeCard();
            secondCard.closeCard();
        }

        firstCard = null; secondCard = null;

        name_Text.gameObject.SetActive(true); // �̸� text Ȱ��ȭ
        Invoke("close_nameText", 1f);

        matchCnt++;

    }

    void close_nameText()
    {
        name_Text.gameObject.SetActive(false);
    }

    public void reStart()
    {
        SceneManager.LoadScene("MainScene");
    }

    void GameOver()
    {
        Time.timeScale = 0f;
        end_Canvas.transform.GetChild(1).GetComponent<Text>().text = "�õ� Ƚ�� : " + matchCnt;
        end_Canvas.SetActive(true);
    }
}
