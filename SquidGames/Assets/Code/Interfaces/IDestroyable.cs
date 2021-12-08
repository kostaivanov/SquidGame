using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal interface IDestroyable
{
    void Deactivate(GameObject obj);

    void Restart(GameObject obj, Vector3 position);
}
