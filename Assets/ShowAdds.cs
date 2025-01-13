using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAdds : MonoBehaviour
{
    public static int adShowed;

    void Update()
    {
        if (adShowed >= 6)
        {
            GetComponent<Interstitial>().ShowAd();
            adShowed = 0;

        }
    }
}
