using UnityEditor;
using UnityEngine;
using System.IO;

/***********************************************************************
 *  DPSpritePalette
 *  Created by Davi Santos - davisan2@gmail.com
 ***********************************************************************/

public class PaletteBuilderImporter : AssetPostprocessor  {

	public static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
	{
		foreach (string asset in importedAssets)
		{
			// Process each act file and creates a copy with .bytes extension to use with unity as a text asset in binary mode.
			if (asset.EndsWith(".act"))
			{
				string filePath = asset.Substring(0, asset.Length - Path.GetFileName(asset).Length) + "/";
				string newFileName = filePath + Path.GetFileNameWithoutExtension(asset) + ".act.bytes";
				
				if (!Directory.Exists(filePath))
				{
					Directory.CreateDirectory(filePath);
				}

				System.IO.File.Copy(asset, newFileName, true);
				Debug.Log("Processed the Palette file: " + asset);
				
				AssetDatabase.Refresh(ImportAssetOptions.Default);
			}
		}
	}

	void OnPreprocessTexture()
	{
		if(assetPath.ToLower().Contains(".paltex"))
		{
			TextureImporter tex = (TextureImporter)assetImporter;
			tex.textureType = TextureImporterType.Advanced;
			tex.npotScale = TextureImporterNPOTScale.None;
			tex.spriteImportMode = SpriteImportMode.None;
			tex.mipmapEnabled = false;
			tex.wrapMode = TextureWrapMode.Clamp;
			tex.filterMode = FilterMode.Point;
			tex.textureFormat = TextureImporterFormat.RGBA32;
		}
	}
	
}
