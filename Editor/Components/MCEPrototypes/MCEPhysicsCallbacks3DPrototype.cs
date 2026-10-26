using MCELoader.Shared.ProxyTypes.ComponentPrototypes;
using MCELoader.Shared.ProxyTypes;


#if UNITY_EDITOR
using MCEEditor.Builder;
#endif

namespace MCEEditor.Components.MCEPrototypes
{

    [System.Serializable]
    public class MCEPhysicsCallbacks3DPrototype : MCEPrototype
    {

        public CallbackFlags CallbackFlags;

#if UNITY_EDITOR

        public override ComponentPrototype ToProxyType()
        {

            PhysicsCallbacks3DPrototype prototype = new()
            {
                CallbackFlags = this.CallbackFlags
            };

            return prototype;
        }
#endif

    }
}
