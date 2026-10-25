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
    public abstract class MCEPrototype
    {

#if UNITY_EDITOR
        public abstract ComponentPrototype ToProxyType();
#endif
    }
}
