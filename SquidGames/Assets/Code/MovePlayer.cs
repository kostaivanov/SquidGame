using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class MovePlayer : MonoBehaviour
{
    public delegate void TimeTicking(float nambuttonNamee);
    public static event TimeTicking OnClickTimer;

    [SerializeField] private Transform[] boxes;
    [SerializeField] private float speed = 1f;
    internal bool move;
    internal int currentIndex, initialIndex;
    [SerializeField] private LayerMask collectablesLayer;
    private int boxIndex;
    internal bool collectableFound;

    internal Vector3 startPosition;
    internal bool plusOn, minusOn;
    internal bool rotationChanged;

    //internal bool untouchable;
    internal bool trap;

    private bool goingBackwards;
    private MoveButtonsStateController moveButtonsStateController;
    private Button[] moveButtons;
    private Button[] skillsButtons;
    internal Coroutine coroutine;


    private void Start()
    {
        move = false;
        trap = false;
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

    public void MoveByTrapDirection(string direction, int numberOfMoves, GameObject obj)
    {
        if (this.gameObject.name == obj.name)
        {
            //trap = true;
            move = true;
            if (direction == "GoForward")
            {
                if (currentIndex == 19)
                {
                    currentIndex += 1;
                }
               
                else
                {
                    currentIndex += numberOfMoves;
                }
                if (coroutine != null)
                {
                    StopCoroutine(coroutine);
                }
            }
            else if(direction == "GoBackward")
            {
                if (currentIndex != 10)
                {
                    RotatePlayer();
                }
                if (currentIndex == 1 || currentIndex == 0)
                {
                    currentIndex -= 1;
                }
                else
                {
                    currentIndex -= numberOfMoves;
                }
                goingBackwards = true;
                if (coroutine != null)
                {
                    StopCoroutine(coroutine);
                }
            }
        }
    }

    internal void MovePlayerForward(int boxIndex, string buttonColor, GameObject obj, Button[] moveButtons, Button[] skillsButtons, MoveButtonsStateController moveButtonsStateController)
    {
        if (this.gameObject.name.Substring(0, 1) == buttonColor)
        {
            Button button = obj.GetComponent<Button>();
            // StartCoroutine(DeactivateButtons(button, moveButtons, skillsButtons));

            this.moveButtons = moveButtons;
            this.skillsButtons = skillsButtons;
            this.moveButtonsStateController = moveButtonsStateController;

            if (button.interactable == false && this.plusOn == true)
            {
                button.interactable = true;
                this.plusOn = false;
                this.moveButtonsStateController.usedButtons.Remove(this.moveButtonsStateController.usedButtons.SingleOrDefault(x => x.gameObject.name == obj.name));
            }
            else if (button.interactable == true && this.minusOn == true)
            {
                button.interactable = false;
                this.minusOn = false;
                this.moveButtonsStateController.usedButtons.Add(obj);

                if (moveButtonsStateController.usedButtons.Count > 2)
                {
                    foreach (Button _button in moveButtons)
                    {
                        _button.interactable = true;
                    }
                }
            }
            else if (button.interactable == true)
            {
                this.boxIndex = boxIndex;
                this.plusOn = false;
                this.minusOn = false;
                move = true;

                if (currentIndex == 19)
                {
                    currentIndex += 1;
                }
                else if (currentIndex == 18 && this.boxIndex > 2)
                {
                    currentIndex += 2;
                }
                else if (currentIndex == 18 && this.boxIndex > 3)
                {
                    currentIndex += 3;
                }
                else
                {
                    currentIndex += this.boxIndex;
                }
                //button.interactable = false;
                //moveButtons.ToList().ForEach(x => Debug.Log(x.gameObject.name));
                //moveButtons.ToList().ForEach(x => x.interactable = false);
                
                this.moveButtonsStateController.usedButtons.Add(obj);
                this.moveButtonsStateController.CheckIfAllUsed(moveButtons);

                foreach (Button _button in moveButtons)
                {
                    _button.interactable = false;
                }
            }
        }
    }

    private void Update()
    {
        //Debug.Log("current index = " + currentIndex + " - and  initial = " + initialIndex);
        Debug.Log("trap = " + trap);
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
        else if (goingBackwards == true && move == true && currentIndex >= 0 && initialIndex > 0 && Vector3.Distance(this.transform.position, boxes[initialIndex - 1].transform.position) > 0.1 && initialIndex > currentIndex)
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
        else if (move == true)
        {
            if (goingBackwards == true)
            {
                goingBackwards = false;
                RotatePlayer();
            }
            move = false;

            if (trap == false)
            {
                coroutine = StartCoroutine(ActivateButtons(this.moveButtonsStateController.usedButtons, this.moveButtons, this.skillsButtons, this.boxIndex));
                OnClickTimer(this.boxIndex);
            }
           

            Debug.Log("2 are de");
            Debug.Log("3 are de");

            //StartCoroutine(coroutine);
            trap = false;
            if (StayOnTopOfCollectable() == true && collectableFound == false)
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

    internal bool StayOnTopOfCollectable()
    {
        Collider2D rayCastHit = Physics2D.OverlapCircle(this.transform.position, 0.5f, collectablesLayer);

        if (rayCastHit != null)
        {
            return true;
        }
        return false;
    }

    private IEnumerator ActivateButtons(List<GameObject> usedButtons, Button[] moveButtons, Button[] skillsButtons, int boxIndex)
    {
        //TurnOnOffButtons(false, usedButton, moveButtons, skillsButtons);

        yield return new WaitForSecondsRealtime(boxIndex + 1);
        //if (TurnButtonsInteractable(moveButtons) == true)
        //{
        //Debug.Log("coroutine");
        foreach (Button _button in moveButtons)
        {
            if (usedButtons.Any(x => x.name == _button.gameObject.name))
            {
                continue;
            }

            _button.interactable = true;

        }
        // }
    }

    //private bool TurnButtonsInteractable(Button[] moveButtons)
    //{
    //    bool allInactive = true;
    //    foreach (Button _button in moveButtons)
    //    {
    //        if (_button.interactable == true)
    //        {
    //            allInactive = false;
    //        }
    //    }
    //    return allInactive;
    //}

    //private void TurnOnOffButtons(bool trueOrFalse, Button usedButton, Button[] moveButtons, Button[] skillsButtons)
    //{
    //    foreach (Button _button in moveButtons)
    //    {
    //        if (_button.gameObject.name != usedButton.gameObject.name)
    //        {
    //            _button.interactable = trueOrFalse;
    //        }
    //    }
    //    foreach (Button _button in skillsButtons)
    //    {
    //        if (_button.gameObject.name != usedButton.gameObject.name)
    //        {
    //            _button.interactable = trueOrFalse;
    //        }
    //    }
    //}
}
