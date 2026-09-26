using MCEEditor.Components;
using UnityEditor;


namespace MCEEditor.UI
{
    [CustomEditor(typeof(ConvertColliders))]
    public class ConvertCollidersEditor : Editor
    {
        public ConvertColliders colliderObj;
        private void OnEnable()
        {
            colliderObj = (ConvertColliders)target;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.HelpBox("This recursively converts Unity colliders to Quantums MapStaticCollider3D during the build process.",MessageType.Info);
            if (colliderObj.HasMeshColliders)
                EditorGUILayout.HelpBox("Mesh colliders are currently unsupported and will be ignored.\nThis object or one of its children has a mesh collider.", MessageType.Warning);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
