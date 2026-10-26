using UnityEngine;
using MCELoader.Shared.ProxyTypes.ComponentPrototypes;
using MCELoader.Shared.ProxyTypes;
using System;



#if UNITY_EDITOR
using MCEEditor.Builder;
#endif

namespace MCEEditor.Components.MCEPrototypes
{

    [System.Serializable]
    public class MCEPhysicsBody3DPrototype : MCEPrototype
    {


        public PhysicsBody3D.ConfigFlags Config;
        public float Mass;
        public float Drag;
        public float AngularDrag;
        public Vector3 CenterOfMass;
        public RotationFreezeFlags RotationFreeze;
        public float? GravityScale;
        public float? InertiaScale;
        public PhysicsBodyInertiaMode InertiaMode;
        public Vector3 ExplicitInertiaTensor;
        public Shape3DConfig ParametricInertiaShape = new();

#if UNITY_EDITOR
        public override void ProcessBuildStage(MCEPrototypeSet owningSet, ref BuildSession session)
        {

        }

        public override ComponentPrototype ToProxyType()
        {

            PhysicsBody3DPrototype prototype = new()
            {

                Config = this.Config,
                Mass = this.Mass,
                Drag = this.Drag,
                AngularDrag = this.AngularDrag,
                CenterOfMass = this.CenterOfMass,
                RotationFreeze = this.RotationFreeze,
                GravityScale = this.GravityScale,
                InertiaScale = this.InertiaScale,
                InertiaMode = this.InertiaMode,
                ExplicitInertiaTensor = this.ExplicitInertiaTensor,
                ParametricInertiaShape = this.ParametricInertiaShape,
            };

            return prototype;
        }
#endif

    }
}
