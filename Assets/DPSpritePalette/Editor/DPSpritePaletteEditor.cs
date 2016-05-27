using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(DPSpritePalette))]
public class DPSpritePaletteEditor : Editor {

	DPSpritePalette dsp;
	public void OnEnable()
	{
		dsp = (DPSpritePalette)target;
	}

	public override void OnInspectorGUI()
	{
		GUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel(new GUIContent("Palette Material","The palette material. It should be the Material /Assets/DPSpritePalette/SpritePaletteMaterial.mat"));
		dsp.paletteMaterial = (Material)EditorGUILayout.ObjectField(dsp.paletteMaterial, typeof(Material), false);
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel(new GUIContent("Color", "Color Tint\nThis color overrides the SpriteRenderer's color."));
		dsp.color = EditorGUILayout.ColorField(dsp.color);
		GUILayout.EndHorizontal();

		EditorGUILayout.Separator();

		GUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel(new GUIContent("Palette Texture", "The texture with the palettes!\nCheck the readme.txt for the format.\n" +
		                                           "Texture Type = ADVANCED\n" +
		                                           "Non Power of 2 = NONE\n" +
		                                           "Import Type = DEFAULT\n" +
		                                           "Read/Write Enabled = any value\n" +
		                                           "Alpha from Greyscale = unchecked\n" +
		                                           "Alpha is Transparency = CHECKED\n" +
		                                           "Bypass sRGB Sampling  = unchecked\n" +
		                                           "Encode as RGBM = auto\n" +
		                                           "Sprite Mode = none\n" +
		                                           "Generate Mipmaps = UNCHECKED\n" +
		                                           "Wrap Mode = CLAMP\n" +
		                                           "Filter Mode = POINT\n" +
		                                           "Format = Automatic True Color\n"));
		dsp.paletteTexture = (Texture2D) EditorGUILayout.ObjectField(dsp.paletteTexture, typeof(Texture2D), false);
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();	
		EditorGUILayout.PrefixLabel("Palette Index");
		dsp.CurrentPaletteIndex = EditorGUILayout.IntField(dsp.CurrentPaletteIndex);
		if(dsp.paletteTexture == null) 
			dsp.CurrentPaletteIndex = 0;
		else
			dsp.CurrentPaletteIndex = Mathf.Clamp(dsp.CurrentPaletteIndex ,0, dsp.paletteTexture.width-1);
		dsp.PaletteIndex = dsp.CurrentPaletteIndex;
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		if(GUILayout.Button("Previous Index")) {
			dsp.PrevPalette();
		}
		if(GUILayout.Button("Next Index")) {
			dsp.NextPalette();
		}
		GUILayout.EndHorizontal();

		SceneView.RepaintAll();
	}

}
