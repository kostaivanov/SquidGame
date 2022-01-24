using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

internal class BombController : MonoBehaviour, ICollectable
{
    private MovePlayer movePlayer;
    //private PlayerHealth playerHealth;

    public delegate void BombEventHandler(GameObject objBomb, GameObject player, Vector3 position);
    public static event BombEventHandler OnBombExplodeHandler;


    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.gameObject.tag == "Player")
        {
            movePlayer = otherObject.GetComponent<MovePlayer>();
            //playerHealth = otherObject.GetComponent<PlayerHealth>();
        }
    }

    private void OnTriggerStay2D(Collider2D otherObject)
    {
        Debug.Log("staying");
        if (movePlayer != null && movePlayer.collectableFound == true)
        {
            if (movePlayer.gameObject.name.StartsWith("B") )
            {
                movePlayer.collectableFound = false;
                Activate();
                StartCoroutine(Explode(this.gameObject, movePlayer.gameObject, movePlayer.startPosition));
            }
            if (movePlayer.gameObject.name.StartsWith("R") )
            {
                movePlayer.collectableFound = false;
                Activate();
                StartCoroutine(Explode(this.gameObject, movePlayer.gameObject, movePlayer.startPosition));

            }
        }
    }

    private IEnumerator Explode(GameObject bomb, GameObject objPlayer, Vector3 position)
    {
        yield return new WaitForSecondsRealtime(0.5f);
        //Deactivate(obj);
        //Restart(obj, position);
        OnBombExplodeHandler(bomb, objPlayer, position);
    }

    public void Activate()
    {
        this.GetComponent<SpriteRenderer>().enabled = true;
    }

    //protected virtual void OnBombExploded()
    //{
    //    if (OnBombExplodeHandler != null)
    //    {
    //        OnBombExplodeHandler(this.gameObject, this.);
    //    }
    //}
}
