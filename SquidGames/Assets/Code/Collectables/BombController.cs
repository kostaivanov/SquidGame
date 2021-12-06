using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour, ICollectable<GameObject>
{
    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.gameObject.tag == "Player")
        {
            Activate(otherObject.gameObject);
        }
    }

    public void Activate(GameObject otherObject)
    {
        otherObject.SetActive(true);
    }
}
