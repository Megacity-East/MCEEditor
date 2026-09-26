using System.Collections.Generic;
using MCELoader.Shared.ProxyTypes;

namespace MCEEditor.Builder
{
    public class BuildSession
    {
        public Map Map;
        public MapConfig MapConfig;

        public List<MapStaticCollider3D> mapStaticCollider3Ds = new();
        public List<SpawnPointWithOrientation> spawnPoints = new();

    }
}
