using UnityEngine;
using UnityEditor;

namespace KevinCastejon.EditorToolbox
{
    [CustomEditor(typeof(SpriteSheetConfiguration))]
    public class SpriteSheetConfigurationEditor : Editor
    {
        private SpriteSheetConfiguration _script;

        private void OnEnable()
        {
            _script = target as SpriteSheetConfiguration;
        }
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Open the AnimationGenerator window"))
            {
                AnimationGeneratorWindow window = EditorWindow.GetWindow(typeof(AnimationGeneratorWindow)) as AnimationGeneratorWindow;
                window.Config = _script;
            }
        }
    }
}