using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyObjectController : MonoBehaviour, ICollectable
{
    [SerializeField] private GameObject[] collectables;

    private MovePlayer movePlayer;


    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.gameObject.tag == "Player")
        {
            movePlayer = otherObject.GetComponent<MovePlayer>();
            if (movePlayer.move == true)
            {
                movePlayer = null;
            }
            else
            {
                movePlayer.trap = false;
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
