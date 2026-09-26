using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MCEEditor.Components
{
    /// <summary>
    /// World Root
    /// </summary>
    public class WorldRoot : MCEComponentBase
    {
        [HideInInspector]
        public string Guid = System.Guid.NewGuid().ToString();

        public string Name;

        public string DisplayName;

        public Transform DevSpawn;

        public string Version = "0.0.1";

        public string[] Authors;

#if UNITY_EDITOR
        public override void ProcessBuildStage(ref MCEEditor.Builder.BuildSession session)
        {
            Scene activeScene = SceneManager.GetActiveScene();
            session.Map.SceneGuid = AssetDatabase.GUIDFromAssetPath(activeScene.path).ToString();
            session.Map.Scene = activeScene.name;
            session.Map.ScenePath = activeScene.path;
            session.Map.WorldSize = 100;

            Vector3 devSpawnPos = DevSpawn.position;
            if (DevSpawn is null)
                devSpawnPos = Vector3.zero;

            session.MapConfig.devSpawn = new()
            {
                position = devSpawnPos
            };

        }
#endif
    }
}
