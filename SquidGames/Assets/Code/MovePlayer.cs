using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

internal class MovePlayer : MonoBehaviour
{
    [SerializeField] private Transform[] boxes;
    [SerializeField] private float speed = 1f;
    internal bool moveBlue, moveRed;
    internal int currentIndexBlue, currentIndexRed, initialBlueIndex, initialRedIndex;
    [SerializeField] private LayerMask collectablesLayer;
    private string _boxIndex;
    internal bool collectableFound;
    private string _buttonColor;

    internal Vector3 startPosition;
    internal bool plusOn, minusOn;
    private bool rotationChanged;
    private bool goingBackwardsBlue;
    private bool goingBackwardsRed;

    private void Start()
    {
        moveBlue = false;
        moveRed = false;
        currentIndexBlue = -1;
        currentIndexRed = -1;

        initialBlueIndex = currentIndexBlue;
        initialRedIndex = currentIndexRed;


        collectableFound = false;
        startPosition = this.transform.position;
        rotationChanged = false;

        goingBackwardsBlue = false;
        goingBackwardsRed = false;
    }

    private void OnEnable()
    {
        OnClickMove.OnClicked += MovePlayerForward;
    }

    private void OnDisable()
    {
        OnClickMove.OnClicked -= MovePlayerForward;
    }

    public void MovePlayerOnPuroposeForward(string boxIndex, GameObject obj)
    {
        if (this.gameObject.name.StartsWith("B"))
        {
            moveBlue = true;
            if (currentIndexBlue == 19)
            {
                currentIndexBlue += 1;
            }
            else
            {
                currentIndexBlue += int.Parse(boxIndex);
            }
        }
        if (this.gameObject.name.StartsWith("R"))
        {
            moveRed = true;
            if (currentIndexRed == 19)
            {
                currentIndexRed += 1;
            }
            else
            {
                currentIndexRed += int.Parse(boxIndex);
            }
        }
    }

    public void MovePlayerOnPuroposeBackward(string boxIndex, GameObject obj)
    {
        if (this.gameObject.name.StartsWith("B"))
        {
            moveBlue = true;
            goingBackwardsBlue = true;
            RotatePlayer();
            if (currentIndexBlue == 19)
            {
                currentIndexBlue += 1;
            }
            else
            {
                currentIndexBlue -= int.Parse(boxIndex);
            }
        }
        if (this.gameObject.name.StartsWith("R"))
        {
            moveRed = true;
            goingBackwardsRed = true;

            RotatePlayer();
            if (currentIndexRed == 19)
            {
                currentIndexRed += 1;
            }
            else
            {
                currentIndexRed -= int.Parse(boxIndex);
            }
        }
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
        else if (this.minusOn == false && button.interactable == true)
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
                }
                button.interactable = false;
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
                button.interactable = false;
            }
        }
    }

    private void Update()
    {
        //Debug.Log("current index = " + currentIndexBlue + " - and  initial = " + initialBlueIndex);

        if (moveBlue == true && currentIndexBlue < boxes.Length && initialBlueIndex < 20 && Vector3.Distance(this.transform.position, boxes[initialBlueIndex + 1].transform.position) > 0.1 && initialBlueIndex < currentIndexBlue)
        {
            Vector3 direction = (boxes[initialBlueIndex + 1].transform.position - this.transform.position);

            Move(direction, boxes[initialBlueIndex + 1]);

            if (Vector3.Distance(this.transform.position, boxes[initialBlueIndex + 1].transform.position) < 0.1)
            {
                initialBlueIndex++;
                if (initialBlueIndex == 10 && rotationChanged == false)
                {
                    RotatePlayer();
                    rotationChanged = true;
                }
            }
        }
        else if (goingBackwardsBlue == false && moveBlue == true)
        {
            moveBlue = false;

            if (StayOnTopOfCollectable() == true && collectableFound == false)
            {
                collectableFound = true;
            }
        }
        if (currentIndexRed < boxes.Length && initialRedIndex < 20 && moveRed == true && Vector3.Distance(this.transform.position, boxes[currentIndexRed].transform.position) > 0.25 && initialRedIndex < currentIndexRed)
        {
            Vector3 direction = (boxes[initialRedIndex + 1].transform.position - this.transform.position);

            Move(direction, boxes[initialRedIndex + 1]);

            if (Vector3.Distance(this.transform.position, boxes[initialRedIndex + 1].transform.position) < 0.25)
            {
                initialRedIndex++;
                if (initialRedIndex == 10 && rotationChanged == false)
                {
                    RotatePlayer();
                    rotationChanged = true;
                }
            }
        }
        else if (goingBackwardsRed == false && moveRed == true)
        {
            moveRed = false;

            if (StayOnTopOfCollectable() == true)
            {
                collectableFound = true;
            }
        }

        if (goingBackwardsBlue == true && moveBlue == true && currentIndexBlue >= 0 && initialBlueIndex > 0  && Vector3.Distance(this.transform.position, boxes[initialBlueIndex - 1].transform.position) > 0.1 && initialBlueIndex > currentIndexBlue)
        {
            Vector3 direction = (boxes[initialBlueIndex - 1].transform.position - this.transform.position);

            Move(direction, boxes[initialBlueIndex - 1]);
            if (Vector3.Distance(this.transform.position, boxes[initialBlueIndex - 1].transform.position) < 0.1)
            {
                initialBlueIndex--;
                if (initialBlueIndex == 10 && rotationChanged == false)
                {
                    RotatePlayer();
                    rotationChanged = true;
                }
            }
        }
        else if (goingBackwardsBlue == true && moveBlue == true)
        {
            moveBlue = false;
            goingBackwardsBlue = false;
            //this.transform.rotation = Quaternion.Euler(0, 0, 0);
            RotatePlayer();
            if (StayOnTopOfCollectable() == true)
            {
                collectableFound = true;
            }
        }
        if (goingBackwardsRed == true && moveRed == true && currentIndexRed >= 0 && initialRedIndex > 0 && Vector3.Distance(this.transform.position, boxes[initialRedIndex - 1].transform.position) > 0.1 && initialRedIndex > currentIndexRed)
        {
            Vector3 direction = (boxes[initialRedIndex - 1].transform.position - this.transform.position);

            Move(direction, boxes[initialRedIndex - 1]);
            if (Vector3.Distance(this.transform.position, boxes[initialRedIndex - 1].transform.position) < 0.1)
            {
                initialRedIndex--;
                if (currentIndexRed == 10 && rotationChanged == false)
                {
                    RotatePlayer();
                    rotationChanged = true;
                }
            }
        }
        else if (goingBackwardsRed == true && moveRed == true)
        {
            moveRed = false;
            goingBackwardsRed = false;
            RotatePlayer();
            if (StayOnTopOfCollectable() == true)
            {
                collectableFound = true;
            }
        }
    }

    private void RotatePlayer()
    {
        if (this.transform.localScale.x < 0)
        {
            this.transform.localScale = new Vector2(0.5f, 0.5f);
        }
        else
        {
            this.transform.localScale = new Vector2(-0.5f, 0.5f);
        }
        //if (this.transform.rotation.y != 0f)
        //{
        //    this.transform.rotation = Quaternion.Euler(0, 0, 0);
        //}
        //else if (this.transform.rotation.y == 0f)
        //{
        //    this.transform.rotation = Quaternion.Euler(0, this.transform.rotation.y - 180, 0);
        //}
    }

    private void Move(Vector3 direction, Transform target)
    {
        this.transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    //void OnDrawGizmosSelected()
    //{
    //    // Draw a yellow sphere at the transform's position
    //    //Vector3 direction = (boxes[initialBlueIndex + 1].transform.position - this.transform.position);
    //    Gizmos.color = Color.red;
    //    //Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 90) * direction;
    //    Debug.DrawRay(transform.position, Vector3.forward, Color.red);
    //}

    internal bool StayOnTopOfCollectable()
    {
        Collider2D rayCastHit = Physics2D.OverlapCircle(this.transform.position, 0.5f, collectablesLayer);

        if (rayCastHit != null)
        {
            return true;
        }
        return false;
    }
}
