using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerAnimationsController : PlayerComponents
{
    private MovePlayer movePlayerScript;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        movePlayerScript = GetComponent<MovePlayer>();
    }


    private void LateUpdate()
    {
        this.AnimationStateSwitch();
        base.animator.SetInteger("state", (int)state);

    }

    protected void AnimationStateSwitch()
    {
        //&& Mathf.Abs(rigidBody.velocity.x) > minimumVelocity_X
        if (movePlayerScript.move == true)
        {
            state = PlayerState.moving;
        }
        else
        {
            state = PlayerState.idle;
        }
    }
}

