using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeckOffUnity : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);        
    }
}
