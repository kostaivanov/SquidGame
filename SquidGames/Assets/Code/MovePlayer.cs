using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

internal class MovePlayer : MonoBehaviour
{
    [SerializeField] private Transform[] boxes;
    [SerializeField] private float speed = 1f;
    internal bool moveBlue, moveRed;
    internal int currentIndexBlue, currentIndexRed;
    [SerializeField] private LayerMask collectablesLayer;
    private string _boxIndex;
    internal bool collectableFound;
    private string _buttonColor;

    internal Vector3 startPosition;
    internal bool plusOn, minusOn;

    private void Start()
    {
        moveBlue = false;
        moveRed = false;
        currentIndexBlue = -1;
        currentIndexRed = -1;
        collectableFound = false;
        startPosition = this.transform.position;
    }

    private void OnEnable()
    {
        OnClickMove.OnClicked += MovePlayerForward;
    }

    private void OnDisable()
    {
        OnClickMove.OnClicked -= MovePlayerForward;
    }

    public void MovePlayerForward(string boxIndex, string buttonColor, GameObject obj)
    {
        if (this.plusOn == true && buttonColor == "B" && this.gameObject.name.StartsWith("B"))
        {
            Debug.Log("1");
            obj.GetComponent<Button>().interactable = true;
            this.plusOn = false;

        }
        else if (this.minusOn == true && buttonColor == "B" && this.gameObject.name.StartsWith("B"))
        {
            Debug.Log("2");
            obj.GetComponent<Button>().interactable = false;
            this.minusOn = false;
        }
        if (this.plusOn == true &&buttonColor == "R" && this.gameObject.name.StartsWith("R"))
        {
            Debug.Log("3");
            obj.GetComponent<Button>().interactable = true;
        }
        else if (buttonColor == "R" && this.gameObject.name.StartsWith("R") && this.minusOn == true)
        {
            Debug.Log("4");

            obj.GetComponent<Button>().interactable = false;
        }
        else if(this.minusOn == false && this.plusOn == false)
        {
            this._boxIndex = boxIndex;
            this._buttonColor = buttonColor;
            Debug.Log("5");

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
            else if (buttonColor == "R" && this.gameObject.name.StartsWith("R"))
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
    }

    private void Update()
    {
        //Debug.Log(this.gameObject.name + " = " + this.plusOn);
        Debug.Log(this.gameObject.name + " = " + this.minusOn);

        if (moveBlue == true && currentIndexBlue < boxes.Length &&  Vector3.Distance(this.transform.position, boxes[currentIndexBlue].transform.position) > 0.2)
        {
            this.transform.Translate(speed * Time.deltaTime, 0, 0);
        }
        else if (moveBlue == true)
        {
            moveBlue = false;
            
            if (CheckIfIsGrounded() == true)
            {
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
                collectableFound = true;
            }
        }
    }

    //void OnDrawGizmosSelected()
    //{
    //    // Draw a yellow sphere at the transform's position
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawSphere(transform.position, 0.5f);
    //}

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
