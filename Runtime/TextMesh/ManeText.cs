using System;
using System.Text;
using Mane.Inspector;
using UnityEngine;

namespace Mane
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public partial class ManeText : MonoBehaviour
    {
        private const string Shader = "ManeText/Alpha";
        private static readonly int AlphaColor = UnityEngine.Shader.PropertyToID("_AlphaColor");


        public enum HorizontalAlignment
        {
            Left = 1,
            Center = 0,
            Right = 2,
        }

        public enum VerticalAlignment
        {
            Top = 1,
            Center = 0,
            Bottom = 2,
        }

        [Flags]
        public enum TextEffect
        {
            None = 0,
            Outline = 1,
            Shadow = 2,
        }

        [Flags]
        private enum DirtyFlags
        {
            None = 0,
            Mesh = 1,
            Material = 2,
        }

        private DirtyFlags AllDirty => (DirtyFlags)3;


        [SerializeField, TextArea] private string _text = string.Empty;

        [Space]
        [SerializeField] private Font _font;
        [SerializeField] private int _fontSize = 24;
        [SerializeField] private float _characterSize = 1f;
        [SerializeField] private Color _color = Color.white;
        [SerializeField] private HorizontalAlignment _horizontal = HorizontalAlignment.Center;
        [SerializeField] private VerticalAlignment _vertical = VerticalAlignment.Center;
        [SerializeField] private int _spacingX;
        [SerializeField] private int _spacingY;
        [SerializeField] private int _maxWidth;
        [SerializeField] private int _maxHeight;
        [SerializeField] private bool _breakDigits = true;

        [Space]
        [SerializeField] private TextEffect _effect = TextEffect.None;
        [AvailableIf("IsOutlineActive", hide: true)]
        [SerializeField] private float _outlineSize;
        [AvailableIf("IsOutlineActive", hide: true)]
        [SerializeField] private Color _outlineColor = Color.red;
        [AvailableIf("IsShadowActive", hide: true)]
        [SerializeField] private Vector2 _shadowOffset = new Vector2(3, -3);
        [AvailableIf("IsShadowActive", hide: true)]
        [SerializeField] private Color _shadowColor = new Color(0, 0, 0, .3f);
        [AvailableIf("IsNoEffectsActive", true, true)]
        [SerializeField] private float _effectsShiftZ = .1f;


        private Mesh _mesh;
        private MeshRenderer _rendererInternal;
        private int _sortingOrder;
        private Vector2 _size = Vector2.zero;
        private DirtyFlags _dirty = (DirtyFlags)3;


        public bool Dirty => _dirty != DirtyFlags.None;

        public Font @Font
        {
            get => _font;
            set
            {
                if (_font == value) return;

                _font = value;
                _dirty = AllDirty;
            }
        }

        public string Text
        {
            get => _text;
            set
            {
                if (_text == value)
                    return;

                _text = value;
                _dirty |= DirtyFlags.Mesh;
            }
        }

        public int FontSize
        {
            get => _fontSize;
            set
            {
                if (_fontSize == value)
                    return;

                _fontSize = value;
                _dirty = DirtyFlags.Mesh;
            }
        }

        public HorizontalAlignment Horizontal
        {
            get => _horizontal;
            set
            {
                if (_horizontal == value)
                    return;

                _horizontal = value;
                _dirty |= DirtyFlags.Mesh;
            }
        }

        public VerticalAlignment Vertical
        {
            get => _vertical;
            set
            {
                if (_vertical == value)
                    return;

                _vertical = value;
                _dirty |= DirtyFlags.Mesh;
            }
        }

        public int SpacingX
        {
            get => _spacingX;
            set
            {
                if (_spacingX == value)
                    return;

                _spacingX = value;
                _dirty |= DirtyFlags.Mesh;
            }
        }

        public int SpacingY
        {
            get => _spacingY;
            set
            {
                if (_spacingY == value)
                    return;

                _spacingY = value;
                _dirty |= DirtyFlags.Mesh;
            }
        }

        public int MaxWidth
        {
            get => _maxWidth;
            set
            {
                if (_maxWidth == value)
                    return;

                _maxWidth = value;
                _dirty |= DirtyFlags.Mesh;
            }
        }

        public int MaxHeight
        {
            get => _maxHeight;
            set
            {
                if (_maxHeight == value)
                    return;

                _maxHeight = value;
                _dirty |= DirtyFlags.Mesh;
            }
        }

        public bool BreakDigits
        {
            get => _breakDigits;
            set
            {
                if (_breakDigits == value)
                    return;

                _breakDigits = value;
                _dirty |= DirtyFlags.Mesh;
            }
        }

        public TextEffect Effect
        {
            get => _effect;
            set
            {
                if (_effect == value)
                    return;

                _effect = value;
                _dirty |= DirtyFlags.Mesh;
            }
        }

        public float OutlineSize
        {
            get => _outlineSize;
            set
            {
                if (Math.Abs(_outlineSize - value) < float.Epsilon) return;

                _outlineSize = value;
                if (IsOutlineActive)
                    _dirty |= DirtyFlags.Mesh;
            }
        }

        public Color OutlineColor
        {
            get => _outlineColor;
            set
            {
                if (_outlineColor == value)
                    return;

                _outlineColor = value;
                if (IsOutlineActive)
                    _dirty |= DirtyFlags.Mesh;
            }
        }

        public Vector2 ShadowOffset
        {
            get => _shadowOffset;
            set
            {
                if (_shadowOffset == value)
                    return;

                _shadowOffset = value;
                if (IsShadowActive)
                    _dirty |= DirtyFlags.Mesh;
            }
        }

        public Color ShadowColor
        {
            get => _shadowColor;
            set
            {
                if (_shadowColor == value)
                    return;

                _shadowColor = value;
                if (IsShadowActive)
                    _dirty |= DirtyFlags.Mesh;
            }
        }

        public Color @Color
        {
            get => _color;
            set
            {
                if (Math.Abs(_color.a - value.a) > float.Epsilon)
                    _dirty |= DirtyFlags.Material;

                if (Math.Abs(_color.r - value.r) > float.Epsilon ||
                    Math.Abs(_color.g - value.g) > float.Epsilon ||
                    Math.Abs(_color.b - value.b) > float.Epsilon)
                    _dirty |= DirtyFlags.Mesh;

                _color = value;
            }
        }

        private Color SolidColor
        {
            get
            {
                Color c = Color;
                c.a = 1;

                return c;
            }
        }

        public int SortingOrder
        {
            get => _sortingOrder;
            set
            {
                _sortingOrder = value;
                if (Renderer != null)
                    Renderer.sortingOrder = value;
            }
        }

        public Vector2 Size
        {
            get
            {
                if (_size == Vector2.zero)
                {
                    ManeTextInfo info = GetWrappedText();
                    if (info != null)
                    {
                        float baseLine = _font.lineHeight / _font.fontSize * _fontSize + _spacingY;
                        float lineHeight = (float) _font.ascent / _font.fontSize * _fontSize;
                        _size = new Vector2(info.MaxLength, baseLine * (info.String.Count - 1) + lineHeight);
                    }
                }

                return _size;
            }
        }


        private MeshRenderer Renderer
        {
            get
            {
                if (_rendererInternal == null)
                    _rendererInternal = GetComponent<MeshRenderer>();

                return _rendererInternal;
            }
        }

        public bool IsOutlineActive => (_effect & TextEffect.Outline) != 0;

        public bool IsShadowActive => (_effect & TextEffect.Shadow) != 0;

        public bool IsNoEffectsActive => _effect == TextEffect.None;

        
        private void Awake() => Font.textureRebuilt += OnFontTextureRebuilt;

        private void Update() => UpdateView(true);

#if UNITY_EDITOR
        private void OnValidate() => _dirty = AllDirty;
#endif

        private void OnDestroy()
        {
            Font.textureRebuilt -= OnFontTextureRebuilt;

            // Destroy mesh
            if (_mesh != null)
            {
#if UNITY_EDITOR
                if (Application.isPlaying)
                    Destroy(_mesh);
                else
                    DestroyImmediate(_mesh);
#else
			    Destroy(_mesh);
#endif
            }

            // Destroy material if need
            if (!ContainMaterial(Renderer.sharedMaterial))
                DestroyMaterial(Renderer.sharedMaterial);
        }

        private void OnFontTextureRebuilt(Font changedFont)
        {
            if (changedFont != _font) return;

            _dirty |= DirtyFlags.Mesh;
            UpdateView(false);
        }


        private void UpdateView(bool requestCharacters)
        {
            if (_font == null) return;

            if (requestCharacters)
                _font.RequestCharactersInTexture(_text, _fontSize);

            if ((_dirty & DirtyFlags.Material) != 0)
                UpdateMaterial();

            if ((_dirty & DirtyFlags.Mesh) != 0)
                RebuildMesh();

            _dirty = DirtyFlags.None;
        }

        private void UpdateMaterial()
        {
            Material m = Renderer.sharedMaterial;
            bool cached = ContainMaterial(m);

            // Transparent to opaque || <Null> to opaque
            if (Math.Abs(_color.a - 1f) < float.Epsilon && (m == null || !cached))
            {
                if (m != null && !cached)
                    DestroyMaterial(m);

                m = FindMaterial(_font);
                if (m == null)
                    m = CreateMaterial(_font, true);

                Renderer.sharedMaterial = m;

                return;
            }

            // Update opaque
            if (Math.Abs(_color.a - 1f) < float.Epsilon && cached)
                return; // do nothing

            // Opaque to transparent || <Null> to transparent
            if (_color.a < 1f && (m == null || cached))
                Renderer.sharedMaterial = CreateMaterial(_font, false);

            // Update transparent
            Renderer.sharedMaterial.SetFloat(AlphaColor, _color.a);
        }

        private void RebuildMesh()
        {
            if (_mesh == null)
            {
                _mesh = new Mesh();
                _mesh.MarkDynamic();
                _mesh.hideFlags = HideFlags.DontSave;
                GetComponent<MeshFilter>().mesh = _mesh;
            }

            UpdateMaterial();

            ManeTextInfo info = GetWrappedText();

            Vector3[] vertices;
            int[] triangles;
            Color[] colors;
            Vector2[] uv;

            if (info != null)
            {
                int vertexMultiplier = 1;
                if (IsShadowActive)
                    vertexMultiplier += 1;
                if (IsOutlineActive)
                    vertexMultiplier += 8;

                // Creating vertex arrays etc.
                int length = info.TotalCount;
                vertices = new Vector3[length * 4 * vertexMultiplier];
                triangles = new int[length * 6 * vertexMultiplier];
                colors = new Color[vertices.Length];
                uv = new Vector2[vertices.Length];

                // Calculate base line (one line height)
                float baseLine = _font.lineHeight / _font.fontSize * _fontSize + _spacingY;
                float lineHeight = (float) _font.ascent / _font.fontSize * _fontSize;

                // Set vertical alignment
                Vector3 pos = Vector3.zero;
                switch (_vertical)
                {
                    case VerticalAlignment.Top:
                        pos = new Vector3(0, (info.String.Count - 1) * baseLine, 0);
                        break;

                    case VerticalAlignment.Center:
                        pos = new Vector3(0, -lineHeight * .5f + (info.String.Count - 1) * baseLine * .5f, 0);
                        break;

                    case VerticalAlignment.Bottom:
                        pos = new Vector3(0, -lineHeight, 0);
                        break;
                }

                // Creating mesh
                for (int s = 0, i = 0; s < info.String.Count; s++)
                {
                    // Calculate alignment offset
                    float offset = 0;
                    if (_horizontal == HorizontalAlignment.Right)
                        offset = -info.Length[s];
                    else if (_horizontal == HorizontalAlignment.Center)
                        offset = info.Length[s] * -.5f;

                    for (int ch = 0; ch < info.String[s].Length; ch++)
                    {
                        _font.GetCharacterInfo(info.String[s][ch], out CharacterInfo chi, _fontSize);

                        if (info.String[s][ch] != ' ')
                        {
                            // Create outline
                            if (IsOutlineActive)
                            {
                                int o = Effect == TextEffect.Outline ? 0 : 1;
                                CreateRect(chi, length * o++ + i, pos + new Vector3(_outlineSize, 0, _effectsShiftZ),
                                    offset, _outlineColor);
                                CreateRect(chi, length * o++ + i, pos + new Vector3(0, _outlineSize, _effectsShiftZ),
                                    offset, _outlineColor);
                                CreateRect(chi, length * o++ + i, pos + new Vector3(-_outlineSize, 0, _effectsShiftZ),
                                    offset, _outlineColor);
                                CreateRect(chi, length * o++ + i, pos + new Vector3(0, -_outlineSize, _effectsShiftZ),
                                    offset, _outlineColor);
                                CreateRect(chi, length * o++ + i,
                                    pos + new Vector3(_outlineSize, _outlineSize, _effectsShiftZ), offset,
                                    _outlineColor);
                                CreateRect(chi, length * o++ + i,
                                    pos + new Vector3(_outlineSize, -_outlineSize, _effectsShiftZ), offset,
                                    _outlineColor);
                                CreateRect(chi, length * o++ + i,
                                    pos + new Vector3(-_outlineSize, _outlineSize, _effectsShiftZ), offset,
                                    _outlineColor);
                                CreateRect(chi, length * o + i,
                                    pos + new Vector3(-_outlineSize, -_outlineSize, _effectsShiftZ), offset,
                                    _outlineColor);
                            } // Create shadow

                            if (IsShadowActive)
                                CreateRect(chi, i,
                                    pos + new Vector3(_shadowOffset.x, _shadowOffset.y, _effectsShiftZ * 2f), offset,
                                    _shadowColor);
                            // Create glyph
                            CreateRect(chi, length * (vertexMultiplier - 1) + i++, pos, offset, SolidColor);
                        }

                        pos += new Vector3(chi.advance + _spacingX, 0, 0);
                    }

                    pos = new Vector3(0, pos.y - baseLine, 0);
                }

                _size = new Vector3(info.MaxLength, baseLine * (info.String.Count - 1) + lineHeight, 0);

                _mesh.Clear();
                _mesh.vertices = vertices;
                _mesh.triangles = triangles;
                _mesh.colors = colors;
                _mesh.uv = uv;
            }
            else
            {
                _size = Vector3.zero;

                _mesh.Clear();
            }


            void CreateRect(CharacterInfo chi, int i, Vector3 pos, float offset, Color color)
            {
                pos *= _characterSize;

                vertices[4 * i + 0] = pos + new Vector3(chi.minX + offset - .5f, chi.maxY + .5f, 0) * _characterSize;
                vertices[4 * i + 1] = pos + new Vector3(chi.maxX + offset + .5f, chi.maxY + .5f, 0) * _characterSize;
                vertices[4 * i + 2] = pos + new Vector3(chi.maxX + offset + .5f, chi.minY - .5f, 0) * _characterSize;
                vertices[4 * i + 3] = pos + new Vector3(chi.minX + offset - .5f, chi.minY - .5f, 0) * _characterSize;

                colors[4 * i + 0] = color;
                colors[4 * i + 1] = color;
                colors[4 * i + 2] = color;
                colors[4 * i + 3] = color;

                float du = .5f / Renderer.sharedMaterial.mainTexture.width;
                float dv = .5f / Renderer.sharedMaterial.mainTexture.height;
                if (chi.uvTopLeft.x > chi.uvBottomRight.x || chi.uvTopLeft.y > chi.uvBottomRight.y)
                {
                    uv[4 * i + 0] = chi.uvTopLeft + new Vector2(du, dv);
                    uv[4 * i + 2] = chi.uvBottomRight + new Vector2(-du, -dv);
                }
                else
                {
                    uv[4 * i + 0] = chi.uvTopLeft + new Vector2(-du, -dv);
                    uv[4 * i + 2] = chi.uvBottomRight + new Vector2(du, dv);
                }

                uv[4 * i + 1] = chi.uvTopRight + new Vector2(du, -dv);
                uv[4 * i + 3] = chi.uvBottomLeft + new Vector2(-du, dv);

                triangles[6 * i + 0] = 4 * i + 0;
                triangles[6 * i + 1] = 4 * i + 1;
                triangles[6 * i + 2] = 4 * i + 2;

                triangles[6 * i + 3] = 4 * i + 0;
                triangles[6 * i + 4] = 4 * i + 2;
                triangles[6 * i + 5] = 4 * i + 3;
            }
        }

        private ManeTextInfo GetWrappedText()
        {
            // Break process if there is negative area size or no text
            if (_font == null || _maxWidth < 0 || _maxHeight < 0 || string.IsNullOrEmpty(_text))
                return null;

            // Make sure our font texture have all glyphs we need
            _font.RequestCharactersInTexture(_text, _fontSize);

            // Creating stuff
            ManeTextInfo res = new ManeTextInfo();
            StringBuilder sb = new StringBuilder();
            int textLength = _text.Length, start = 0, offset = 0, linesCount = 0, maxLines = int.MaxValue;
            float substringWidth = 0, lineWidth = 0;
            bool lineIsEmpty = true;
            if (_maxHeight > 0)
                maxLines = _maxHeight / (_font.lineHeight / _font.fontSize * _fontSize + _spacingY);

            // Let's magic begin
            for (; offset < textLength; ++offset)
            {
                char ch = _text[offset];

                // Whoa! End of line!
                if (ch == '\n')
                {
                    if (!lineIsEmpty)
                        sb.Append(' ');

                    // Add the previous word to the final string
                    if (start < offset)
                        sb.Append(_text.Substring(start, offset - start));
                    res.Append(sb.ToString(), substringWidth);
                    linesCount++;
                    if (linesCount > maxLines)
                        return res;

                    sb = new StringBuilder();
                    lineIsEmpty = true;
                    start = offset + 1;
                    substringWidth = 0;
                    lineWidth = 0;
                    continue;
                }

                // Request character info
                _font.GetCharacterInfo(ch, out CharacterInfo info, FontSize);

                // Calculate how wide this symbol or character is going to be
                float glyphWidth = _spacingX + info.advance;

                // If this marks the end of a word, add it to the final string.
                if (ch == ' ' && start < offset)
                {
                    int end = offset - start + 1;

                    // Last word on the last line should not include a spaces
                    if (_maxWidth > 0 && substringWidth > _maxWidth && offset < textLength)
                    {
                        char cho = Text[offset];
                        if (cho <= ' ') --end;
                    }

                    if (!_breakDigits || (_breakDigits && !char.IsDigit(_text[offset - 1])))
                    {
                        if (!lineIsEmpty) sb.Append(' ');
                        sb.Append(_text.Substring(start, end - 1));
                        lineIsEmpty = false;
                        lineWidth = substringWidth;
                        start = offset + 1;
                    }
                }

                // Increase the width
                substringWidth += glyphWidth;

                // Doesn't fit?
                if (_maxWidth > 0 && Mathf.RoundToInt(substringWidth) > _maxWidth)
                {
                    // Technically empty line means that even a one word doesn't fit here
                    if (lineIsEmpty)
                    {
                        res.Append(_text.Substring(start, Mathf.Max(0, offset - start)), substringWidth - glyphWidth);
                        linesCount++;
                        if (linesCount > maxLines)
                            return res;

                        if (ch == ' ')
                        {
                            start = offset + 1;
                            substringWidth = 0;
                        }
                        else
                        {
                            start = offset;
                            substringWidth = glyphWidth;
                        }
                    }
                    else
                    {
                        res.Append(sb.ToString(), lineWidth);

                        // Revert the position to the beginning of the word and reset the line
                        lineIsEmpty = true;
                        offset = start - 1;
                        linesCount++;
                        if (linesCount > maxLines)
                            return res;
                        sb = new StringBuilder();
                        substringWidth = 0;
                    }
                }
            }

            if (start < offset)
            {
                if (!lineIsEmpty)
                    sb.Append(' ');
                sb.Append(_text.Substring(start, offset - start));
            }

            res.Append(sb.ToString(), substringWidth);

            return res;
        }
    }
}