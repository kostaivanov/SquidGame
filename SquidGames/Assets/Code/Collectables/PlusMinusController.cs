using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusMinusController : MonoBehaviour, ICollectable<GameObject>
{
    private MovePlayer player;

    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        player = otherObject.GetComponent<MovePlayer>();
    }

    private void OnTriggerStay2D(Collider2D otherObject)
    {
        if (player != null && player.gameObject.tag == "Player")
        {
            if(player.gameObject.name.StartsWith("B") && player.moveBlue == false)
            {
                Activate(otherObject.gameObject);
            }
            if (player.gameObject.name.StartsWith("R") && player.moveRed == false)
            {
                Activate(otherObject.gameObject);
            }
        }
    }

    public void Activate(GameObject otherObject)
    {
        otherObject.GetComponent<SpriteRenderer>().enabled = true;
        otherObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;

    }
}
