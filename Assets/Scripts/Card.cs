using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int myKey;
    public string myName;

    Transform front;
    Transform back;

    Animator animator;

    bool is_Opened = false;

    private void Start()
    {
        front = transform.GetChild(0);
        back = transform.GetChild(1);
        animator = GetComponent<Animator>();
    }

    public void openCard()
    {
        animator.SetBool("isOpen", true);

        if (!is_Opened)
        {
            gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.gray;
            is_Opened = true;
        }

        //front.gameObject.SetActive(true); //ī�带 �������� ���̻� �ʿ� Active�� �����״� �� �ʿ䰡 ����.
        //back.gameObject.SetActive(false);

        if (GameManager.I.firstCard == null)
        {
            GameManager.I.firstCard = gameObject.GetComponent<Card>();
        }
        else
        {
            GameManager.I.secondCard = gameObject.GetComponent<Card>();
            GameManager.I.isMatched();
        }
    }

    void destroyCardInvoke()
    {
        Destroy(gameObject);
    }
    public void destroyCard()
    {
        Invoke("destroyCardInvoke", 1.0f);
    }

    void closeCardInvoke()
    {
        animator.SetBool("isOpen", false);
        front.gameObject.SetActive(false);
        back.gameObject.SetActive(true);
    }
    public void closeCard()
    {
        Invoke("closeCardInvoke", 1.0f);
    }
}
