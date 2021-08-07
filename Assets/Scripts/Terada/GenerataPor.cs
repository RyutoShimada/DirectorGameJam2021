using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerataPor : MonoBehaviour
{
    [SerializeField] float m_distance;
    [SerializeField] GameObject m_playerPrefab = null;

    public void Start()
    {
        Vector2 p1 = Vector2.zero;
        Vector2 p2 = Vector2.zero;

        p1 = Ramdom();
        p2 = Ramdom();

        float distance = Vector2.Distance(p1, p2);

        if (distance < m_distance)
        {
            // もう一回ランダムな数値を取り直す
            while (true)
            {
                p1 = Ramdom();
                p2 = Ramdom();

                distance = Vector2.Distance(p1, p2);

                if (distance >= m_distance)
                {
                    break;
                }
            }
        }

        // ok
        Create(p1);
        Create(p2);
    }

    void Create(Vector2 v)
    {
        Instantiate(m_playerPrefab, v, transform.rotation);
    }

    Vector2 Ramdom()
    {
        Vector2 v2;
        var x = Random.Range(-8.5f, 8.5f);
        var y = Random.Range(-4.5f, 4.5f);
        v2 = new Vector2(x, y);
        return v2;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
