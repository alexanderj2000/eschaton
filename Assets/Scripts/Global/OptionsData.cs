using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsData : MonoBehaviour
{
    public static OptionsData Options;

    void Awake()
    {
        if (Options != null)
        {
            Destroy(gameObject);
            return;
        }

        Options = this;
        DontDestroyOnLoad(gameObject);
    }
}