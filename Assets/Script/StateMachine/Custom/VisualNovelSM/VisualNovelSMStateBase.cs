using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine.VisualNovelSM
{

    public class VisualNovelSMStateBase : StateMachineStateBase
    {

        protected VisualNovelSMContext context;

        public override void Setup(ISMContext _context)
        {
            context = _context as VisualNovelSMContext;
        }
    }

}

