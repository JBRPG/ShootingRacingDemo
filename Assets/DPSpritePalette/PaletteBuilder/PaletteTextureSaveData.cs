using UnityEngine;
using System.Collections;
using System;

/***********************************************************************
 *  DPSpritePalette
 *  Created by Davi Santos - davisan2@gmail.com
 ***********************************************************************/

[Serializable]
public class PaletteTextureSaveData : ScriptableObject 
{
	public string PaletteName;
	public TextAsset BasePalette;
	public TextAsset[] Palettes;

}
