using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusMinusController : MonoBehaviour, ICollectable
{
    private MovePlayer player;

    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        player = otherObject.GetComponent<MovePlayer>();
        if (player != null)
        {
            Debug.Log(player.gameObject.name);
        }
    }

    private void OnTriggerStay2D(Collider2D otherObject)
    {
        Debug.Log(otherObject.gameObject.name);
        if (player != null && player.gameObject.tag == "Player")
        {
            Debug.Log(player.gameObject.name);

            if (player.gameObject.name.StartsWith("B") && player.moveBlue == false)
            {
                Activate();
            }
            if (player.gameObject.name.StartsWith("R") && player.moveRed == false)
            {
                Activate();
            }
        }
    }

    public void Activate()
    {
        this.GetComponent<SpriteRenderer>().enabled = true;
        this.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;

    }
}
