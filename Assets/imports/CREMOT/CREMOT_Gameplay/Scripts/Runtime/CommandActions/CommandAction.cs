using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CREMOT.GameplayUtilities
{
    public abstract class CommandAction : GameData
    {
        public override Object Me()
        {
            return this;
        }

        public abstract void Execute(MonoBehaviour caller);

        public virtual void Execute()
        {
            Execute(null);
        }
    }
}
