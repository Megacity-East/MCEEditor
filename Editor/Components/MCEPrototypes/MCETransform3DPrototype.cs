using UnityEngine;
using System;



#if UNITY_EDITOR
using MCELoader.Shared.ProxyTypes.ComponentPrototypes;
using MCELoader.Shared.ProxyTypes;
using MCEEditor.Builder;
#endif

namespace MCEEditor.Components.MCEPrototypes
{

    [System.Serializable]
    public class MCETransform3DPrototype : MCEPrototype
    {

        [HideInInspector]
        public Vector3 Position;
        [HideInInspector]
        public Quaternion Rotation;

#if UNITY_EDITOR
        public override void ProcessBuildStage(MCEPrototypeSet owningSet, ref BuildSession session)
        {
            Position = owningSet.transform.position;
            Rotation = owningSet.transform.rotation;
        }

        public override ComponentPrototype ToProxyType()
        {

            Transform3DPrototype prototype = new()
            {
                Position = Position,
                Rotation = Rotation.eulerAngles
            };

            return prototype;
        }
#endif

    }
}
