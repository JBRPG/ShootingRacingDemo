using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/***********************************************************************
 *  DPSpritePalette
 *  Created by Davi Santos - davisan2@gmail.com
 ***********************************************************************/

[ExecuteInEditMode, RequireComponent(typeof(SpriteRenderer))]
public class DPSpritePalette : MonoBehaviour 
{	
	/// <summary>
	/// The maximum number of colors for a palette.
	/// If you want to increase this value, you must change the variable "maxColors" (line 86) in the shader! Beware! This will may decrease the performance.
	/// </summary>
	private const int MAX_PALETTE_COLORS = 32;

	
	/// <summary>
	/// The palette material. It should be the Material "Assets/DPSpritePalette/SpritePaletteMaterial.mat"
	/// </summary>
	public Material paletteMaterial;
	
	/// <summary>
	/// The color. Use this color instead the one from Sprite Renderer
	/// </summary>
	public Color color = Color.white;

	/// <summary>
	/// The texture with the palettes!
	/// The format must be:
	/// 	Texture Type          = ADVANCED
	/// 	Non Power of 2        = NONE
	/// 	Import Type	          = DEFAULT
	/// 	Read/Write Enabled	  = any value
	/// 	Alpha from Greyscale  = unchecked
	/// 	Alpha is Transparency = CHECKED
	/// 	Bypass sRGB Sampling  = unchecked
	/// 	Encode as RGBM        = auto
	///     Sprite Mode           = none
	/// 	Generate Mipmaps      = UNCHECKED
	/// 	Wrap Mode             = CLAMP
	/// 	Filter Mode		      = POINT
	///     Format                = Automatic True Color
	/// </summary>
	public Texture2D paletteTexture;

	/// <summary>
	/// The index of the current palette.
	/// </summary>
	public int CurrentPaletteIndex = 0;
	
	private SpriteRenderer sp;
	private int _currentIndex = 0;
	private Vector4 _texturePixelSize = new Vector4();
	private Texture2D _lastpaletteTexture = null;
	private MaterialPropertyBlock block;
	//private bool dirtyFlag = true;


	//---------------------------------------------------------------------------------------

	/// <summary>
	/// Gets the number palettes.
	/// </summary>
	/// <returns>The number palettes.</returns>
	public int GetNumPalettes() { return paletteTexture == null ? 1 : paletteTexture.width; }

	/// <summary>
	/// Gets the number colors of the palette
	/// </summary>
	/// <returns>The number colors.</returns>
	public int GetNumColors() { return paletteTexture == null ? 1 : paletteTexture.height; }

	/// <summary>
	/// Gets or sets the index of the palette.
	/// </summary>
	/// <value>The index of the palette.</value>
	public int PaletteIndex {
		get { return _currentIndex; }
		set { this.SetPaletteIndex(value); }
	}
	
	/// <summary>
	/// Sets the palette texture.
	/// </summary>
	/// <param name="newTexture">Palette Texture. Check the readme for the correct format.</param>
	/// <param name="resetPaletteIndex">If set to <c>true</c> the palette index will be reset to 0.</param>
	public void SetPalette(Texture2D newTexture, bool resetPaletteIndex = true)
	{
		if(newTexture.height > MAX_PALETTE_COLORS) Debug.LogWarning("The palette has more than 32 colors! You need to change de shader if you want more colors!!");
		paletteTexture = newTexture;
		_lastpaletteTexture = paletteTexture;
		CalcPixelSize();

		if(resetPaletteIndex) SetPaletteIndex(0);
		//dirtyFlag = true;
	}

	/// <summary>
	/// Sets the index of the palette.
	/// </summary>
	/// <param name="index">Index.</param>
	public void SetPaletteIndex(int index) 
	{
		if(paletteTexture == null) return;

		if(index >= paletteTexture.width) { 
			Debug.LogError("Palette index out of range!");
			return;
		}
		_currentIndex = index;
		CurrentPaletteIndex = index;

		//dirtyFlag = true;
	}

	/// <summary>
	/// Change to the previous palette.
	/// </summary>
	public void PrevPalette() {
		if(_currentIndex == 0) _currentIndex = GetNumPalettes()-1;
		else {
			_currentIndex -= 1;
		}
		PaletteIndex = _currentIndex;
	}

	/// <summary>
	/// Change to the next palette.
	/// </summary>
	public void NextPalette() {
		_currentIndex = (_currentIndex + 1) % GetNumPalettes();
		PaletteIndex = _currentIndex;
	}

	void CalcPixelSize ()
	{
		_texturePixelSize = new Vector4 (1.0f / paletteTexture.width, 1.0f / paletteTexture.height, paletteTexture.width, paletteTexture.height);
	}

	//---------------------------------------------------------------------------------------
	
	void Awake()
	{
		block = new MaterialPropertyBlock ();
		
		sp = GetComponent<SpriteRenderer>();
				
		if(paletteTexture == null) {
			Debug.Log("Palette Texture not assigned!!\nAssign the Palette Texture");
			return;
		}
		SetPaletteIndex(CurrentPaletteIndex);
		CalcPixelSize ();
	}
	void Start()
	{
		sp.sharedMaterial = paletteMaterial;
		//dirtyFlag = true;
	}

	void OnRenderObject() {
		if(Application.isPlaying) return;
		if(paletteTexture == null) return;

		if(_lastpaletteTexture != paletteTexture) {
			CalcPixelSize();
			_lastpaletteTexture = paletteTexture;
		}

		//if(!dirtyFlag) return;
		MaterialPropertyBlock block = new MaterialPropertyBlock();
		block.Clear();
		sp.GetPropertyBlock(block);
		block.SetTexture("_PaletteTex", paletteTexture);
		block.SetVector("_PaletteTextureSize", _texturePixelSize);
		sp.SetPropertyBlock(block);

		float index = (_currentIndex+0.5f)/paletteTexture.width;
		sp.color = new Color(color.r, map(color.g, color.b), index, color.a);

		//dirtyFlag = false;
	}
	
	void LateUpdate()
	{	
		if(!Application.isPlaying) return; 
		if(paletteTexture == null) return;

		if(_lastpaletteTexture != paletteTexture) {
			CalcPixelSize();
			_lastpaletteTexture = paletteTexture;
		}

		//if(!dirtyFlag) return;
		block.Clear();
		sp.GetPropertyBlock(block);
		block.SetTexture("_PaletteTex", paletteTexture);
		block.SetVector("_PaletteTextureSize", _texturePixelSize);
		sp.SetPropertyBlock(block);

		float index = (_currentIndex+0.5f)/paletteTexture.width;
		sp.color = new Color(color.r, map(color.g, color.b), index, color.a);


		//dirtyFlag = false;
	}

	
	float map(float a, float b) {
		a = Mathf.Round(a*15);
		b = Mathf.Round(b*15);
		return (a * 16 + b) / 256.0f;
	}
	
	Vector2 unmap(float val) {
		Vector2 res = new Vector2();
		val *= 256.0f;
		res.x =  val / 16;
		res.y =  (val% 16);
		res.x /= 15.0f;
		res.y /= 15.0f;
		return res;
	}
}

