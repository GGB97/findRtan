using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class shuffle : MonoBehaviour
{
    public GameObject base_card;
    public Sprite[] texture;

    // Start is called before the first frame update
    void Start()
    {
        List<int> rtans = new List<int> ();
        int cnt = 0;
        for (int i = 0; i<8; i++) // 0~7 두번씩 할당
        {
            for(int j =0; j< 2; j++)
            {
                rtans.Add(cnt/2);
                cnt++;
            }
        }
        rtans = rtans.OrderBy(item => Random.Range(-1f, 1f)).ToList(); // 리스트 셔플

        cnt = 0;
        for (int i = 0; i < 4; i++)
        {
            // i == y축 값 조절
            for (int j = 0; j < 4; j++)
            {
                // j == x축 값 조절 / 카드 생성 후 이미지 및 key값 할당
                GameObject card = Instantiate(base_card, new Vector3((j * 1.4f) - 2.1f, (i * 1.4f) - 2f, 0), Quaternion.identity, gameObject.transform);
                card.transform.GetChild(0).transform.GetComponent<SpriteRenderer>().sprite = texture[rtans[cnt]];
                card.transform.GetComponent<Card>().myKey = rtans[cnt];
                card.transform.GetComponent<Card>().myName = rtans[cnt].ToString(); // 추후 이름을 Enum으로 해서 넣으면 될듯?
                cnt++;
            }
        }
    }
}
