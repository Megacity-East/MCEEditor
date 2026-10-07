using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using System;
using System.Linq;

using MCEEditor.Components;
using MCEEditor.Builder;
using System.IO;
using UnityEngine.SceneManagement;

namespace MCEEditor.UI
{

    public class MapSettings : EditorWindow
    {
        [MenuItem("MCEEditor/Map Settings")]
        public static void OpenSettingsWindow()
        {

            EditorWindow wnd = GetWindow<MapSettings>();
            wnd.titleContent = new GUIContent("Map Settings");

        }

        public void CreateGUI()
        {
            VisualElement root = rootVisualElement;


            WorldRoot[] roots = Resources.FindObjectsOfTypeAll<WorldRoot>();
            if (roots.Length == 1)
            {
                WorldRoot worldRoot = roots.First();
                Action buildAction = () => MapBuilder.BuildMap(worldRoot);
                Button TriggerBuild = new(buildAction)
                {
                    text = "Build Map"
                };

                root.Add(TriggerBuild);
            }

        }
    }
}
