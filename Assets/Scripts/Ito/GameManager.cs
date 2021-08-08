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
    /// <summary>制限時間内に勝敗が決まらなかったときに出るテキスト</summary>
    [SerializeField] Text m_drawText;
    /// <summary>勝敗 </summary>
    [SerializeField] Text m_winne;
    /// <summary>制限時間</summary>
    [SerializeField] Text m_timerText;
    /// <summary>最初のカウントダウン</summary>
    [SerializeField] Text m_countDownText;
    /// <summary>秒数</summary>
    [SerializeField] float m_seconds = 0;
    /// <summary>分数</summary>
    [SerializeField] int m_minutes = 2;
    /// <summary>ゲームオブジェクト参照 </summary>
    [SerializeField] GameObject[] Generate;
    /// <summary>プレイヤー配列</summary>
    Player[] m_players;
    [SerializeField] int m_playercount;
    /// <summary>カウントダウンのイメージ配列 </summary>
    [SerializeField] Image[] m_images;
    /// <summary>リザルトイメージ</summary>
    [SerializeField] Image m_result;

    public void GameOver()
    {
        //if (player[0],m_currenhp > player[1],m_currenhp)
        //{
        //    m_winne.text = "Player1勝利";
        //}
        //else if (player[0],m_currenhp < player[1],m_currenhp)
        //{
        //    m_winne.text = "Playe2勝利";
        //}
        //else
        //{
        //    m_drawText.text = "引き分け";
        //}
    }




    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CoroutineGameStart(5));
        m_drawText.enabled = false;
        // Player[] playe = Generata(2);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isGame == true)
        {
            mainTime();

            if (m_isGame == false)
            {
                Destroy(m_timerText);
                m_countDownText.text = "終了";
                StartCoroutine(Coroutineresult(2));

            }


        }

    }
    IEnumerator CoroutineGameStart(int waitSeconds)
    {
        for (int i = waitSeconds; i >= 0; i--)
        {
            yield return new WaitForSeconds(1f);

            if (i != 0)
            {
                m_countDownText.text = i.ToString();
                m_images[i].gameObject.SetActive(true);


                if (i + 1 < m_images.Length)
                {
                    m_images[i + 1].gameObject.SetActive(false);
                }


            }
            else
            {
                m_images[i + 1].gameObject.SetActive(false);
                m_countDownText.text = "はじめ！";
                m_images[i].gameObject.SetActive(true);
                yield return new WaitForSeconds(0.5f);
                m_images[i].gameObject.SetActive(false);
                m_countDownText.text = "";
            }
        }
        this.m_isGame = true;

        //foreach (var item in m_players)
        //{
        //    //item,CanMove(true);
        //}
    }

    IEnumerator Coroutineresult(int WaitSeconds)
    {
        for( int i = WaitSeconds; i >= 0; i--)
        {
            yield return new WaitForSeconds(1f);

            if( i == 0)
            {
                m_result.gameObject.SetActive(true);
            }
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

