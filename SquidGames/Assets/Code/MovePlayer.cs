using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

internal class MovePlayer : MonoBehaviour
{
    [SerializeField] private Transform[] boxes;
    [SerializeField] private float speed = 1f;
    internal bool move;
    internal int currentIndex, initialIndex;
    [SerializeField] private LayerMask collectablesLayer;
    private string _boxIndex;
    internal bool collectableFound;
    private string _buttonColor;

    internal Vector3 startPosition;
    internal bool plusOn, minusOn;
    private bool rotationChanged;

    private bool goingBackwards;


    private void Start()
    {
        move = false;

        currentIndex = -1;
        initialIndex = -1;

        initialIndex = currentIndex;

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

    public void MovePlayerOnPuroposeForward(int numberOfMoves, GameObject obj)
    {
        if (this.gameObject.name == obj.name)
        {
            move = true;
            if (currentIndex == 19)
            {
                currentIndex += 1;
            }
            else
            {
                currentIndex += numberOfMoves;
            }
        }
    }

    public void MovePlayerOnPuroposeBackward(int numberOfMoves, GameObject obj)
    {
        if (this.gameObject.name == obj.name)
        {
            move = true;
            goingBackwards = true;
            RotatePlayer();
            if (currentIndex == 19)
            {
                currentIndex += 1;
            }
            else
            {
                currentIndex -= numberOfMoves;
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
                move = true;
                if (currentIndex == 19)
                {
                    currentIndex += 1;
                }
                else
                {
                    currentIndex += int.Parse(_boxIndex);
                }
                button.interactable = false;
            }
            else if (buttonColor == "R" && this.gameObject.name.StartsWith("R"))
            {
                move = true;

                if (currentIndex == 19)
                {
                    currentIndex += 1;
                }
                else
                {
                    currentIndex += int.Parse(_boxIndex);
                }
                button.interactable = false;
            }
        }
    }

    private void Update()
    {
        //Debug.Log("current index = " + currentIndexBlue + " - and  initial = " + initialBlueIndex);
        //if (moveBlue == true || moveRed == true || move == true)
        //{
        //    Physics2D.IgnoreLayerCollision(6, 8);
        //    Physics2D.IgnoreLayerCollision(7, 8);
        //    Debug.Log("ignoring collision");
        //}
        if (move == true && currentIndex < boxes.Length && initialIndex < 20 && Vector3.Distance(this.transform.position, boxes[initialIndex + 1].transform.position) > 0.1 && initialIndex < currentIndex)
        {
            Vector3 direction = (boxes[initialIndex + 1].transform.position - this.transform.position);

            Move(direction, boxes[initialIndex + 1]);

            if (Vector3.Distance(this.transform.position, boxes[initialIndex + 1].transform.position) < 0.1)
            {
                initialIndex++;
                if (initialIndex == 10 && rotationChanged == false)
                {
                    RotatePlayer();
                    rotationChanged = true;
                }
            }
        }
        else if (goingBackwards == false && move == true)
        {
            move = false;

            if (StayOnTopOfCollectable() == true && collectableFound == false)
            {
                collectableFound = true;
            }
        }

        if (goingBackwards == true && move == true && currentIndex >= 0 && initialIndex > 0 && Vector3.Distance(this.transform.position, boxes[initialIndex - 1].transform.position) > 0.1 && initialIndex > currentIndex)
        {
            Vector3 direction = (boxes[initialIndex - 1].transform.position - this.transform.position);

            Move(direction, boxes[initialIndex - 1]);
            if (Vector3.Distance(this.transform.position, boxes[initialIndex - 1].transform.position) < 0.1)
            {
                initialIndex--;
                if (initialIndex == 10 && rotationChanged == false)
                {
                    RotatePlayer();
                    rotationChanged = true;
                }
            }
        }
        else if (goingBackwards == true && move == true)
        {
            move = false;
            goingBackwards = false;
            //this.transform.rotation = Quaternion.Euler(0, 0, 0);
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
