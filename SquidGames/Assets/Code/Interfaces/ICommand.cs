using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface ICommand
{
    void Execute(MovePlayer player);
}
