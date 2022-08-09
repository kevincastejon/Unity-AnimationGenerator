using UnityEditor;
using UnityEngine;

namespace KevinCastejon.EditorToolbox
{
    [CustomPropertyDrawer(typeof(AnimationInfo))]
    public class AnimationInfoDrawer : PropertyDrawer
    {
        private readonly int _numberOfFields = 5;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return 20f * _numberOfFields;
        }
        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
        {
            SerializedProperty name = property.FindPropertyRelative("name");
            SerializedProperty length = property.FindPropertyRelative("length");
            SerializedProperty loop = property.FindPropertyRelative("loop");
            SerializedProperty autoFramerate = property.FindPropertyRelative("autoFramerate");
            SerializedProperty framerate = property.FindPropertyRelative("framerate");
            rect.height -= 4;
            float rectWid = rect.width;
            float rectX = rect.x;
            rect.height /= _numberOfFields;
            rect.width = 35;
            if (GUI.Button(rect, EditorGUIUtility.ObjectContent(null, typeof(Animation)).image))
            {
                GUI.FocusControl(name.stringValue + "0");
            }
            rect.x = rectX + 35;
            rect.width = rectWid * 0.25f;
            GUI.SetNextControlName(name.stringValue + "0");
            EditorGUI.SelectableLabel(rect, name.stringValue);
            rect.x = rectX + rectWid * 0.25f;
            rect.width = rectWid * 0.75f;
            EditorGUI.DelayedTextField(rect, name, new GUIContent("Name"));
            rect.y += rect.height;
            EditorGUI.DelayedIntField(rect, length, new GUIContent("Number of sprites"));
            length.intValue = Mathf.Max(length.intValue, 1);
            rect.y += rect.height;
            loop.boolValue = EditorGUI.Toggle(rect, new GUIContent("Loop"), loop.boolValue);
            rect.y += rect.height;
            autoFramerate.boolValue = EditorGUI.Toggle(rect, new GUIContent("AutoFramerate", "AutoFramerate will set the framerate equals to the number of frames so the animation duration will be 1 second"), autoFramerate.boolValue);
            if (autoFramerate.boolValue)
            {
                EditorGUI.BeginDisabledGroup(true);
                framerate.intValue = length.intValue;
            }
            rect.y += rect.height;
            EditorGUI.DelayedIntField(rect, framerate, new GUIContent("Framerate"));
            framerate.intValue = Mathf.Clamp(framerate.intValue, 0, 999);
            if (autoFramerate.boolValue)
            {
                EditorGUI.EndDisabledGroup();
            }
        }
    }
}