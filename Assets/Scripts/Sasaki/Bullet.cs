using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //弾速
    [SerializeField] private float m_bulletSpeed = 5.0f;

    //爆発アニメーション
    [SerializeField] private GameObject m_exprosionAnime = default;


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
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //爆発アニメーション生成
        Instantiate(m_exprosionAnime,
            transform.position,
            transform.rotation);
        //消滅
        Destroy(this.gameObject);
    }
}
