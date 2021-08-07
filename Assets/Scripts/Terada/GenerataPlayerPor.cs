using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerataPlayerPor : MonoBehaviour
{
    [SerializeField] float m_distance;
    [SerializeField] GameObject[] m_playerPrefab = null;

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
        Create(p1, 1);
        Create(p2, 2);
    }

    void Create(Vector2 v, int operater)
    {
        if (m_playerPrefab.Length == 0) return;

        Player player;

        if (operater == 1)
        {
            player = Instantiate(m_playerPrefab[0], v, transform.rotation).GetComponent<Player>();
            player.m_operater = OperaterState.FirstPlayer;
        }
        else
        {
            player = Instantiate(m_playerPrefab[1], v, transform.rotation).GetComponent<Player>();
            player.m_operater = OperaterState.SecondPlayer;
        }
    }

    Vector2 Ramdom()
    {
        Vector2 v2;
        var x = Random.Range(-8.1f, 8.1f);
        var y = Random.Range(-4.1f, 4.1f);
        v2 = new Vector2(x, y);
        return v2;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
