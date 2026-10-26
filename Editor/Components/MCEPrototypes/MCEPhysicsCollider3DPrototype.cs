using UnityEngine;
using MCELoader.Shared.ProxyTypes.ComponentPrototypes;
using MCELoader.Shared.ProxyTypes;



#if UNITY_EDITOR
using MCEEditor.Builder;
#endif

namespace MCEEditor.Components.MCEPrototypes
{

    [System.Serializable]
    public class MCEPhysicsCollider3DPrototype : MCEPrototype
    {

        public bool IsTrigger;
        public int Layer;

        public AssetRef PhysicsMaterial;
        public Shape3DConfig ShapeConfig = new();



#if UNITY_EDITOR
        public override void ProcessBuildStage(MCEPrototypeSet owningSet, ref BuildSession session)
        {
            if (owningSet.TryGetComponent<BoxCollider>(out var box))
            {
                this.ShapeConfig.ShapeType = Shape3DType.Box;
                this.ShapeConfig.BoxExtents = Vector3.Scale(box.size, owningSet.transform.lossyScale) / 2; // we scale so the gameobject being scaled doesnt cause weridness

            }
        }
        public override ComponentPrototype ToProxyType()
        {

            PhysicsCollider3DPrototype prototype = new()
            {
                IsTrigger = this.IsTrigger,
                Layer = this.Layer,
                PhysicsMaterial = this.PhysicsMaterial,
                ShapeConfig = this.ShapeConfig,
            };

            return prototype;
        }
#endif

    }
}
