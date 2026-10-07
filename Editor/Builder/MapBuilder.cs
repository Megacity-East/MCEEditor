#if UNITY_EDITOR
using UnityEngine;
using MCEEditor.Components;
using Newtonsoft.Json;
using UnityEditor;
using System.IO;
using UnityEngine.SceneManagement;
using System.Linq;
using System.IO.Compression;
using MCELoader;
using MCELoader.Shared;
using MCELoader.Shared.ProxyTypes;
using System.Collections.Generic;

namespace MCEEditor.Builder
{
    public static class MapBuilder
    {

        public static void BuildMap(WorldRoot mapRoot)
        {
            BuildSession session = new()
            {
                Map = new(),
                MapConfig = new()
            };

            // this is a stupid fuckin fix
            session.MapConfig.pathDataSerializable.allPoints = new Vector3[0];
            session.MapConfig.pathDataSerializable.allCumulativeDistances = new float[0];
            session.MapConfig.pathDataSerializable.allRadii = new float[0];
            session.MapConfig.pathDataSerializable.allTangents = new Vector3[0];
            session.MapConfig.pathDataSerializable.allNormals = new Vector3[0];

            foreach (var comp in mapRoot.GetComponentsInChildren<MCEComponentBase>())
            {
                comp.ProcessBuildStage(ref session);
            }

            session.Map.StaticColliders3D = session.mapStaticCollider3Ds.ToArray();
            session.MapConfig.spawnPoints = session.spawnPoints.ToArray();

            MCEManifest manifest = new()
            {
                Name = mapRoot.Name,
                DisplayName = mapRoot.DisplayName,
                Authors = mapRoot.Authors,
                Version = mapRoot.Version,

                MapGuid = mapRoot.Guid,
                EditorVersion = Constants.EditorVersion,
                TargetLoaderVersion = Constants.TargetLoaderVersion,

            };


            string mapJson = JsonConvert.SerializeObject(value: session.Map, settings: JsonUtils.SerializerSettings);
            string mapConfigJson = JsonConvert.SerializeObject(value: session.MapConfig, settings: JsonUtils.SerializerSettings);
            string manifestJson = JsonConvert.SerializeObject(value: manifest, settings: JsonUtils.SerializerSettings);


            string staticMeshOutputPath = "Assets/MCEEditor_TEMP";
            DirectoryInfo staticMeshOutputDirectory = Directory.CreateDirectory(staticMeshOutputPath);

            List<string> staticMeshPaths = new(); // im great at naming things
            foreach ((int colliderId, Mesh sourceMesh) in session.staticMeshColliders)
            {
                Mesh clonedMesh = new()
                {
                    name = colliderId.ToString(),
                    vertices = sourceMesh.vertices,
                    triangles = sourceMesh.triangles
                };
                string clonedMeshPath = Path.Combine(staticMeshOutputPath, $"{colliderId}.asset");
                AssetDatabase.CreateAsset(clonedMesh, clonedMeshPath);
                staticMeshPaths.Add(clonedMeshPath);
            }

            AssetDatabase.SaveAssets();
            foreach (string staticMeshPath in staticMeshPaths)
                if (AssetImporter.GetAtPath(staticMeshPath) is ModelImporter modelImporter)
                    modelImporter.isReadable = true;

            AssetBundleBuild sceneBundle = new()
            {
                assetBundleName = "scene",
                assetNames = new[] { SceneManager.GetActiveScene().path }
            };

            AssetBundleBuild assetsBundle = new()
            {
                assetBundleName = "assets",
                assetNames = staticMeshPaths.ToArray(),
            };

            AssetBundleBuild[] bundles = new[] { sceneBundle, assetsBundle };

            string bundleOutputPath = FileUtil.GetUniqueTempPathInProject();
            DirectoryInfo bundleDirectory = Directory.CreateDirectory(bundleOutputPath);

            string zipArchivePath = FileUtil.GetUniqueTempPathInProject();
            DirectoryInfo zipArchiveDirectory = Directory.CreateDirectory(zipArchivePath);

            BuildAssetBundleOptions options = BuildAssetBundleOptions.ChunkBasedCompression;
            BuildPipeline.BuildAssetBundles(outputPath: bundleOutputPath, builds: bundles, options, BuildTarget.StandaloneWindows);


            FileInfo sceneBundleFile = bundleDirectory.GetFiles().First(file => file.Name is "scene");
            FileInfo assetsBundleFile = bundleDirectory.GetFiles().First(file => file.Name is "assets");
            sceneBundleFile.MoveTo(Path.Combine(zipArchivePath, "scene"));
            assetsBundleFile.MoveTo(Path.Combine(zipArchivePath, "assets"));

            AssetDatabase.DeleteAssets(staticMeshPaths.ToArray(), new());
            File.Delete(staticMeshOutputPath+".meta");
            bundleDirectory.Delete(recursive: true);
            staticMeshOutputDirectory.Delete(recursive:true);
            


            string mapConfigPath = Path.Combine(zipArchivePath, "MapConfig.json");
            string mapPath = Path.Combine(zipArchivePath, "Map.json");
            string manifestPath = Path.Combine(zipArchivePath, "manifest.json");

            File.WriteAllText(mapConfigPath, mapConfigJson);
            File.WriteAllText(mapPath, mapJson);
            File.WriteAllText(manifestPath, manifestJson);

            string amfPath = $"{mapRoot.Name}.amf"; // TODO get ablity to specify output path
            File.Delete(amfPath);
            ZipFile.CreateFromDirectory(zipArchivePath, amfPath);

            zipArchiveDirectory.Delete(recursive: true);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();


        }
    }
}
#endif