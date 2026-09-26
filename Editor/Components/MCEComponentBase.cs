using UnityEngine;

namespace MCEEditor.Components
{
    public abstract class MCEComponentBase : MonoBehaviour
    {
#if UNITY_EDITOR
        public abstract void ProcessBuildStage(ref MCEEditor.Builder.BuildSession session);
#endif
    }
}
