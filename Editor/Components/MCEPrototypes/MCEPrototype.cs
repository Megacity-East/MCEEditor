#if UNITY_EDITOR
using MCELoader.Shared.ProxyTypes;
using MCEEditor.Builder;
using UnityEditor.ShaderKeywordFilter;
#endif

namespace MCEEditor.Components.MCEPrototypes
{

    [System.Serializable]
    public abstract class MCEPrototype
    {
        [UnityEngine.HideInInspector]
        public string Name = "Unknown";
        public string GetName() => this.GetType().Name;

#if UNITY_EDITOR
        public abstract ComponentPrototype ToProxyType();

        public virtual void OnComponentAdded(MCEPrototypeSet owningSet, int componentIndex)
        {
            Name = GetName();
        }

        public virtual void ProcessBuildStage(MCEPrototypeSet owningSet, ref BuildSession session) { }
#endif
    }
}
