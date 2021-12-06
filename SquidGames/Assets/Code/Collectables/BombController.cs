using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour, ICollectable
{
    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.gameObject.tag == "Player")
        {
            Activate();
        }
    }

    public void Activate()
    {
        this.gameObject.SetActive(true);
    }
}
