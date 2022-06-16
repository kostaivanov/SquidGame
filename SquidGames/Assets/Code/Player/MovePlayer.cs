using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

internal class MovePlayer : PlayerComponents
{
    public delegate void TimeTicking(float nambuttonNamee);
    public static event TimeTicking OnClickTimer;

    private List<Transform> boxes;
    private int lastIndex;
    internal List<GameObject> players;

    private PushPlayer pusPlayerRef;
    private SwapPlayer swapPlayer;
    private BombPlayer bombPlayer;

    internal List<MovePlayer> playersMove;

    [SerializeField] private float speed = 1f;
    internal bool move;
    internal int currentIndex, initialIndex, balanceIndex;
    [SerializeField] private LayerMask collectablesLayer, trapsLayer;
    private int boxIndex;
    internal bool collectableFound;
    internal bool holdsCollectable;

    internal Vector3 startPosition;
    internal bool plusOn, minusOn;
    internal bool rotationChanged;

    //internal bool untouchable;
    internal bool trap;
    //internal bool push;
    private bool goingBackwards;
    private MoveButtonsStateController moveButtonsStateController;
    private Button[] moveButtons;
    private Button[] skillsButtons;
    internal Coroutine coroutine;

    private int indexToPush;

    private void Start()
    {
        //boxes = new List<Transform>();
        //foreach  (GameObject box in InstantiateItems.moveBoxes)
        //{
        //    boxes.Add(box.transform);
        //}
        boxes = InstantiateItems.moveBoxes.Select(obj => obj.transform).ToList();
        //if (boxes != null)
        //{
        //    Debug.Log(boxes.Count);

        //}
        players = new List<GameObject>();
        players = GameObject.FindGameObjectsWithTag("Player").ToList();
        playersMove = new List<MovePlayer>();
        if (players != null )
        {
            foreach (GameObject p in players)
            {
                //MovePlayer moveP = p.GetComponent<MovePlayer>();
                playersMove.Add(p.GetComponent<MovePlayer>());
            }
        }
        
        move = false;
        trap = false;

        currentIndex = 0;
        initialIndex = 0;
        balanceIndex = 0;
        indexToPush = 0;
        initialIndex = currentIndex;
        lastIndex = boxes.Count - 1;
        collectableFound = false;
        holdsCollectable = false;

        startPosition = this.transform.position;
        rotationChanged = false;
        pusPlayerRef = GetComponent<PushPlayer>();
        swapPlayer = GetComponent<SwapPlayer>();
        bombPlayer = GetComponent<BombPlayer>();
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
            if (initialIndex != 11)
            {
                rotationChanged = false;
            }
            move = true;
            if (direction == "GoForward")
            {
                if (currentIndex == 21)
                {
                    currentIndex = 1;
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
                RotatePlayer();
                //if (currentIndex != 10)
                //{
                //    RotatePlayer();
                //}
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
        //Debug.Log(" testing if not moving - " + CheckIfAnySkillActivated(skillsButtons));

        if (this.gameObject.name.Substring(0, 1) == buttonColor && CheckIfAnySkillActivated(skillsButtons) == false)
        {
            Button button = obj.GetComponent<Button>();
            // StartCoroutine(DeactivateButtons(button, moveButtons, skillsButtons));
            this.moveButtons = moveButtons;
            this.skillsButtons = skillsButtons;
            this.moveButtonsStateController = moveButtonsStateController;
            if (initialIndex != 11 && initialIndex != 21)
            {
                rotationChanged = false;
            }
            if (pusPlayerRef.toPushEnemy == true)
            {
                indexToPush = currentIndex + boxIndex;
                foreach (MovePlayer p in playersMove)
                {
                    if (p.initialIndex == indexToPush)
                    {
                        p.move = true;
                        p.currentIndex += 1;
                    }
                }
                //move = true;
                pusPlayerRef.toPushEnemy = false;
            }
            else if (button.interactable == false && this.plusOn == true)
            {
                button.interactable = true;
                this.plusOn = false;

                //Removingbutton when used/clicked to the used buttons list.
                this.moveButtonsStateController.usedButtons.Remove(this.moveButtonsStateController.usedButtons.SingleOrDefault(x => x.gameObject.name == obj.name));
                Debug.Log("Used button was removed from List = +");
            }
            else if (button.interactable == true && this.minusOn == true)
            {
                button.interactable = false;
                this.minusOn = false;

                //Adding button when used/clicked to the used buttons list.
                this.moveButtonsStateController.usedButtons.Add(obj);
                Debug.Log("Used button was added from List = - by = " + this.gameObject.name);

                //Turn all buttons ON if all are not interactable and used.
                if (moveButtonsStateController.usedButtons.Count > 3)
                {
                    foreach (Button _button in moveButtons)
                    {
                        _button.interactable = true;
                    }
                    this.moveButtonsStateController.usedButtons.Clear();
                }
            }
            else if (button.interactable == true && swapPlayer.toSwitchEnemy == false && bombPlayer.toBombEnemy == false)
            {
                //Debug.Log("not moving");
                this.boxIndex = boxIndex;
                this.plusOn = false;
                this.minusOn = false;
                move = true;


                int sum = currentIndex + this.boxIndex;
                if (currentIndex == 21)
                {
                    currentIndex = this.boxIndex;
                    initialIndex = 0;
                    Debug.Log("one time click current index = " + currentIndex + " - and  initial = " + initialIndex);
                }
                else if (sum > 20)
                {
                    balanceIndex = this.boxIndex - (lastIndex - currentIndex);
                    //currentIndex = (sum % 10) - 1;
                    currentIndex = 21;
                }    
                else
                {
                    currentIndex += this.boxIndex;
                }

                //button.interactable = false;
                //moveButtons.ToList().ForEach(x => Debug.Log(x.gameObject.name));
                //moveButtons.ToList().ForEach(x => x.interactable = false);

                //Adding button when used/clicked to the used buttons list.
                this.moveButtonsStateController.usedButtons.Add(obj);
                this.moveButtonsStateController.CheckIfAllUsed(this.moveButtonsStateController.usedButtons);

                foreach (Button _button in moveButtons)
                {
                    _button.interactable = false;
                }            
            }
        }
    }

    private void Update()
    {
        if (initialIndex == 11 && rotationChanged == false)
        {
            RotatePlayer();
            rotationChanged = true;
        }
        if (initialIndex == 21 && rotationChanged == false)
        {
            RotatePlayer();
            rotationChanged = true;
            Debug.Log("Rotated!");
        }
        //Debug.Log(this.gameObject.name + " = plus = " + plusOn + "; = minus = " + minusOn); ;
        Debug.Log(this.gameObject.name + " = move  = " + move);
        //currentIndex < boxes.Count && initialIndex < boxes.Count - 1 &&
        if (move == true && initialIndex < currentIndex && Vector3.Distance(this.transform.position, boxes[initialIndex + 1].transform.position) > 0.1 )
        {           
            Vector3 direction = (boxes[initialIndex + 1].transform.position - this.transform.position);

            Move(direction, boxes[initialIndex + 1]);

            if (Vector3.Distance(this.transform.position, boxes[initialIndex + 1].transform.position) < 0.1)
            {
                initialIndex++;
                if (initialIndex == 21 && balanceIndex > 0)
                {
                    if (rotationChanged == false)
                    {
                        RotatePlayer();
                        rotationChanged = true;
                    }
                    initialIndex = 0;
                    currentIndex = balanceIndex;
                    balanceIndex = 0;
                    Debug.Log("Rotated!");
                }


                //if (initialIndex == 11 && rotationChanged == false)
                //{
                //    RotatePlayer();
                //    rotationChanged = true;
                //}
            }
        }
        else if (goingBackwards == true && move == true && currentIndex >= 0 && initialIndex > 0 && Vector3.Distance(this.transform.position, boxes[initialIndex - 1].transform.position) > 0.1 && initialIndex > currentIndex)
        {
            Vector3 direction = (boxes[initialIndex - 1].transform.position - this.transform.position);

            Move(direction, boxes[initialIndex - 1]);
            if (Vector3.Distance(this.transform.position, boxes[initialIndex - 1].transform.position) < 0.1)
            {
                initialIndex--;
                //if (initialIndex == 11 && rotationChanged == false)
                //{
                //    RotatePlayer();
                //    rotationChanged = true;
                //}
                //else if(initialIndex == 0 && rotationChanged == false)
                //{
                //    Debug.Log(this.gameObject.name + " = rotated" );
                //    RotatePlayer();
                //    rotationChanged = true;
                //}
            }
        }
        else if (move == true && balanceIndex == 0)
        {
            if (goingBackwards == true)
            {
                goingBackwards = false;
                RotatePlayer();
            }
            move = false;

            if (StayOnTopOfCollectable(trapsLayer) == true && trap == false)
            {
                trap = true;
            }

            //StartCoroutine(coroutine);

            if (StayOnTopOfCollectable(collectablesLayer) == true && collectableFound == false)
            {
                collectableFound = true;
                Debug.Log("Coroutine for counting has started!");
                coroutine = StartCoroutine(ActivateButtons(this.moveButtonsStateController.usedButtons, this.moveButtons, this.skillsButtons, this.boxIndex));
                OnClickTimer(this.boxIndex);
            }
            else if(StayOnTopOfCollectable(collectablesLayer) == false && StayOnTopOfCollectable(trapsLayer) == false)
            {
                coroutine = StartCoroutine(ActivateButtons(this.moveButtonsStateController.usedButtons, this.moveButtons, this.skillsButtons, this.boxIndex));
                OnClickTimer(this.boxIndex);
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

    internal bool StayOnTopOfCollectable(LayerMask layer)
    {
        Collider2D rayCastHit = Physics2D.OverlapCircle(this.transform.position, 0.5f, layer);

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

        foreach (Button _button in moveButtons)
        {
            if (usedButtons.Any(x => x.name == _button.gameObject.name))
            {
                continue;
            }

            _button.interactable = true;
        }
    }

    private bool CheckIfAnySkillActivated(Button[] buttons)
    {
        string[] separatingStrings = { "_" };
        foreach (Button b in buttons)
        {
            string[] words = b.gameObject.name.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);

            if (words[1].StartsWith("B"))
            {
                if (b.GetComponent<OnClickBomb>().activated == true)
                {
                    return true;
                }
               
            }
            else if (words[1].StartsWith("S"))
            {
                if (b.GetComponent<OnClickSwitch>().activated == true)
                {
                    return true;
                }
            }
        }
        return false;
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
}
