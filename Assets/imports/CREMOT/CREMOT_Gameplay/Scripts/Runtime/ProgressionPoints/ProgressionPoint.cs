using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CREMOT.GameplayUtilities
{
    public abstract class ProgressionPoint : GameData
    {
        [ReadOnly]
        [SerializeField] private string id;


        public string ID => id;
        

        public event Action onCompleted;


        public override UnityEngine.Object Me()
        {
            return this;
        }


        [Button]
        private void RefreshGUID()
        {
            if (!string.IsNullOrEmpty(id))
            {
                return;
            }

            ForceRefreshGUID();
        }

        [Button]
        private void ForceRefreshGUID()
        {
            id = System.Guid.NewGuid().ToString();
        }

        public virtual bool IsDone()
        {
            return ProgressionPointManager.CheckIsDone(this);
        }

        public virtual void SetIsDone(bool isDone)
        {
            ProgressionPointManager.SetIsDone(id, isDone);
            onCompleted?.Invoke();
        }
    }
}
