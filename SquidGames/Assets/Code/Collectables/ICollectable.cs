using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal interface ICollectable<T>
{
    void Activate(T otherObject);
}
