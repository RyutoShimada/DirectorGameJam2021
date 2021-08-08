using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOuter : MonoBehaviour
{
    [SerializeField]GameObject m_fadeEffecter = default; //画面いっぱいの無地

    // Start is called before the first frame update
    public void OnClick()
    {
        Instantiate(m_fadeEffecter, transform.position, transform.rotation);
    }
}
