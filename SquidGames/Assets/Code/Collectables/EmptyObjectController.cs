using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyObjectController : MonoBehaviour, ICollectable
{
    [SerializeField] private GameObject[] collectables;

    private MovePlayer movePlayer;
    private List<Collider2D> colliders;

    void Start()
    {
        colliders = new List<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.gameObject.tag == "Player")
        {
            Debug.Log(otherObject.gameObject.name + " is abt to take = " + this.gameObject.name);

            if (!colliders.Contains(otherObject))
            {
                colliders.Add(otherObject);
            }
            if (colliders.Count > 0)
            {
                if (colliders[0].gameObject.name == otherObject.gameObject.name)
                {
                    movePlayer = colliders[0].GetComponent<MovePlayer>();
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D otherObject)
    {
        if (movePlayer != null && movePlayer.collectableFound == true && movePlayer.move == false)
        {
            movePlayer.collectableFound = false;

            StartCoroutine(Deactivate());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (movePlayer != null)
        {
            movePlayer = null;
            colliders.Clear();
        }
    }

    public IEnumerator Deactivate()
    {
        yield return new WaitForSecondsRealtime(1f);

        InstantiateItems.SpawnRandomObject(this.collectables, this.gameObject);
        Destroy(this.gameObject);
    }

    public void Activate()
    {
        throw new System.NotImplementedException();
    }
}
