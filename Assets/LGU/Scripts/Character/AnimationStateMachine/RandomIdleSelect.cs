using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomIdleSelect : StateMachineBehaviour
{
    int waitTimes = 0;

    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    //Debug.Log("StateEnter - �ִϸ��̼��� ����� ������ ����");
    //    //Test();
    //}

    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    Debug.Log("StateUpdate - �ִϸ��̼��� ����ǰ� ���� �� ����");
    //}

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log("StateExit - �ִϸ��̼��� ���� �� ����");
        waitTimes--;
        if (waitTimes < 0)
        {
            animator.SetInteger("IdleSelect", RandomSelect());
            //waitTimes = Random.Range(1, 4);
            waitTimes = 0;
        }
        else
        {
            animator.SetInteger("IdleSelect", 0);
        }
    }

    int RandomSelect()
    {
        float number = Random.Range(0.0f, 1.0f);
        int select = 0;
        if (number < 0.5f)
        {
            select = 1;
        }
        else if (number < 0.8f)
        {
            select = 2;
        }
        else if (number < 0.95f)
        {
            select = 3;
        }
        else
        {
            select = 4;
        }
        return select;
    }
}
