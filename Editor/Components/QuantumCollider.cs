using UnityEngine;

#if UNITY_EDITOR
using MCEEditor.Builder;
#endif

using MCELoader.Shared.ProxyTypes;
using System.Linq;

namespace MCEEditor.Components
{
    [ExecuteInEditMode]
    public class QuantumCollider : MCEComponentBase
    {
        private Collider[] colliders;

        public bool HasAnyColliders => colliders.Length > 0;
        public bool HasMultipleColliders => colliders.Length > 1;

        public Collider UnityCollider;

        public void UpdateFields()
        {
            colliders = GetComponents<Collider>();
            if (HasAnyColliders)
                UnityCollider = colliders.FirstOrDefault();

        }

        public void Start()
        {
            UpdateFields();
        }

#if UNITY_EDITOR

        public MapStaticCollider3D? GenerateStaticCollider()
        {
            MapStaticCollider3D collider3D = new()
            {
                Position = this.transform.position,
                Rotation = this.transform.rotation,
                PhysicsMaterial = new(Constants.PhysicsMaterial_Terrian)
            };

            collider3D.StaticData.Tag = this.tag;
            collider3D.StaticData.Name = this.name;
            collider3D.StaticData.Layer = this.gameObject.layer;

            collider3D.StaticData.IsTrigger = UnityCollider.isTrigger;
            collider3D.StaticData.MutableMode = StaticColliderData.StaticColliderMutableMode.Immutable;


            if (UnityCollider is BoxCollider box)
            {
                collider3D.ShapeType = Shape3DType.Box;
                collider3D.BoxExtents = Vector3.Scale(box.size, this.transform.lossyScale) / 2; // we scale so the gameobject being scaled doesnt cause weridness
                return collider3D;

            }

            if (UnityCollider is CapsuleCollider capsule)
            {
                collider3D.ShapeType = Shape3DType.Capsule;
                collider3D.CapsuleHeight = capsule.height;
                collider3D.CapsuleRadius = capsule.radius;
                return collider3D;
            }

            if (UnityCollider is SphereCollider sphere)
            {
                collider3D.ShapeType = Shape3DType.Sphere;
                collider3D.SphereRadius = sphere.radius;
                return collider3D;
            }

            if (UnityCollider is MeshCollider meshCollider)
            {
                collider3D.ShapeType = Shape3DType.Mesh;
                return collider3D;
            }

            return null;
        }

        public override void ProcessBuildStage(ref BuildSession session)
        {
            UpdateFields();

            if (HasAnyColliders == false)
            {
                Debug.LogWarning($"[QuantumCollider] '{UnityEditor.Search.SearchUtils.GetTransformPath(this.transform)}' doesnt have any attached colliders.");
                return;
            }

            if (HasMultipleColliders)
            {
                Debug.LogWarning($"[QuantumCollider] '{UnityEditor.Search.SearchUtils.GetTransformPath(this.transform)}' has more than one collider.");
            }

            int colliderIndex = session.mapStaticCollider3Ds.Count;
            if (GenerateStaticCollider() is MapStaticCollider3D collider3D)
            {
                collider3D.StaticData.ColliderIndex = colliderIndex;

                if(collider3D.ShapeType == Shape3DType.Mesh)
                {
                    Mesh colliderMesh = ((MeshCollider)UnityCollider).sharedMesh;
                    session.staticMeshColliders.Add(colliderIndex,colliderMesh);
                }

                session.mapStaticCollider3Ds.Add(collider3D);
            }
            ;
        }
#endif
    }
}
