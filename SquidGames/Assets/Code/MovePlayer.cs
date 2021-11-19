using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] private Transform[] boxes;
    public float speed = 1f;
    int currentWP = 0;

    public void MovePlayerForward(int index)
    {
        this.transform.Translate(speed * Time.deltaTime, 0, 0);
    }

    private void Update()
    {
        if (Vector3.Distance(this.transform.position, boxes[currentWP].transform.position) < 0.1)
        {
            currentWP++;
            MovePlayerForward(currentWP);
        }
    }
}
