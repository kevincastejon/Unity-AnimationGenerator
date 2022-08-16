using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

namespace KevinCastejon.EditorToolbox
{
    /// <summary>
    /// Configuration asset for generating AnimationClip assets from a Texture2D spritesheet asset into AnimationGenerator window.
    /// </summary>
    [CreateAssetMenu(menuName = "Animation Spritesheet Configuration", order = 400)]
    public class SpriteSheetConfiguration : ScriptableObject
    {
        [SerializeField] private List<AnimationInfo> _animations = new List<AnimationInfo>();
        public List<AnimationInfo> Animations { get => _animations; }

        [OnOpenAsset]
        private static bool OpenSpriteSheet(int instanceID, int line)
        {
            AnimationGeneratorWindow window = EditorWindow.GetWindow(typeof(AnimationGeneratorWindow)) as AnimationGeneratorWindow;
            window.Config = (SpriteSheetConfiguration)EditorUtility.InstanceIDToObject(instanceID);
            return true;
        }
    }
}