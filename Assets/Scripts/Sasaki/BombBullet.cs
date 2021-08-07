using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBullet : MonoBehaviour
{
    //弾速
    [SerializeField] private float m_bulletSpeed = 5.0f;

    //爆発アニメーション
    [SerializeField] private GameObject m_exprosionAnime = default;

    //爆破弾消滅制限
    [SerializeField] private float m_bulletDestroySeconds = 2.0f;


    // Start is called before the first frame update
    void Start()
    {
        // プレイヤーの向いている方向に飛ばす
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = this.transform.up * m_bulletSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(BulletDestroy());
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Bullet" && collision.gameObject.tag != "Finish")
        {
            //爆発アニメーション生成
            Instantiate(m_exprosionAnime,
            transform.position,
            transform.rotation);
            //消滅
            Destroy(this.gameObject);
        }
    }
    IEnumerator BulletDestroy()
    {
        yield return new WaitForSeconds(m_bulletDestroySeconds);
        Destroy(gameObject);
    }
}
