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
            SerializedProperty name = property.FindPropertyRelative("_name");
            SerializedProperty length = property.FindPropertyRelative("_length");
            SerializedProperty loop = property.FindPropertyRelative("_loop");
            SerializedProperty autoFramerate = property.FindPropertyRelative("_autoFramerate");
            SerializedProperty frameRate = property.FindPropertyRelative("_frameRate");
            rect.height -= 4;
            float rectWid = rect.width;
            float rectX = rect.x;
            rect.height /= _numberOfFields;
            //rect.width = 35;
            //if (GUI.Button(rect, EditorGUIUtility.ObjectContent(null, typeof(Animation)).image))
            //{
            //    GUI.FocusControl(name.stringValue + "0");
            //}
            //rect.x = rectX + 35;
            rect.width = rectWid * 0.25f;
            EditorGUI.SelectableLabel(rect, name.stringValue);
            rect.x = rectX + rectWid * 0.25f;
            rect.width = rectWid * 0.75f;
            EditorGUI.DelayedTextField(rect, name, new GUIContent("Name"));
            rect.y += rect.height;
            length.intValue = EditorGUI.IntField(rect, new GUIContent("Frame Count"), length.intValue);
            length.intValue = Mathf.Max(length.intValue, 1);
            rect.y += rect.height;
            loop.boolValue = EditorGUI.Toggle(rect, new GUIContent("Loop"), loop.boolValue);
            rect.y += rect.height;
            autoFramerate.boolValue = EditorGUI.Toggle(rect, new GUIContent("AutoFramerate", "AutoFramerate will set the framerate equals to the number of frames so the animation duration will be 1 second"), autoFramerate.boolValue);
            if (autoFramerate.boolValue)
            {
                EditorGUI.BeginDisabledGroup(true);
                frameRate.intValue = length.intValue;
            }
            rect.y += rect.height;
            frameRate.intValue = EditorGUI.IntField(rect, new GUIContent("Frame Rate"), frameRate.intValue);
            frameRate.intValue = Mathf.Clamp(frameRate.intValue, 0, 999);
            if (autoFramerate.boolValue)
            {
                EditorGUI.EndDisabledGroup();
            }
        }
    }
}