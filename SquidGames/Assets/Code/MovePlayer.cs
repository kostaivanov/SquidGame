using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] private Transform[] boxes;
    [SerializeField] private float speed = 1f;
    internal bool moveBlue, moveRed;
    internal int currentIndexBlue, currentIndexRed;
    [SerializeField] private LayerMask collectablesLayer;
    private string _boxIndex;
    internal bool collectableFound;
    private string _buttonColor;


    private void Start()
    {
        moveBlue = false;
        moveRed = false;
        currentIndexBlue = -1;
        currentIndexRed = -1;
        collectableFound = false;
    }

    private void OnEnable()
    {
        OnClickMove.OnClicked += MovePlayerForward;
    }

    private void OnDisable()
    {
        OnClickMove.OnClicked -= MovePlayerForward;
    }

    public void MovePlayerForward(string boxIndex, string buttonColor)
    {
        this._boxIndex = boxIndex;
        this._buttonColor = buttonColor;

        if (buttonColor == "B" && this.gameObject.name.StartsWith("B"))
        {
            moveBlue = true;
            if (currentIndexBlue == 9)
            {
                currentIndexBlue += 1;
            }
            else
            {
                currentIndexBlue += int.Parse(_boxIndex);

            }
        }
        else if(buttonColor == "R" && this.gameObject.name.StartsWith("R"))
        {
            moveRed = true;
            if (currentIndexRed == 9)
            {
                currentIndexRed += 1;
            }
            else
            {
                currentIndexRed += int.Parse(_boxIndex);
            }
        }

    }

    private void Update()
    {
        if (moveBlue == true && currentIndexBlue < boxes.Length &&  Vector3.Distance(this.transform.position, boxes[currentIndexBlue].transform.position) > 0.2)
        {
            this.transform.Translate(speed * Time.deltaTime, 0, 0);
        }
        else if (moveBlue == true)
        {
            moveBlue = false;
            
            if (CheckIfIsGrounded() == true)
            {
                Debug.Log("Namerihme blue kurrr");

                collectableFound = true;
            }
        }
        if (moveRed == true && currentIndexRed < boxes.Length && Vector3.Distance(this.transform.position, boxes[currentIndexRed].transform.position) > 0.2)
        {
            this.transform.Translate(speed * Time.deltaTime, 0, 0);
        }
        else if (moveRed == true)
        {
            moveRed = false;

            if (CheckIfIsGrounded() == true)
            {
                Debug.Log("Namerihme kurrr");
                collectableFound = true;
            }
        }


    }
    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.5f);
    }

    internal bool CheckIfIsGrounded()
    {
        Collider2D rayCastHit = Physics2D.OverlapCircle(this.transform.position, 0.5f, collectablesLayer);

        if (rayCastHit != null)
        {
            return true;
        }
        return false;
    }
}
