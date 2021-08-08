using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombGenerater : MonoBehaviour
{
    [SerializeField] GameObject m_bomb = default; //爆弾のオブジェクト

    [SerializeField] int m_startBombNum = 5; //爆弾の初期配置数

    [SerializeField] int m_MaxBombNum = 100; //爆弾最大数

    float m_bombInterbal = 5.0f; //爆弾生成時間の間隔

    [SerializeField]float m_tenpoUp = 0.5f; //生成ごとに短縮する間隔

    [SerializeField] Transform m_bombPool = null; //プール

    const float c_outLineX = 7.0f; //x軸限界
    const float c_outLineY = 4.0f; //y軸限界

    GameObject[] m_bombArray = new GameObject[100]; //生成した爆弾の格納

    int m_n = 0; //爆弾格納カウンタ

    bool m_bombFlg = false; //アップデート内での制御用フラグ

    private void StartGenerater() //爆弾の初期生成
    {
        GameObject bombObj;
        

        for (int i = 0; i < m_MaxBombNum; i++) //全爆弾生成、非アクティブ化
        {
           bombObj = Instantiate(m_bomb, m_bombPool);
           bombObj.SetActive(false);
        }

        foreach(Transform t in m_bombPool) //初期配置爆弾のみアクティブ化
        {
            m_bombArray[m_n] = t.gameObject;
            if (m_n < m_startBombNum)
            {
                if (!t.gameObject.activeSelf)
                {
                    float bombX = Random.Range(-c_outLineX, c_outLineX);
                    float bombY = Random.Range(-c_outLineY, c_outLineY);

                    t.gameObject.transform.position = new Vector2(bombX, bombY);
                    t.gameObject.SetActive(true);
                }
            }
            m_n++;
        }
        StartCoroutine(BombUpdater());
    }

    private void Update()
    {
        if(m_bombFlg == true)
        {
            StartCoroutine(BombUpdater());
            m_bombFlg = false;
        }
    }

    IEnumerator BombUpdater() //爆弾間隔生成
    {
        yield return new WaitForSeconds(m_bombInterbal);
        float bombX = Random.Range(-c_outLineX, c_outLineX);
        float bombY = Random.Range(-c_outLineY, c_outLineY);

        m_bombArray[m_startBombNum].transform.position = new Vector2(bombX, bombY);
        m_bombArray[m_startBombNum].gameObject.SetActive(true);
        if (m_bombInterbal >= 2.0f)
        {
            m_bombInterbal -= m_tenpoUp;
        }
        m_startBombNum++;
        m_bombFlg = true;
    }
}
