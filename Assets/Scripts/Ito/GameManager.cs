using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = new GameManager();

    public bool m_isGame { get; private set; }

    /// <summary>時間経過 </summary>
    private int counter;
    /// <summary>制限時間</summary>
    [SerializeField] Text m_timerText;
    /// <summary>秒数</summary>
    [SerializeField] float m_seconds = 0;
    /// <summary>分数</summary>
    [SerializeField] int m_minutes = 2;
    /// <summary>ゲームオブジェクト参照 </summary>
    [SerializeField] GenerataPlayerPor m_generataPlayerPor = null;
    /// <summary>プレイヤー配列</summary>
    GameObject[] m_playersObj;
    [SerializeField] int m_playercount;
    /// <summary>カウントダウンのイメージ配列 </summary>
    [SerializeField] GameObject[] m_images;
    /// <summary>リザルトイメージ</summary>
    [SerializeField] GameObject m_result;

    [SerializeField] GameObject[] m_syousya;

    [SerializeField] BombGenerater bombGenerater;

    Player[] m_players;

    bool m_gameEnd = false;

    public void GameOver()
    {
        m_isGame = false;
        m_gameEnd = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CoroutineGameStart(5));
        m_playersObj = m_generataPlayerPor.Generater(m_playercount);
        m_players = new Player[m_playersObj.Length];

        m_players[0] = m_playersObj[0].transform.Find("Player1Controller").GetComponent<Player>();
        m_players[1] = m_playersObj[1].transform.Find("Player2Controller").GetComponent<Player>();

        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isGame == true)
        {
            mainTime();
        }

        if (m_gameEnd)
        {
            Destroy(m_timerText);
            Coroutineresult();
        }

    }

    IEnumerator CoroutineGameStart(int waitSeconds)
    {
        for (int i = waitSeconds; i >= 0; i--)
        {
            yield return new WaitForSeconds(1f);

            if (i != 0)
            {
                m_images[i].gameObject.SetActive(true);


                if (i + 1 < m_images.Length)
                {
                    m_images[i + 1].gameObject.SetActive(false);
                }
            }
            else
            {
                m_images[i + 1].gameObject.SetActive(false);
                m_images[i].gameObject.SetActive(true);
                yield return new WaitForSeconds(0.5f);
                m_images[i].gameObject.SetActive(false);
            }
        }
        this.m_isGame = true;

        bombGenerater.StartGenerater();

        foreach (var item in m_players)
        {
            item.gameObject.GetComponent<Player>().CanMove(true);
        }
    }

    void Coroutineresult()
    {
        m_result.SetActive(true);

        if (m_players[0].m_currentHp > m_players[1].m_currentHp)
        {
            m_syousya[0].SetActive(true);
        }
        else if (m_players[0].m_currentHp < m_players[1].m_currentHp)
        {
            m_syousya[1].SetActive(true);
        }
        else
        {
            m_syousya[3].SetActive(true);
        }
    }

    void mainTime()
    {
        if (m_seconds > 0)
        {
            m_seconds -= Time.deltaTime;
        }
        else
        {
            if (m_minutes > 0)
            {
                m_minutes--;
                m_seconds = 59;
            }
            else
            {
                m_minutes = 0;
                m_seconds = 0;
                m_isGame = false;
                Debug.Log("TimeUp!");
            }
        }
        if (m_timerText != null)
        {
            m_timerText.text = $"{m_minutes.ToString("00")} : {m_seconds.ToString("00")}";
        }
    }
}

