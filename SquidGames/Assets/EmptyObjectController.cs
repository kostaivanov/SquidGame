using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyObjectController : MonoBehaviour
{
    [SerializeField] private GameObject[] collectables;

    private MovePlayer movePlayer;


    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.gameObject.tag == "Player")
        {
            movePlayer = otherObject.GetComponent<MovePlayer>();
        }
    }
    private void OnTriggerStay2D(Collider2D otherObject)
    {
        if (movePlayer != null && movePlayer.collectableFound == true)
        {
            if (movePlayer.gameObject.name.StartsWith("B"))
            {
                movePlayer.collectableFound = false;

                StartCoroutine(Deactivate());
            }
            if (movePlayer.gameObject.name.StartsWith("R"))
            {
                movePlayer.collectableFound = false;

                StartCoroutine(Deactivate());
            }
        }
    }

    public IEnumerator Deactivate()
    {
        yield return new WaitForSecondsRealtime(1f);

        InstantiateItems.SpawnRandomObject(this.collectables, this.gameObject);
        Destroy(this.gameObject);
    }
}
