using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EmptyObjectController : MonoBehaviour, ICollectable
{
    [SerializeField] private GameObject[] collectables;

    private List<MovePlayer> movePlayerList;
    private List<Collider2D> colliders;

    void Start()
    {
        colliders = new List<Collider2D>();
        movePlayerList = new List<MovePlayer>();
    }

    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.gameObject.tag == "Player")
        {
            //Debug.Log(otherObject.gameObject.name + " is abt to take = " + this.gameObject.name);

            if (!colliders.Contains(otherObject))
            {
                colliders.Add(otherObject);
                movePlayerList.Add(otherObject.gameObject.GetComponent<MovePlayer>());

            }
            //if (colliders.Count > 0)
            //{
            //    foreach (Collider2D coll in colliders)
            //    {
            //        movePlayerList.Add(coll.GetComponent<MovePlayer>());
            //    }
            //}
        }
    }

    private void OnTriggerStay2D(Collider2D otherObject)
    {
        if (otherObject.gameObject.tag == "Player" && movePlayerList != null)
        {
            foreach (MovePlayer p in movePlayerList)
            {
                if (p.collectableFound == true && p.move == false)
                {
                    p.collectableFound = false;
                    Debug.Log("empty object = " + otherObject.gameObject.name);
                    StartCoroutine(Deactivate());
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (movePlayerList != null && !movePlayerList.Any())
        {
            movePlayerList.Clear();
            colliders.Clear();
        }
    }

    public IEnumerator Deactivate()
    {
        yield return new WaitForSecondsRealtime(1f);

        //InstantiateItems.SpawnRandomObject(this.collectables, this.gameObject);
        InstantiateItems.SpawnRandomObject(this.gameObject);
        Destroy(this.gameObject);
    }

    public void Activate()
    {
        throw new System.NotImplementedException();
    }
}
