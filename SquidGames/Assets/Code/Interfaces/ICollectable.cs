using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal interface ICollectable
{
    void Activate();
    IEnumerator Deactivate();
}
