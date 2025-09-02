using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CREMOT.GameplayUtilities
{
    public static class ProgressionPointManager
    {
        #region Field
        private static Dictionary<string, bool> progressionPointsState = new();

        #endregion

        #region Properties
        public static Dictionary<string, bool> ProgressionPointsState => progressionPointsState;

        #endregion


        public static bool CheckIsDone(ProgressionPoint progressionPoint)
        {
            if (progressionPoint == null || !progressionPointsState.ContainsKey(progressionPoint.ID))
            {
                return false;
            }

            return progressionPointsState[progressionPoint.ID];
        }

        public static void SetIsDone(string progressionPointId, bool done)
        {
            progressionPointsState[progressionPointId] = done;
        }

    }
}
