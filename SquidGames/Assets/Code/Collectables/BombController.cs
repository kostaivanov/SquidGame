using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
internal class BombController : MonoBehaviour, ICollectable
{
    private List<MovePlayer> movePlayerList;
    //private PlayerHealth playerHealth;

    public delegate void BombEventHandler(bool killedByTrap, GameObject bombObject, GameObject playerObject, Vector3 position);
    public static event BombEventHandler OnBombExplodeHandler;
    private List<Collider2D> colliders;

    private void Start()
    {
        colliders = new List<Collider2D>();
        movePlayerList = new List<MovePlayer>();
    }

    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.gameObject.tag == "Player")
        {
            if (!colliders.Contains(otherObject))
            {
                colliders.Add(otherObject);
            }
            //if (colliders.Count > 0)
            //{
                foreach (Collider2D coll in colliders)
                {
                    movePlayerList.Add(coll.GetComponent<MovePlayer>());
                }
                //if (colliders[0].gameObject.name == otherObject.gameObject.name)
                //{
                //    movePlayer = colliders[0].GetComponent<MovePlayer>();
                //}
            //}
            //playerHealth = otherObject.GetComponent<PlayerHealth>();
        }
    }

    private void OnTriggerStay2D(Collider2D otherObject)
    {
        //&& movePlayer.collectableFound == true
        if (otherObject.gameObject.tag == "Player" && movePlayerList != null)
        {
            foreach (MovePlayer p in movePlayerList)
            {
                if (p.move == false && p.trap == true)
                {
                    p.trap = false;
                    Activate();
                    StartCoroutine(Explode(this.gameObject, p.gameObject, p.startPosition, p));
                }
            }          
            //Debug.Log("bomb = " + otherObject.gameObject.name);
        }
    }

    private void OnTriggerExit2D(Collider2D otherObject)
    {
        if (otherObject.gameObject.tag == "Player")
        {
            if (movePlayerList != null && !movePlayerList.Any())
            {
                //movePlayer.trap = false;
                movePlayerList.Clear();
                //movePlayerList = null;
            }
        }
    }

    private IEnumerator Explode(GameObject bombObject, GameObject playerObject, Vector3 playerStartPosition, MovePlayer movePlayer)
    {
        yield return new WaitForSecondsRealtime(0.5f);
        //movePlayer.trap = false;
        //Deactivate(obj);
        //Restart(obj, position);
        OnBombExplodeHandler(false, bombObject, playerObject, playerStartPosition);
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
