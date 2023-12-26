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
    public AudioClip matchSound;

    public static int level = 1;

    float time = 30f;
    bool is_noTime = false;
    int matchCnt = 0;
    int sCnt = 0;
    int fCnt = 0;
    int totalPoint = 0;

    float penalty = 3f;
    float selectLimit = 5f;
    AudioSource audioSource;

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
        is_Running = true;
        matchCnt = 0;
        sCnt = 0;
        fCnt = 0;
        totalPoint = 0;

        if (level == 3)
        {
            penalty = 5f;
            selectLimit = 3f;
        }
        else
        {
            penalty = 3f;
            selectLimit = 5f;
        }

        end_Canvas.SetActive(false);
        name_Text.gameObject.SetActive(false);

        audioSource = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        if (is_Running)
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

        if(firstCard != null && secondCard == null)
        {
            selectLimit -= Time.deltaTime;
            if (selectLimit <= 0)
            {
                firstCard.closeCardInvoke();
                firstCard = null; selectLimit = 5f;
            }
        }
    }

    public void isMatched()
    {
        int firstKey = firstCard.myKey;
        int secondKey = secondCard.myKey;

        if (firstKey == secondKey)
        {
            selectLimit = 5f;
            name_Text.text = firstCard.myName;

            audioSource.PlayOneShot(matchSound);

            firstCard.destroyCard();
            secondCard.destroyCard();

            if (GameObject.Find("Cards").transform.childCount == 2)
            {
                GameOver();
            }
            else
            {
                time += 5f; // �׳� ���߸� �ð� �þ�� ������ ���Ƽ� ����.
                sCnt++;
            }
        }
        else
        {
            name_Text.text = "����!!";

            time -= penalty;

            firstCard.closeCard();
            secondCard.closeCard();

            fCnt++;
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
        SceneManager.LoadScene("LevelScene");
    }

    void GameOver()
    {
        Time.timeScale = 0f;
        totalPoint = (sCnt * 10) - (fCnt * 5);
        if (totalPoint < 0) totalPoint = 0;

        end_Canvas.transform.GetChild(2).GetComponent<Text>().text = "�õ� Ƚ�� : " + matchCnt;
        end_Canvas.transform.GetChild(3).GetComponent<Text>().text = "���� : " + sCnt;
        end_Canvas.transform.GetChild(4).GetComponent<Text>().text = "���� : " + fCnt;
        end_Canvas.transform.GetChild(5).GetComponent<Text>().text = "���� : " + totalPoint;

        end_Canvas.SetActive(true);
    }
}
