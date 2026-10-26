using MCEEditor.Components.MCEPrototypes;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using MCELoader.Shared.ProxyTypes.ComponentPrototypes;


#if UNITY_EDITOR
using MCELoader.Shared.ProxyTypes;
using MCEEditor.Builder;
#endif
namespace MCEEditor.Components
{
    public sealed class MCEPrototypeSet : MCEComponentBase
    {
        // Start is called before the first frame update

        public bool AdvancedMode = false;
        [SerializeReference]
        public List<MCEPrototype> prototypes = new() { new MCETransform3DPrototype() };
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }

#if UNITY_EDITOR
        public override void ProcessBuildStage(ref BuildSession session)
        {
            foreach (MCEPrototype prototype in prototypes)
                prototype.ProcessBuildStage(this, ref session);

            ComponentPrototypeSet prototypeSet = new()
            {
                Components = prototypes.Select(proto => proto.ToProxyType()).ToArray()
            };

            session.mapEntities.Add(prototypeSet);
        }
#endif
    }
}
