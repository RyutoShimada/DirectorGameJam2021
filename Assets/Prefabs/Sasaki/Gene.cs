using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gene : MonoBehaviour
{
    [SerializeField] GameObject m_prefabBomb = null;
    [SerializeField] Transform m_pool = null;

    // Start is called before the first frame update
    void Start()
    {
        GameObject go;
        for (int i = 0; i < 100; i++)
        {
            go = Instantiate(m_prefabBomb, m_pool);
            go.SetActive(false);
        }
    }

    public void Generate()
    {
        int i = 0;
        foreach (Transform t in m_pool)
        {
            if (i > 50) break;

            if (!t.gameObject.activeSelf)
            {
                t.gameObject.SetActive(true);
            }
            i++;
        }
    }

    public void UnActive()
    {
        foreach (Transform t in m_pool)
        {
            if (t.gameObject.activeSelf)
            {
                t.gameObject.SetActive(false);
            }
        }
    }
}
