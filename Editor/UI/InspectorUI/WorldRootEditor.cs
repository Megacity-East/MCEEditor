using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Codice.Client.BaseCommands.Merge.IncomingChanges;
using MCEEditor.Components;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace MCEEditor.UI
{
    [CustomEditor(typeof(WorldRoot))]
    public class WorldRootEditor : Editor
    {

        public SerializedProperty mapNameProp;
        public SerializedProperty displayNameProp;
        public SerializedProperty authorsProp;
        public SerializedProperty devSpawnProp;
        public SerializedProperty guidProp;
        public SerializedProperty versionProp;

        private void OnEnable()
        {
            guidProp = serializedObject.FindProperty(nameof(WorldRoot.Guid));
            mapNameProp = serializedObject.FindProperty(nameof(WorldRoot.Name));
            authorsProp = serializedObject.FindProperty(nameof(WorldRoot.Authors));
            versionProp = serializedObject.FindProperty(nameof(WorldRoot.Version));
            devSpawnProp = serializedObject.FindProperty(nameof(WorldRoot.DevSpawn));
            displayNameProp = serializedObject.FindProperty(nameof(WorldRoot.DisplayName));

        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            WorldRoot root = (WorldRoot)serializedObject.targetObject;
            EditorGUILayout.LabelField("Map GUID: " + guidProp.stringValue);
            
            /// Map name warnings
            if (!Regex.IsMatch(mapNameProp.stringValue, "^[a-zA-Z0-9_]*$"))
            {
                string message = @"Invalid format, must be alphanumerical.";
                EditorGUILayout.HelpBox(message, MessageType.Error);
            }
            if (mapNameProp.stringValue.Length == 0)
            {
                string message = @"Invalid format, must not be empty.";
                EditorGUILayout.HelpBox(message, MessageType.Error);
            }
            if (mapNameProp.stringValue != SceneManager.GetActiveScene().name)
            {
                string message = @"Scene name does not match map name, this is known to cause instability";
                EditorGUILayout.HelpBox(message, MessageType.Warning);
            }

            EditorGUILayout.PropertyField(mapNameProp);

            EditorGUILayout.PropertyField(displayNameProp);

            EditorGUILayout.PropertyField(authorsProp);

            EditorGUILayout.PropertyField(versionProp);
            if (root.DevSpawn == null)
            {
                string message = @"No Devspawn specified, will default to 0,0,0";
                EditorGUILayout.HelpBox(message, MessageType.Warning);
            }
            EditorGUILayout.PropertyField(devSpawnProp);


            serializedObject.ApplyModifiedProperties();
        }
    }
}
