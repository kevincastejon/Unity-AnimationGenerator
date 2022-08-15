using System;
using UnityEngine;

namespace KevinCastejon.EditorToolbox
{
    [Serializable]
    public class AnimationInfo
    {
        [SerializeField] private string _name;
        [SerializeField] private int _length;
        [SerializeField] private bool _loop;
        [SerializeField] private bool _autoFramerate;
        [SerializeField] private int _frameRate;

        public AnimationInfo()
        {
            _name = "AnimationClipName";
            _length = 1;
            _loop = true;
            _autoFramerate = true;
            _frameRate = 60;
        }

        public string Name { get => _name; }
        public int Length { get => _length; }
        public bool Loop { get => _loop; }
        public bool AutoFramerate { get => _autoFramerate; }
        public int FrameRate { get => _frameRate; }
    }
}