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
    AudioSource openSound;

    bool is_Opened = false;

    private void Start()
    {
        front = transform.GetChild(0);
        back = transform.GetChild(1);
        animator = GetComponent<Animator>();
        openSound = GetComponent<AudioSource>();
    }

    public void openCard()
    {
        if (GameManager.I.secondCard == null)
        {
            animator.SetBool("isOpen", true);
            openSound.Play();

            if (!is_Opened)
            {
                gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.gray;
                is_Opened = true;
            }

            //front.gameObject.SetActive(true); //카드를 뒤집으면 더이상 필요 Active를 껏다켰다 할 필요가 없음.
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
    }

    void destroyCardInvoke()
    {
        Destroy(gameObject);
    }
    public void destroyCard()
    {
        Invoke("destroyCardInvoke", 1.0f);
    }

    public void closeCardInvoke()
    {
        animator.SetBool("isOpen", false);
        //front.gameObject.SetActive(false);
        //back.gameObject.SetActive(true);
    }
    public void closeCard()
    {
        Invoke("closeCardInvoke", 1.0f);
    }
    //public void closeCard(float delay) // closeCarkInvoke 함수를 public으로 변경하는게 나은지 이렇게 쓰는게 더 나은지.. 성능은 전자가 더 괜찮을것같은데
    //{
    //    Invoke("closeCardInvoke", delay); // 어차피 바로 시작해야하는건데 Invoke를 쓰기엔 낭비같다.
    //}
}
