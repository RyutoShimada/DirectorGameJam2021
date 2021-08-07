using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public enum OperaterState
{
    FirstPlayer,
    SecondPlayer
}

[RequireComponent(typeof(Rigidbody2D))]

public class Player : MonoBehaviour
{
    [SerializeField] int m_hp = 10;
    [SerializeField] int m_damage = 1;
    [SerializeField] float m_speed = 5f;

    [SerializeField] GameObject m_bulletPrefab = null;
    [SerializeField] Transform m_bulletGeneratePos = null;

    public OperaterState m_operater = OperaterState.FirstPlayer;
    Rigidbody2D m_rb2d;
    Vector2 m_velo;
    Vector2 m_dir;

    // Start is called before the first frame update
    void Start()
    {
        m_rb2d = GetComponent<Rigidbody2D>();
        m_velo = m_rb2d.velocity;
        m_dir = Vector2.up;
    }

    void FixedUpdate()
    {
        Move();
    }

    // Update is called once per frame
    void Update()
    {   
        Fire();
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
                Debug.Log($"{m_operater} : Fire!");
                if (!m_bulletPrefab) return;

                Instantiate(m_bulletPrefab, m_bulletGeneratePos.position, m_bulletGeneratePos.rotation);
            }
        }
    }
}
