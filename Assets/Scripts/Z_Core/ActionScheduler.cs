using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        //** Variables 
        IAction currentAction;


        //** Start and Update


        //** Public Methods
        
        public void CancelCurrentAction()
        {
            StartAction(null);
        }

        public void StartAction(IAction action)
        {
            //if we are going to continue doing the same action then we can just exit 
            if (currentAction == action) return;
            
            //Stop the current action and begin the new one
            if (currentAction != null)
            {
                currentAction.Cancel();
            }
            currentAction = action;
        }
        
        //** Private Methods


    }
}
