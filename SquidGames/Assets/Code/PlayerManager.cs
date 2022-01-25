using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IPlayerDesribable
{
    private string name;
    public string Name { get =>name; set => name = this.gameObject.name; }

    private int initialIndexPosition;
    public float InitialIndexPosition { get => initialIndexPosition; set => initialIndexPosition = -1; }

    private int currentIndexPosition;
    public int CurrentIndexPosition { get => currentIndexPosition; set => currentIndexPosition = - 1; }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
