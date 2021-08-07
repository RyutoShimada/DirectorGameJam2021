using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    //爆弾爆発アニメーション
    [SerializeField] private GameObject m_bomberAnime = default;

    //爆破弾
    [SerializeField] private GameObject m_bombBullet = default;

    [SerializeField] float m_waitForSeconds = 0.5f;

    //爆破弾の最大値
    [SerializeField] int m_bombShot = 10;

    //爆破弾層
    private int m_bombShotMilfiyu = 3;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            //爆発アニメーション生成
            Instantiate(m_bomberAnime,
                transform.position,
                transform.rotation);


            //爆破弾生成
            for (var i = 1; i <= m_bombShot; i++)
            {
                Instantiate(m_bombBullet,
               transform.position,
               Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward));

                if (i == m_bombShot)
                {
                    StartCoroutine(Generate());
                }
            }

            
        }
    }

    IEnumerator Generate()
    {
        yield return new WaitForSeconds(m_waitForSeconds);
        for (var n = 0; n < 4; n++)
        {
            for (var x = 1; x <= m_bombShot; x++)
            {
                Instantiate(m_bombBullet,
                       transform.position,
                       Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward));

            }
            yield return new WaitForSeconds(m_waitForSeconds);
        }
        Destroy(gameObject);
    }
}
