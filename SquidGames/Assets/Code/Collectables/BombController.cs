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
            movePlayer.trap = true;
            
            //playerHealth = otherObject.GetComponent<PlayerHealth>();
        }
    }

    private void OnTriggerStay2D(Collider2D otherObject)
    {
        if (movePlayer != null && movePlayer.collectableFound == true && movePlayer.move == false)
        {
            movePlayer.collectableFound = false;

            Activate();
            StartCoroutine(Explode(this.gameObject, movePlayer.gameObject, movePlayer.startPosition));
        }
        {
            movePlayer = null;
        }
    }

    private void OnTriggerExit2D(Collider2D otherObject)
    {
        if (otherObject.gameObject.tag == "Player")
        {
            if (movePlayer != null)
            {
                movePlayer.trap = false;
                movePlayer = null;
            }
        }
    }

    private IEnumerator Explode(GameObject bombObject, GameObject playerObject, Vector3 playerStartPosition)
    {
        yield return new WaitForSecondsRealtime(0.5f);
        //Deactivate(obj);
        //Restart(obj, position);
        OnBombExplodeHandler(bombObject, playerObject, playerStartPosition);
    }

    public void Activate()
    {
        this.GetComponent<SpriteRenderer>().enabled = true;
    }

    public IEnumerator Deactivate()
    {
        throw new NotImplementedException();
    }

    //protected virtual void OnBombExploded()
    //{
    //    if (OnBombExplodeHandler != null)
    //    {
    //        OnBombExplodeHandler(this.gameObject, this.);
    //    }
    //}
}
