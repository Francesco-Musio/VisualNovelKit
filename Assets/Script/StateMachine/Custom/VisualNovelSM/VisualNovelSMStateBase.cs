using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine.VisualNovelSM
{

    public class VisualNovelSMStateBase : StateMachineStateBase
    {

        protected SceneContextManager context;

        public override void Setup(ISMContext _context)
        {
            this.context = _context as SceneContextManager;
        }
    }

}

