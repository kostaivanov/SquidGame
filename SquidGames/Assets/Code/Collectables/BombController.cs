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
    private List<Collider2D> colliders;

    private void Start()
    {
        colliders = new List<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.gameObject.tag == "Player")
        {
            if (!colliders.Contains(otherObject))
            {
                colliders.Add(otherObject);
            }
            movePlayer = colliders[0].GetComponent<MovePlayer>();
            movePlayer.trap = true;
            Debug.Log("trap = " + otherObject.gameObject.name);
            //playerHealth = otherObject.GetComponent<PlayerHealth>();
        }
    }

    private void OnTriggerStay2D(Collider2D otherObject)
    {
        if (movePlayer != null && movePlayer.collectableFound == true && movePlayer.move == false)
        {
            movePlayer.collectableFound = false;

            Activate();
            StartCoroutine(Explode(this.gameObject, movePlayer.gameObject, movePlayer.startPosition, movePlayer));
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

    private IEnumerator Explode(GameObject bombObject, GameObject playerObject, Vector3 playerStartPosition, MovePlayer movePlayer)
    {
        yield return new WaitForSecondsRealtime(0.5f);
        movePlayer.trap = false;
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
