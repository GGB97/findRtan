using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int myKey;       // 같은 카드인지 확인하기 위한 Key
    public string myName;   // 이 이미지의 주인이 누구인지 확인하기 위한 이름 (지금은 Key와 동일한 값이 들어가지만 enum으로 변형 가능성 있음)
    public int myID;        // 중복 검사를 위한 ID

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
        // 2번째 카드 슬롯이 비어 있을 때 (카드를 한 장만 선택하고 있는 경우)
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
            // 첫 번째 카드의 ID가 나와 같은 ID가 아닐 때(카드를 빠른 속도로 더블 클릭 시 자신이 first/second 둘 다 들어가는 경우가 발생)
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
    //public void closeCard(float delay) // closeCarkInvoke 함수를 public으로 변경하는게 나은지 이렇게 쓰는게 더 나은지.. 성능은 전자가 더 괜찮을것같은데
    //{
    //    Invoke("closeCardInvoke", delay); // 어차피 바로 시작해야하는건데 Invoke를 쓰기엔 낭비같다.
    //}
    
    public void ShowCard()
    {
        animator.SetBool("isOpen", true);
    }
}
