using System;
using System.Linq;
using System.Reflection;
using MCEEditor.Components;
using MCEEditor.Components.MCEPrototypes;
using UnityEditor;
using UnityEngine;


namespace MCEEditor.UI
{
    [CustomEditor(typeof(MCEPrototypeSet))]
    public class MCEPrototypeSetEditor : Editor
    {
        private MCEPrototypeSet prototypeSet;

        private SerializedProperty advancedModeProp;
        private SerializedProperty prototypesProp;

        private string newTypeString = "MCEEditor.Components.MCEPrototypes.MCEPrototype";
        private void OnEnable()
        {
            prototypeSet = (MCEPrototypeSet)target;
            advancedModeProp = serializedObject.FindProperty(nameof(MCEPrototypeSet.AdvancedMode));
            prototypesProp = serializedObject.FindProperty(nameof(MCEPrototypeSet.prototypes));
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(advancedModeProp);
            if (prototypeSet.AdvancedMode)
            {
                EditorGUILayout.HelpBox("MCEPrototypeSets are not meant to be edited by regular users.\nExpect ZERO documentation, you're on your own.", MessageType.Warning);
                EditorGUILayout.PropertyField(prototypesProp);

                newTypeString = EditorGUILayout.TextField(newTypeString);
                if (GUILayout.Button("AddComponent"))
                {
                    Assembly protypeAssembly = Assembly.GetAssembly(typeof(MCEPrototype));
                    Type newComponentType = protypeAssembly.GetTypes().First(type => type.FullName == newTypeString);
                    ConstructorInfo constructor = newComponentType.GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, CallingConventions.HasThis ,new Type[0], null);

                    if (constructor != null)
                    {
                        MCEPrototype prototype = (MCEPrototype)constructor.Invoke(null);
                        int componentIndex = prototypeSet.prototypes.Count;
                        prototypeSet.prototypes.Add(prototype);
                        prototype.OnComponentAdded(prototypeSet,componentIndex);
                    }

                }

            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}