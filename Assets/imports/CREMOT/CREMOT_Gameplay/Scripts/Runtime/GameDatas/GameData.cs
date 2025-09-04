using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CREMOT.GameplayUtilities
{
    public abstract class GameData : ScriptableObject, IGameData
    {
        public abstract Object Me();
    }

    public interface IGameData
    {
        public abstract Object Me();
    }
}
