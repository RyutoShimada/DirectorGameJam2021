using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExprosionDestroy : MonoBehaviour
{
    private void AfterExprosion()
    {
        Destroy(this.gameObject);
    }
}
