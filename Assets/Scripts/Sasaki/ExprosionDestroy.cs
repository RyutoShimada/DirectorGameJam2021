using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExprosionDestroy : MonoBehaviour
{
    public void AfterExprosion()
    {
        Destroy(this.gameObject);
    }
}
