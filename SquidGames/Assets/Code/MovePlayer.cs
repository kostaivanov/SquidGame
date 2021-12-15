using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

internal class MovePlayer : MonoBehaviour
{
    [SerializeField] private Transform[] boxes;
    [SerializeField] private float speed = 1f;
    internal bool moveBlue, moveRed;
    internal int currentIndexBlue, currentIndexRed, initialBlueIndex;
    [SerializeField] private LayerMask collectablesLayer;
    private string _boxIndex;
    internal bool collectableFound;
    private string _buttonColor;

    internal Vector3 startPosition;
    internal bool plusOn, minusOn;
    private bool rotationChanged;
    private void Start()
    {
        moveBlue = false;
        moveRed = false;
        currentIndexBlue = -1;
        currentIndexRed = -1;

        initialBlueIndex = currentIndexBlue;


        collectableFound = false;
        startPosition = this.transform.position;
        rotationChanged = false;
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
        Button button = obj.GetComponent<Button>();
        if (button.interactable == false && this.plusOn == true && buttonColor == "B" && this.gameObject.name.StartsWith("B"))
        {
            button.interactable = true;
            this.plusOn = false;

        }
        else if (this.minusOn == true && buttonColor == "B" && this.gameObject.name.StartsWith("B"))
        {
            button.interactable = false;
            this.minusOn = false;
        }
        else if (button.interactable == false && this.plusOn == true && buttonColor == "R" && this.gameObject.name.StartsWith("R"))
        {
            button.interactable = true;
            this.plusOn = false;
        }
        else if (this.minusOn == true && buttonColor == "R" && this.gameObject.name.StartsWith("R"))
        {
            button.interactable = false;
            this.minusOn = false;
        }
        else if(this.minusOn == false && button.interactable == true)
        {
            this._boxIndex = boxIndex;
            this._buttonColor = buttonColor;
            this.plusOn = false;

            if (buttonColor == "B" && this.gameObject.name.StartsWith("B"))
            {
                moveBlue = true;
                if (currentIndexBlue == 19)
                {
                    currentIndexBlue += 1;
                }
                else
                {
                    currentIndexBlue += int.Parse(_boxIndex);
                    //currentIndexBlue += 1;
                }

            }
            else if (buttonColor == "R" && this.gameObject.name.StartsWith("R"))
            {
                moveRed = true;

                if (currentIndexRed == 19)
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
        Debug.Log(currentIndexBlue);

        if (moveBlue == true && currentIndexBlue < boxes.Length && Vector3.Distance(this.transform.position, boxes[initialBlueIndex + 1].transform.position) > 0.15 && initialBlueIndex < currentIndexBlue)
        {
            Vector3 direction = (boxes[initialBlueIndex + 1].transform.position - this.transform.position);

            Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 90) * direction;
            Quaternion targetRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: rotatedVectorToTarget);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, 2f * Time.deltaTime);
            this.transform.Translate(speed * Time.deltaTime, 0, 0);
            if (Vector3.Distance(this.transform.position, boxes[initialBlueIndex + 1].transform.position) < 0.15)
            {
                initialBlueIndex++;
                if (currentIndexBlue == 10 && rotationChanged == false)
                {
                    this.transform.localScale = new Vector2(0.5f, 0.5f);
                    rotationChanged = true;
                }
            }
        }
        else if (moveBlue == true)
        {
            moveBlue = false;
            
            if (CheckIfIsGrounded() == true)
            {
                collectableFound = true;
            }
        }
        if (moveRed == true && currentIndexRed < boxes.Length && Vector3.Distance(this.transform.position, boxes[currentIndexRed].transform.position) > 0.3)
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
