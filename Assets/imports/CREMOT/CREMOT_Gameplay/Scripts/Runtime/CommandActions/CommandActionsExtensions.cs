using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CREMOT.GameplayUtilities
{
    public static class CommandActionsExtensions
    {
        public static void Execute(this IEnumerable<CommandAction> actions, MonoBehaviour caller)
        {
            foreach (var action in actions)
            {
                if (action == null)
                    continue;

                action.Execute(caller);
            }
        }
    }
}
