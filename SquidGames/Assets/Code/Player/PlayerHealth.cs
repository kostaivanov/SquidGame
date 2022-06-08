using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerHealth : MonoBehaviour
{
    internal bool dead;
    internal bool numbersChanged;
    // Start is called before the first frame update
    void Start()
    {
        dead = false;
        numbersChanged = false;
    }
    //private void Update()
    //{
    //    if (dead == true)
    //    {

    //    }
    //}
}
