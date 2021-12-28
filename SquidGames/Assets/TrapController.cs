using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    private MovePlayer movePlayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.gameObject.tag == "Player")
        {
            movePlayer = otherObject.GetComponent<MovePlayer>();
            string _moveNumber = "2";

            if (otherObject.gameObject.name.StartsWith("R"))
            {
                movePlayer.MovePlayerForward(_moveNumber, "R", this.gameObject);
            }
            else
            {
                movePlayer.MovePlayerForward(_moveNumber, "B", this.gameObject);
            }

        }
    }
}
