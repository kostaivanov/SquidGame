using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] private Transform[] boxes;
    public float speed = 1f;
    public bool move;
    private string _boxIndex;
    private string _buttonColor;
    private void Start()
    {
        move = false;
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
        move = true;
        this._boxIndex = boxIndex;
        this._buttonColor = buttonColor;
        Debug.Log(_boxIndex);

        Debug.Log(_buttonColor);

    }

    private void Update()
    {
        if (_buttonColor != null && this.gameObject.name.StartsWith(_buttonColor))
        {
            Debug.Log(boxes[int.Parse(_boxIndex)]);
            if (move == true && Vector3.Distance(this.transform.position, boxes[int.Parse(_boxIndex)].transform.position) > 0.4)
            {
                this.transform.Translate(speed * Time.deltaTime, 0, 0);
            }
        }
    }
}
