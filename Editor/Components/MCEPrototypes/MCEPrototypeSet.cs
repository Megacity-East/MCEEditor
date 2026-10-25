using System.Collections.Generic;
using MCEEditor.Components;
using System.Collections;
using UnityEngine;
using MCEEditor.Builder;
#if UNITY_EDITOR
using MCELoader.Shared.ProxyTypes;
#endif
namespace MCEEditor.Components.MCEPrototypes
{
    public sealed class MCEPrototypeSet : MCEComponentBase
    {
        // Start is called before the first frame update
        public List<MCEPrototype> prototypes;
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
            ComponentPrototypeSet prototypeSet = new();

            foreach(MCEPrototype mcePrototype in prototypes)
            {
                session.mapEntities.Add()
            }
        }
#endif
    }
}
