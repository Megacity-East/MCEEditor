using System.Collections.Generic;
using MCELoader.Shared.ProxyTypes;
using UnityEngine;

namespace MCEEditor.Builder
{
    public class BuildSession
    {
        public Map Map;
        public MapConfig MapConfig;

        public List<MapStaticCollider3D> mapStaticCollider3Ds = new();
        public List<SpawnPointWithOrientation> spawnPoints = new();
        public Dictionary<int,Mesh> staticMeshColliders = new();

    }
}
