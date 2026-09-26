using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using MCEEditor.Builder;
using MCELoader.Shared;
#endif

namespace MCEEditor.Components
{

    public class SpawnPointWithOrientation : MCEComponentBase
    {

        [Range(0, 5)]
        public float Radius;

#if UNITY_EDITOR
        public override void ProcessBuildStage(ref BuildSession session)
        {
            MCELoader.Shared.ProxyTypes.SpawnPointWithOrientation point = new()
            {
                position = this.transform.position,
                degreesOnY = Radius,
            };

            session.spawnPoints.Add(point);
        }
#endif
    }
}
