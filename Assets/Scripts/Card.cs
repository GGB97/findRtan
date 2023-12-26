using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int myKey;       // ���� ī������ Ȯ���ϱ� ���� Key
    public string myName;   // �� �̹����� ������ �������� Ȯ���ϱ� ���� �̸� (������ Key�� ������ ���� ������ enum���� ���� ���ɼ� ����)
    public int myID;        // �ߺ� �˻縦 ���� ID

    Transform front;
    Transform back;

    Animator animator;
    AudioSource openSound;

    bool is_Opened = false;

    private void Awake()
    {
        front = transform.GetChild(0);
        back = transform.GetChild(1);
        animator = GetComponent<Animator>();
        openSound = GetComponent<AudioSource>();
    }

    public void openCard()
    {
        // 2��° ī�� ������ ��� ���� �� (ī�带 �� �常 �����ϰ� �ִ� ���)
        if (GameManager.I.secondCard == null)
        {
            animator.SetBool("isOpen", true);
            openSound.Play();

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
            // ù ��° ī���� ID�� ���� ���� ID�� �ƴ� ��(ī�带 ���� �ӵ��� ���� Ŭ�� �� �ڽ��� first/second �� �� ���� ��찡 �߻�)
            else if (GameManager.I.firstCard.myID != myID) 
            {
                GameManager.I.secondCard = gameObject.GetComponent<Card>();
                GameManager.I.isMatched();
            }
        }
    }

    void destroyCardInvoke()
    {
        Destroy(gameObject);
    }
    public void destroyCard()
    {
        Invoke(nameof(destroyCardInvoke), 1.0f);
    }

    public void closeCardInvoke()
    {
        animator.SetBool("isOpen", false);
        //front.gameObject.SetActive(false);
        //back.gameObject.SetActive(true);
    }
    public void closeCard()
    {
        Invoke(nameof(closeCardInvoke), 1.0f);
    }
    //public void closeCard(float delay) // closeCarkInvoke �Լ��� public���� �����ϴ°� ������ �̷��� ���°� �� ������.. ������ ���ڰ� �� �������Ͱ�����
    //{
    //    Invoke("closeCardInvoke", delay); // ������ �ٷ� �����ؾ��ϴ°ǵ� Invoke�� ���⿣ ���񰰴�.
    //}
    
    public void ShowCard()
    {
        animator.SetBool("isOpen", true);
    }
}
