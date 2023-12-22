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
        for (int i = 0; i<8; i++) // 0~7 �ι��� �Ҵ�
        {
            for(int j =0; j< 2; j++)
            {
                rtans.Add(cnt/2);
                cnt++;
            }
        }
        rtans = rtans.OrderBy(item => Random.Range(-1f, 1f)).ToList(); // ����Ʈ ����

        cnt = 0;
        for (int i = 0; i < 4; i++)
        {
            // i == y�� �� ����
            for (int j = 0; j < 4; j++)
            {
                // j == x�� �� ���� / ī�� ���� �� �̹��� �� key�� �Ҵ�
                GameObject card = Instantiate(base_card, new Vector3((j * 1.4f) - 2.1f, (i * 1.4f) - 2f, 0), Quaternion.identity, gameObject.transform);
                card.transform.GetChild(0).transform.GetComponent<SpriteRenderer>().sprite = texture[rtans[cnt]];
                card.transform.GetComponent<Card>().myKey = rtans[cnt];
                card.transform.GetComponent<Card>().myName = rtans[cnt].ToString(); // ���� �̸��� Enum���� �ؼ� ������ �ɵ�?
                cnt++;
            }
        }
    }
}
