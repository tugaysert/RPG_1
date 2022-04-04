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
            //combat ba?lad???nda mover'? iptal edece?iz
            //mover ba?lad???nda combat'? iptal edece?iz 
            //ve bu ikisinin bir cycle olu?turmas?n? istemiyoruz.

            //Method her ça??r?ld???nda action'a bir de?er atan?yor,
            //atanan de?er e?er önceki set edilmi? de?erden farkl? ve null de?ilse,
            //di?er eylemi cancel ediyor, field'? gelen actiona set ediyoruz.
           
            if (currentAction == action) return;
            if (currentAction != null)             
            {
                currentAction.Cancel();
            }
             currentAction = action;
        }
    }
}

