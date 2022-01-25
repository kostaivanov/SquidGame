using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IPlayerDesribable
{
    string Name { get; set; }
    float InitialIndexPosition { get; set; }
    int CurrentIndexPosition { get; set; }
}
