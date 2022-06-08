using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

internal class MovePlayerCommand : ICommand
{
    int boxIndex;
    string buttonColor;
    GameObject gameObject;
    [SerializeField] private Button[] moveButtons;
    [SerializeField] private Button[] skillsButtons;

    public void Execute(MovePlayer player)
    {
        //player.MovePlayerForward(boxIndex, buttonColor, gameObject, moveButtons, skillsButtons);
    }

}
