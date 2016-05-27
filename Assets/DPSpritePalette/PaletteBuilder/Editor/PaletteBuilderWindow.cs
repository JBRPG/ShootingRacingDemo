using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;

/***********************************************************************
 *  DPSpritePalette
 *  Created by Davi Santos - davisan2@gmail.com
 ***********************************************************************/

public class PaletteBuilderWindow : EditorWindow {

	[MenuItem ("Window/DP Sprite Palette/Palette Builder")]
	static void CreatePBWindow () 
	{
		PaletteBuilderWindow window = (PaletteBuilderWindow) EditorWindow.GetWindow(typeof(PaletteBuilderWindow), true, "Palette Builder");
		window.Show();
	}

	string PaletteName = "";
	TextAsset BasePalette = null;
	TextAsset[] Palettes;
	Vector2 scrollPosition;
	
	void OnGUI () 
	{

		GUILayout.BeginHorizontal();
		if(GUILayout.Button("Save Palette Definition")) 
		{
			SavePaletteData();
		}
		if(GUILayout.Button("Load Palette Definition")) 
		{
			LoadPaletteData();
		}
		GUILayout.EndHorizontal();

		GUILayout.Label ("Settings", EditorStyles.boldLabel);
		PaletteName = EditorGUILayout.TextField ("Palette Texture Name:", PaletteName);


		GUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel(new GUIContent("Base Palette (.act):","The Base Color palette."));
		BasePalette = (TextAsset) EditorGUILayout.ObjectField(BasePalette, typeof(TextAsset), false);
		GUILayout.EndHorizontal();

		scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

		if(Palettes == null) Palettes = new TextAsset[64];

		for(int i=0; i < Palettes.Length; ++i) 
		{
			GUILayout.BeginHorizontal();
			EditorGUILayout.PrefixLabel(new GUIContent("Palette " + (i+1) + " (.act):",""));
			Palettes[i] = (TextAsset) EditorGUILayout.ObjectField(Palettes[i], typeof(TextAsset), false);
			GUILayout.EndHorizontal();
		}
		EditorGUILayout.EndScrollView();

		if(GUILayout.Button("Create Palette Texture!")) 
		{
			if(Palettes == null) { EditorUtility.DisplayDialog("Error!", "Unspected error!", "Close"); return; } 
			if(PaletteName == null || string.IsNullOrEmpty(PaletteName)) { EditorUtility.DisplayDialog("Error!", "The Palette Texture Name is not assigned!", "Close"); return; }
			if(BasePalette == null) { EditorUtility.DisplayDialog("Error!", "The Base Palette is not assigned!", "Close"); return; } 
			if(Palettes != null && Palettes[0] == null) { EditorUtility.DisplayDialog("Error!", "Must have at last one palette assigned and no skips between the palettes!", "Close"); return; } 
	
			bool PaletteIsOk = true;
			bool foundNull = false;
			int PosSkipped = 0;
			for(int i =0; i < Palettes.Length; ++i) {
				if(foundNull && Palettes[i] != null) PaletteIsOk = false;
				if(Palettes[i] == null && !foundNull) { foundNull = true; PosSkipped = i+1; }
			}
			if(!PaletteIsOk){ EditorUtility.DisplayDialog("Error!", "You Skipped the Palette #" + PosSkipped + "!", "Close"); return; } 


			EditorApplication.delayCall += CreatePalette;
		}
	
	}

	void CreatePalette()
	{
		int retVal = CreatePaletteTexture();
		if(retVal == -1) { EditorUtility.DisplayDialog("Error!", "Error while reading the Base Palette! The format is not recognized!", "Close"); return; }
		if(retVal > 0) { EditorUtility.DisplayDialog("Error!", "Error while reading the Palette" + retVal+ "! The format is not recognized!", "Close"); return; }
		if(retVal < -10) { EditorUtility.DisplayDialog("Error!", "The Palette " + (-retVal-10)+ " has a different number of colors!", "Close"); return; }
	}


	int CreatePaletteTexture()
	{
		Color32[] basePal = ReadACTPalette(BasePalette);
		if(basePal == null) return -1;

		List<Color32[]> palColors = new List<Color32[]>();

		for(int i =0; i < Palettes.Length; ++i) {
			Color32[] colors = ReadACTPalette(Palettes[i]);
			if(Palettes[i] != null && colors == null) return i +1;
			if(Palettes[i] != null && colors != null) palColors.Add(colors);
		}

		// check sizes
		int basePalSize = basePal.Length;
		for(int i =0; i < palColors.Count; ++i) {
			int size = palColors[i].Length;
			if(size != basePalSize) return -(i+1) -10;
		}

		// all done, now just create the texture!
		int numCols = basePalSize;
		int numPals = palColors.Count+1;
		Texture2D tex = new Texture2D(numPals, numCols, TextureFormat.RGBA32, false);
		tex.filterMode = FilterMode.Point;
		tex.wrapMode = TextureWrapMode.Clamp;

		// copy the base colors
		for(int j=0; j < numCols; ++j) {
			tex.SetPixel(0,j, basePal[numCols-1-j]);
		}

		// copy the palettes
		for(int i=1; i < numPals; ++i) {
			for(int j=0; j < numCols; ++j) {
				tex.SetPixel(i,j, palColors[i-1][numCols-1-j]);
			}
		}

		byte[] pngData = tex.EncodeToPNG();

		string path = "Assets";
		foreach (UnityEngine.Object obj in Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets))
		{
			path = AssetDatabase.GetAssetPath(obj);
			if (File.Exists(path))
			{
				path = Path.GetDirectoryName(path);
			}
			break;
		}

		string saveDir = EditorUtility.SaveFilePanel("Save Palette Texture", path, PaletteName + ".paltex.png", "paltex.png");

		if(pngData != null && !string.IsNullOrEmpty(saveDir)) File.WriteAllBytes(saveDir, pngData);
		DestroyImmediate(tex);
		AssetDatabase.Refresh();

		return 0;
	}

	Color32[] ReadACTPalette(TextAsset asset)
	{
		if(asset == null) return null;

		int transparentColorIndex = -1;

		byte[] bytes = asset.bytes;
		int numberOfColors = 0;
		if(bytes.Length == 768) { 
			numberOfColors = 256;
		}
		else if(bytes.Length == 772) { 
			numberOfColors = asset.bytes[asset.bytes.Length-3];
			transparentColorIndex = asset.bytes[asset.bytes.Length-1];
			if(numberOfColors == 0) numberOfColors = 256;
		}
		else {
			return null;
		}

		Color32[] colors = new Color32[numberOfColors];
		for(int i =0; i < numberOfColors; ++i) {
			if(i == transparentColorIndex) continue;
			colors[i] = new Color32(bytes[3*i], bytes[3*i+1], bytes[3*i+2], 255);
		}
		return colors;
	}


	void SavePaletteData()
	{
		string path = "Assets";
		foreach (UnityEngine.Object obj in Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets))
		{
			path = AssetDatabase.GetAssetPath(obj);
			if (File.Exists(path))
			{
				path = Path.GetDirectoryName(path);
			}
			break;
		}
		string saveDir = EditorUtility.SaveFilePanel("Save Palette Texture Data", path, "", "datapal.asset");
		saveDir = saveDir.Replace(Application.dataPath, "Assets");

		if(!string.IsNullOrEmpty(saveDir)) {
			//PaletteTextureSaveData paletteData = PaletteBuilderWindow.CreateAsset<PaletteTextureSaveData>(saveDir);
			PaletteTextureSaveData paletteData = ScriptableObject.CreateInstance<PaletteTextureSaveData>();

			paletteData.PaletteName = PaletteName;
			paletteData.BasePalette = BasePalette;
			paletteData.Palettes = new TextAsset[Palettes.Length];
			for(int i=0; i < Palettes.Length; ++i)
			{
				paletteData.Palettes[i] = Palettes[i];
			}

			AssetDatabase.CreateAsset (paletteData, saveDir);
			AssetDatabase.SaveAssets ();
		}

	}
	void LoadPaletteData()
	{
		string path = "Assets";
		foreach (UnityEngine.Object obj in Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets))
		{
			path = AssetDatabase.GetAssetPath(obj);
			if (File.Exists(path))
			{
				path = Path.GetDirectoryName(path);
			}
			break;
		}
		string loadDir = EditorUtility.OpenFilePanel("Open Palette Texture Data", path, "datapal.asset");
		loadDir = loadDir.Replace(Application.dataPath, "Assets");

		if(!string.IsNullOrEmpty(loadDir)) {
			//PaletteTextureSaveData paletteData = AssetDatabase.LoadAssetAtPath<PaletteTextureSaveData>(loadDir);
			// compatibility update for older versions of Unity
			PaletteTextureSaveData paletteData = AssetDatabase.LoadAssetAtPath(loadDir, typeof(PaletteTextureSaveData)) as PaletteTextureSaveData;

			PaletteName = paletteData.PaletteName;
			BasePalette = paletteData.BasePalette;
			Palettes = new TextAsset[paletteData.Palettes.Length];
			for(int i=0; i < paletteData.Palettes.Length; ++i)
			{
				Palettes[i] = paletteData.Palettes[i];
			}
		}
	}	
}
