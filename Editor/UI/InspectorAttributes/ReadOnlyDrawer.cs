// Source - https://stackoverflow.com/a/77920674
// Posted by Ujjwal Raut, modified by community. See post 'Timeline' for change history
// Retrieved 2026-10-26, License - CC BY-SA 4.0

using UnityEngine;
using UnityEditor;

namespace MCEEditor.UI.Attributes
{

    // Put this script in the "Editor" folder
    [CustomPropertyDrawer(typeof(MCEEditor.Attributes.ReadOnlyAttribute))]
    public class ReadOnlyDrawer : PropertyDrawer
    {

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {

            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
    }
}