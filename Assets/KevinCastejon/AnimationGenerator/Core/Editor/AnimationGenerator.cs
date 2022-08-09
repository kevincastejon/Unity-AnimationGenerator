using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace KevinCastejon.EditorToolbox
{
    /// <summary>
    /// Generates AnimationClip assets from a Texture2D spritesheet asset and allows to save and load a configuration file
    /// </summary>
    public class AnimationGenerator : EditorWindow
    {
        static GUIStyle BoldStyle;
        private Texture2D _spritesheet;
        private List<Sprite> _sprites;
        private SpriteSheetConfiguration _config;
        private AnimationInfo _presetInfo;
        private Editor _previewEditor;
        private GUIStyle _horizontalLine;
        private bool _isPlaying;
        private int _spriteIndexPreview;
        private int _animIndexPreview = 0;
        private int _spriteStartIndex;
        private float _frameDuration;
        private float _nextFrameTime;
        private string _internalPath;
        private Vector2 _animScrollPos;
        private Vector2 _previewScrollPos;

        private void OnEnable()
        {
            _internalPath = EditorPrefs.HasKey("AnimationGeneratorSpriteRendererInternalPath") ? EditorPrefs.GetString("AnimationGeneratorSpriteRendererInternalPath") : "";
            titleContent = new GUIContent("Animation Generator");
            minSize = new Vector2(800, 1000);
        }

        private void Update()
        {
            if (_isPlaying)
            {
                if (EditorApplication.timeSinceStartup >= _nextFrameTime)
                {
                    if (_spriteStartIndex + _presetInfo.length - 1 >= _sprites.Count)
                    {
                        return;
                    }
                    _spriteIndexPreview++;
                    if (_spriteIndexPreview < _spriteStartIndex || _spriteIndexPreview >= _spriteStartIndex + _presetInfo.length)
                    {
                        _spriteIndexPreview = _spriteStartIndex;
                    }
                    _nextFrameTime = (float)EditorApplication.timeSinceStartup + _frameDuration;
                }
            }
            if (Selection.activeObject && (Selection.activeObject as Texture2D) != null)
            {
                GetSprites();
            }
            Repaint();
        }

        private void OnGUI()
        {
            if (_spritesheet == null)
            {
                EditorGUILayout.LabelField("Select a spritesheet in the project view");
                return;
            }
            BoldStyle = new GUIStyle(EditorStyles.label);
            BoldStyle.fontStyle = FontStyle.Bold;
            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("Generate AnimationClip assets from " + _spritesheet.name + " (" + _sprites.Count + " sprites)", BoldStyle);
            EditorGUILayout.Space(10);
            EditorGUILayout.BeginHorizontal();
            _config = EditorGUILayout.ObjectField("Animation Config", _config, typeof(SpriteSheetConfiguration), false) as SpriteSheetConfiguration;
            if (GUILayout.Button("+", GUILayout.MaxWidth(16f)))
            {
                string newConfigPath = EditorUtility.SaveFilePanelInProject("Create an animation config file", _spritesheet.name + "Config", "asset", "Save a file", EditorPrefs.HasKey("AnimationGeneratorOutputConfigPath") ? EditorPrefs.GetString("AnimationGeneratorOutputConfigPath") : "Assets");
                if (newConfigPath.Length > 0)
                {
                    EditorPrefs.SetString("AnimationGeneratorOutputConfigPath", newConfigPath);
                    SpriteSheetConfiguration ssc = CreateInstance<SpriteSheetConfiguration>();
                    AssetDatabase.CreateAsset(ssc, newConfigPath);
                    AssetDatabase.Refresh();
                    _config = ssc;
                }
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(5);
            if (_config == null)
            {
                return;
            }
            SerializedObject presetSo = new SerializedObject(_config);
            SerializedProperty sp = presetSo.FindProperty("animations");
            _animScrollPos = EditorGUILayout.BeginScrollView(_animScrollPos, GUILayout.MaxHeight(400f));
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(sp, new GUIContent("Animations"));
            if (EditorGUI.EndChangeCheck())
            {
                _config.EnsureNotEmpty();
                _config.EnsureUniqueNames();
                if (_animIndexPreview >= _config.animations.Count)
                {
                    _animIndexPreview = _config.animations.Count - 1;
                }
            }
            EditorGUILayout.EndScrollView();
            EditorGUILayout.Space(10);
            EditorGUI.BeginChangeCheck();
            _internalPath = EditorGUILayout.TextField("Sprite internal path", _internalPath);
            if (EditorGUI.EndChangeCheck())
            {
                EditorPrefs.SetString("AnimationGeneratorSpriteRendererInternalPath", _internalPath);
            }
            EditorGUILayout.Space(10);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space(10);
            if (GUILayout.Button("Ok"))
            {
                WriteFiles();
            }
            EditorGUILayout.Space(10);
            if (GUILayout.Button("Cancel"))
            {
                Close();
            }
            EditorGUILayout.Space(10);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(10);
            _horizontalLine = new GUIStyle();
            _horizontalLine.normal.background = EditorGUIUtility.whiteTexture;
            _horizontalLine.margin = new RectOffset(0, 0, 4, 4);
            _horizontalLine.fixedHeight = 1;
            HorizontalLine(Color.grey);
            string[] names = GetNamesArray();
            EditorGUI.BeginChangeCheck();
            _animIndexPreview = EditorGUILayout.Popup(_animIndexPreview, names);
            if (EditorGUI.EndChangeCheck())
            {
                _spriteIndexPreview = GetSpriteStartIndexFromAnimationIndex(_animIndexPreview);
            }
            string newFocus = GUI.GetNameOfFocusedControl();
            if (newFocus.Length > 0 && newFocus != "Config")
            {
                string selectedAnimName = newFocus.Substring(0, newFocus.Length - 1);
                _animIndexPreview = GetAnimationIndexByString(selectedAnimName);
                _spriteIndexPreview = GetSpriteStartIndexFromAnimationIndex(_animIndexPreview);
                GUI.FocusControl("");
            }
            if (_animIndexPreview > -1)
            {
                _presetInfo = _config.animations[_animIndexPreview];
                _frameDuration = 1f / _presetInfo.framerate;
                _spriteStartIndex = GetSpriteStartIndexFromAnimationIndex(_animIndexPreview);
                if (_spriteStartIndex + _presetInfo.length - 1 >= _sprites.Count)
                {
                    EditorGUILayout.LabelField(new GUIContent("Not enougth sprites into the " + _spritesheet.name + " Texture asset !"));
                    return;
                }
                EditorGUI.BeginChangeCheck();
                _isPlaying = EditorGUILayout.Toggle("Play", _isPlaying);
                if (EditorGUI.EndChangeCheck())
                {
                    _nextFrameTime = 0;
                }
                _spriteIndexPreview = Mathf.Clamp(EditorGUILayout.IntSlider("Preview sprites", _spriteIndexPreview, 0, _sprites.Count - 1), _spriteStartIndex, _spriteStartIndex + _presetInfo.length - 1);
                Rect lastRect = GUILayoutUtility.GetLastRect();
                lastRect.y += lastRect.height / 2;
                lastRect.height *= 0.5f;
                lastRect.y -= lastRect.height / 2;
                lastRect.width -= 207;
                lastRect.x += 152;
                float totalWid = lastRect.width;
                float startX = lastRect.x;
                lastRect.width = _spriteStartIndex / (_sprites.Count - 1f) * (totalWid - 10);
                Color maskColor = Color.black;
                maskColor.a = 0.5f;
                EditorGUI.DrawRect(lastRect, maskColor);
                lastRect.width = (_sprites.Count - (_spriteStartIndex + _presetInfo.length)) / (float)_sprites.Count * (totalWid - 8);
                lastRect.x = startX + totalWid - lastRect.width;
                EditorGUI.DrawRect(lastRect, maskColor);
                _previewScrollPos = EditorGUILayout.BeginScrollView(_previewScrollPos, GUILayout.Height(400f), GUILayout.Width(position.width));
                _previewEditor = Editor.CreateEditor(_sprites[_spriteIndexPreview]);
                _previewEditor.OnPreviewGUI(new Rect(0, 0, position.width, 400), new GUIStyle { normal = { background = Texture2D.grayTexture } });
                GUILayout.EndScrollView();
            }
        }

        private void GetSprites()
        {
            string selectedPath = AssetDatabase.GetAssetPath(Selection.activeObject.GetInstanceID());
            _spritesheet = Selection.activeObject as Texture2D;
            if (_spritesheet)
            {
                _sprites = AssetDatabase.LoadAllAssetsAtPath(selectedPath).OfType<Sprite>().ToList();
                _sprites.Sort(delegate (Sprite x, Sprite y)
                {
                    return EditorUtility.NaturalCompare(x.name, y.name);
                });
            }
        }

        private int GetSpriteStartIndexFromAnimationIndex(int animIndex)
        {
            int spriteIndex = 0;
            for (int i = 0; i < animIndex; i++)
            {
                spriteIndex += _config.animations[i].length;
            }
            return spriteIndex;
        }

        private int GetAnimationIndexByString(string name)
        {
            for (int i = 0; i < _config.animations.Count; i++)
            {
                if (_config.animations[i].name == name)
                {
                    return i;
                }
            }
            return -1;
        }

        private string[] GetNamesArray()
        {
            string[] names = new string[_config.animations.Count];
            for (int i = 0; i < _config.animations.Count; i++)
            {
                names[i] = _config.animations[i].name;
            }
            return names;
        }

        private void HorizontalLine(Color color)
        {
            var c = GUI.color;
            GUI.color = color;
            GUILayout.Box(GUIContent.none, _horizontalLine);
            GUI.color = c;
        }

        private void WriteFiles()
        {
            string folderPath = EditorUtility.OpenFolderPanel("Choose a folder to create your AnimationClip assets", EditorPrefs.HasKey("AnimationGeneratorOutputFolderPath") ? EditorPrefs.GetString("AnimationGeneratorOutputFolderPath") : "Assets", "");
            EditorPrefs.SetString("AnimationGeneratorOutputFolderPath", folderPath);
            int assetsStringIndex = folderPath.IndexOf("Assets");
            if (assetsStringIndex == -1)
            {
                return;
            }
            folderPath = folderPath.Substring(folderPath.IndexOf("Assets"));
            Close();
            int spriteCount = 0;
            for (int i = 0; i < _config.animations.Count; i++)
            {
                AnimationInfo presetInfo = _config.animations[i];
                if (spriteCount + presetInfo.length > _sprites.Count)
                {
                    break;
                }
                Sprite[] spr = new Sprite[presetInfo.length];
                _sprites.CopyTo(spriteCount, spr, 0, presetInfo.length);
                AnimationClip clip = CreateAnimationClip(spr, presetInfo, _internalPath);
                AssetDatabase.CreateAsset(clip, folderPath + "/" + presetInfo.name + ".anim");
                spriteCount += presetInfo.length;
            }
        }
        
        [MenuItem("Window/Animation/AnimationGenerator")]
        private static void OpenWindow()
        {
            AnimationGenerator window = GetWindow(typeof(AnimationGenerator)) as AnimationGenerator;
        }

        private static AnimationClip CreateAnimationClip(Sprite[] sprites, AnimationInfo presetInfo, string path)
        {
            AnimationClip clip = new AnimationClip();
            clip.frameRate = presetInfo.framerate;

            ObjectReferenceKeyframe[] keys = new ObjectReferenceKeyframe[presetInfo.length];
            for (int i = 0; i < presetInfo.length; i++)
            {
                keys[i] = new ObjectReferenceKeyframe
                {
                    time = i / clip.frameRate,
                    value = sprites[i]
                };
            }
            EditorCurveBinding curveBinding = new EditorCurveBinding();
            curveBinding.propertyName = "m_Sprite";
            curveBinding.path = path;
            curveBinding.type = typeof(SpriteRenderer);
            AnimationUtility.SetObjectReferenceCurve(clip, curveBinding, keys);
            AnimationClipSettings clipSetting = new AnimationClipSettings
            {
                loopTime = presetInfo.loop,
                stopTime = 1f
            };
            AnimationUtility.SetAnimationClipSettings(clip, clipSetting);
            return clip;
        }

    }
}