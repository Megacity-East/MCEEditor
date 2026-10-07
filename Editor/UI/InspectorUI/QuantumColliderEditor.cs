using MCEEditor.Components;
using UnityEditor;
using UnityEngine;


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
            if (colliderObj.HasAnyColliders == false)
            {
                EditorGUILayout.HelpBox("This object has no colliders.", MessageType.Error);
                return;
            }
            if (colliderObj.HasMultipleColliders)
                EditorGUILayout.HelpBox("This Object has multiple colliders, QuantumCollider only supports 1 collider per object.\nYou may experience undefined behaviour.", MessageType.Warning);
            if (colliderObj.UnityCollider is not BoxCollider)
                if (colliderObj.transform.lossyScale != Vector3.one)
                    EditorGUILayout.HelpBox("The only collider which supports scaling is the BoxCollider.\nYou may experience undefined behaviour.", MessageType.Warning);

        }
    }
}