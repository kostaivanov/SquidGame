using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandInvoker : MonoBehaviour
{
    static Queue<ICommand> commandBuffer;

    void Awake()
    {
        commandBuffer = new Queue<ICommand>();
    }

    public static void AddCommand(ICommand command)
    {
        commandBuffer.Enqueue(command);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
