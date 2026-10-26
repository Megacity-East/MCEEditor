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
    public class MCEStartLineGatePrototype : MCEPrototype
    {

        public int arena;
        public bool goalUp;
        public bool awake;
        public float up;
        public Vector3 baseRotation;
        public Vector2 timeStamps;


#if UNITY_EDITOR

        public override ComponentPrototype ToProxyType()
        {

            StartLineGatePrototype prototype = new()
            {
                arena = this.arena,
                goalUp = this.goalUp,
                awake = this.awake,
                up = this.up,
                baseRotation = this.baseRotation,
                timeStamps = this.timeStamps,
            };

            return prototype;
        }
#endif

    }
}
