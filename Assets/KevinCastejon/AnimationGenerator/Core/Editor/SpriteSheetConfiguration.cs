using System.Collections.Generic;
using UnityEngine;

namespace KevinCastejon.EditorToolbox
{
    /// <summary>
    /// Configuration asset for generating AnimationClip assets from a Texture2D spritesheet asset into AnimationGenerator window.
    /// </summary>
    [CreateAssetMenu(menuName = "Animation Spritesheet Configuration", order = 400)]
    public class SpriteSheetConfiguration : ScriptableObject
    {
        public List<AnimationInfo> animations = new List<AnimationInfo>();

        public void EnsureUniqueNames()
        {
            Dictionary<string, int> names = new Dictionary<string, int>();
            for (int i = 0; i < animations.Count; i++)
            {
                if (names.ContainsKey(animations[i].name))
                {
                    names[animations[i].name]++;
                    animations[i].name = animations[i].name + "_";
                }
                else
                {
                    names.Add(animations[i].name, 0);
                }
            }
        }

        public void EnsureNotEmpty()
        {
            if (animations.Count == 0)
            {
                Reset();
            }
        }

        private void Reset()
        {
            animations = new List<AnimationInfo>() { new AnimationInfo() };
        }
    }
}