using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] private Transform[] boxes;
    public float speed = 1f;
    private bool moveBlue, moveRed;
    private int currentIndex;

    private string _boxIndex;
    private string _buttonColor;


    private void Start()
    {
        moveBlue = false;
        moveRed = false;
        currentIndex = 0;
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
        currentIndex += int.Parse(_boxIndex);
        if (buttonColor == "B")
        {
            moveBlue = true;

        }
        else if(buttonColor == "R")
        {
            moveRed = true;
        }

    }

    private void Update()
    {
        if (_buttonColor != null && this.gameObject.name.StartsWith(_buttonColor))
        {
            
            if (moveBlue == true &&  Vector3.Distance(this.transform.position, boxes[currentIndex - 1].transform.position) > 0.2)
            {
                this.transform.Translate(speed * Time.deltaTime, 0, 0);
                Debug.Log(_buttonColor + " - " + "moving 1");
            }
            else if(moveRed == true && Vector3.Distance(this.transform.position, boxes[currentIndex - 1].transform.position) > 0.2)
            {
                this.transform.Translate(speed * Time.deltaTime, 0, 0);
                Debug.Log(_buttonColor + " - " + "moving 2");
            }
            else if(moveBlue == true || moveRed == true)
            {
                moveBlue = false;
                moveRed = false;
            }
        }
    }
}
