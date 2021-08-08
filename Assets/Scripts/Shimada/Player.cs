using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[SerializeField]
public enum OperaterState
{
    FirstPlayer,
    SecondPlayer
}

[RequireComponent(typeof(Rigidbody2D))]

public class Player : MonoBehaviour
{
    [SerializeField] int m_maxHp = 10;
    public int m_currentHp = 10;
    [SerializeField] int m_damage = 1;
    [SerializeField] float m_speed = 5f;

    [SerializeField] GameObject m_bulletPrefab = null;
    [SerializeField] Transform m_bulletGeneratePos = null;

    [SerializeField] Slider m_hpBar = null;
    [SerializeField] Transform m_hpBarPos = null;
    [SerializeField] float m_hpBarPosDirection = 1f;

    [SerializeField] Image[] m_HPCounts = null;

    public OperaterState m_operater = OperaterState.FirstPlayer;
    Rigidbody2D m_rb2d;
    Vector2 m_dir;
    [SerializeField] bool m_canMove;

    // Start is called before the first frame update
    void Start()
    {
        m_rb2d = GetComponent<Rigidbody2D>();
        m_dir = Vector2.up;
        m_currentHp = m_maxHp;
        m_hpBar.value = (float)m_currentHp / m_maxHp;
        m_hpBarPos.position = transform.position + new Vector3(0, m_hpBarPosDirection, 0);
        m_canMove = false;
    }

    void FixedUpdate()
    {
        if (!GameManager.Instance.m_isGame) return;
        if (!m_canMove) return;
        Move();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.m_isGame) return;
        if (!m_canMove) return;
        Fire();
        UpdateHpBarPosition();
    }

    private void Move()
    {
        float h = 0;
        float v = 0;

        if (m_operater == OperaterState.FirstPlayer)
        {
            if (Input.GetKey(KeyCode.W))
            {
                v = 1f;
            }
            if (Input.GetKey(KeyCode.S))
            {
                v = -1f;
            }
            if (Input.GetKey(KeyCode.A))
            {
                h = -1f;
            }
            if (Input.GetKey(KeyCode.D))
            {
                h = 1f;
            }
            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D)))
            {
                v = 0;
                h = 0;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                v = 1f;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                v = -1f;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                h = -1f;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                h = 1f;
            }
            if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow) || (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow)))
            {
                v = 0;
                h = 0;
            }
        }

        m_dir = (Vector2.up * v + Vector2.right * h).normalized;

        if (m_dir != Vector2.zero)
        {
            m_rb2d.velocity = m_dir * m_speed;
            this.transform.up = m_dir;
        }
        else
        {
            m_rb2d.velocity = Vector2.zero;
        }
    }

    void Fire()
    {
        if (m_operater == OperaterState.FirstPlayer)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                Debug.Log($"{m_operater} : Fire!");
                if (!m_bulletPrefab) return;

                Instantiate(m_bulletPrefab, m_bulletGeneratePos.position, m_bulletGeneratePos.rotation);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.RightControl))
            {
                if (!m_bulletPrefab) return;

                Instantiate(m_bulletPrefab, m_bulletGeneratePos.position, m_bulletGeneratePos.rotation);
            }
        }
    }

    /// <summary>
    /// GameManagerから呼ばれる
    /// </summary>
    /// <param name="canMove">動けるかどうか</param>
    public void CanMove(bool canMove)
    {
        m_canMove = canMove;
    }

    void Damage(int damageNum)
    {
        m_currentHp -= damageNum;
        m_hpBar.value = (float)m_currentHp / m_maxHp;
        
        if (m_currentHp <= 0)
        {
            StartCoroutine(GameManager.Instance.GameOver());
        }
        else
        {
            if (m_currentHp + 1 < m_HPCounts.Length)
            {
                m_HPCounts[(int)m_currentHp + 1].gameObject.SetActive(false);
            }
            m_HPCounts[(int)m_currentHp].gameObject.SetActive(true);
        }

    }

    void UpdateHpBarPosition()
    {
        m_hpBarPos.position = transform.position + new Vector3(0, m_hpBarPosDirection, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!GameManager.Instance.m_isGame) return;

        if (collision.collider.tag == "Bullet")
        {
            Damage(m_damage);
        }
    }
}
