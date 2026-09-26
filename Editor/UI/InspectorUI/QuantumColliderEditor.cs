using MCEEditor.Components;
using UnityEditor;


namespace MCEEditor.UI
{
    [CustomEditor(typeof(QuantumCollider))]
    public class QuantumColliderEditor : Editor
    {
        public QuantumCollider colliderObj;

        private void OnEnable()
        {
            colliderObj = (QuantumCollider)target;
        }

        public override void OnInspectorGUI()
        {
            colliderObj.UpdateFields();
            serializedObject.Update();
            if(colliderObj.HasAnyColliders == false)
            {
                EditorGUILayout.HelpBox("This object has no colliders.", MessageType.Error);
                return;
            }
            if (colliderObj.IsMeshCollider)
                EditorGUILayout.HelpBox("Mesh colliders are currently unsupported and will be ignored.\nThis object has a mesh collider.", MessageType.Warning);
            if (colliderObj.HasMultipleColliders)
                EditorGUILayout.HelpBox("This Object has multiple colliders, QuantumCollider only supports 1 collider per object.", MessageType.Warning);

        }
    }
}