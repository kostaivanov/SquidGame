using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal interface IDestroyable
{
    void Deactivate(bool trapOrSkill, GameObject obj1, GameObject obj2, Vector3 position);

    void Restart(bool trapOrSkill, GameObject obj1, GameObject obj2, Vector3 position);
}
