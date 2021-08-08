using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerataPlayerPor : MonoBehaviour
{
    [SerializeField] float m_distance;
    [SerializeField] GameObject[] m_playerPrefab = null;

    public GameObject[] Generater(int count)
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

        GameObject[] players = new GameObject[count];
        players[0] = Create(p1, 1);
        players[1] = Create(p2, 2);

        return players;
    }

    GameObject Create(Vector2 v, int operater)
    {
        if (m_playerPrefab.Length == 0) return null;

        GameObject player;

        if (operater == 1)
        {
            player = Instantiate(m_playerPrefab[0], v, transform.rotation);
            player.GetComponent<Player>().m_operater = OperaterState.FirstPlayer;
        }
        else
        {
            player = Instantiate(m_playerPrefab[1], v, transform.rotation);
            player.GetComponent<Player>().m_operater = OperaterState.SecondPlayer;
        }

        return player;
    }

    Vector2 Ramdom()
    {
        Vector2 v2;
        var x = Random.Range(-8.1f, 8.1f);
        var y = Random.Range(-4.1f, 4.1f);
        v2 = new Vector2(x, y);
        return v2;
    }
}

