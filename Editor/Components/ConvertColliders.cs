using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#if UNITY_EDITOR
using MCEEditor.Builder;
#endif

using MCELoader.Shared.ProxyTypes;

namespace MCEEditor.Components
{
    public class ConvertColliders : MCEComponentBase
    {
        public Collider[] Colliders => this.GetComponentsInChildren<Collider>();

#if UNITY_EDITOR


        public override void ProcessBuildStage(ref BuildSession session)
        {
            foreach (Collider collider in Colliders)
            {
                if (collider.TryGetComponent<QuantumCollider>(out _))
                {
                    continue;
                }
                collider.gameObject.AddComponent<QuantumCollider>().ProcessBuildStage(ref session);
            }
        }

#endif
    }
}
