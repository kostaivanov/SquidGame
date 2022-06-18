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
    private Animator animator;
    private PointEffector2D pointEffector;

    public delegate void BombEventHandler(bool killedByTrap, GameObject bombObject, GameObject playerObject, Vector3 position);
    public static event BombEventHandler OnBombExplodeHandler;
    private List<Collider2D> colliders;
    private List<Rigidbody2D> playerRigidBodies;

    private MovePlayer player;

    private void Start()
    {
        colliders = new List<Collider2D>();
        movePlayerList = new List<MovePlayer>();
        animator = GetComponent<Animator>();
        pointEffector = GetComponent<PointEffector2D>();
        animator.enabled = false;
        pointEffector.enabled = false;
        playerRigidBodies = new List<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.gameObject.tag == "Player")
        {
            Debug.Log("bomb = " + otherObject.gameObject.name);

            if (!colliders.Contains(otherObject))
            {
                colliders.Add(otherObject);
                playerRigidBodies.Add(otherObject.gameObject.GetComponent<Rigidbody2D>());
                foreach (Transform rb in otherObject.gameObject.transform)
                {
                    playerRigidBodies.Add(rb.gameObject.GetComponent<Rigidbody2D>());
                }
            }

            foreach (Collider2D coll in colliders)
            {
                movePlayerList.Add(coll.GetComponent<MovePlayer>());
            }

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
                    player = p;
                }
            }          
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
        yield return new WaitForSecondsRealtime(2f);
        //movePlayer.trap = false;
        //Deactivate(obj);
        //Restart(obj, position);
        OnBombExplodeHandler(false, bombObject, playerObject, playerStartPosition);
    }

    public void Activate()
    {
        animator.enabled = true;
        pointEffector.enabled = true;
        this.GetComponent<SpriteRenderer>().enabled = true;
    }

    public IEnumerator Deactivate()
    {
        throw new NotImplementedException();
    }

    public void Restart()
    {
        StartCoroutine(Explode(this.gameObject, player.gameObject, player.startPosition, player));

    }

    public void SetAllRigidBodiesToDynamic()
    {
        player.GetComponent<Animator>().enabled = false;
        foreach (Rigidbody2D rb in playerRigidBodies)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            Debug.Log(rb.gameObject.name);
        }
    }
    //protected virtual void OnBombExploded()
    //{
    //    if (OnBombExplodeHandler != null)
    //    {
    //        OnBombExplodeHandler(this.gameObject, this.);
    //    }
    //}
}
