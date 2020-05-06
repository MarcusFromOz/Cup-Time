using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        IAction currentAction;

        public void StartAction(IAction action)
        {
            //if we are going to continue doing the same action then we can just leave this function 
            if (currentAction == action) return;
            
            if (currentAction != null)
            {
                currentAction.Cancel();
            }
            currentAction = action;

        }
    }
}
