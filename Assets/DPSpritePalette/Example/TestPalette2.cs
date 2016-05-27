using UnityEngine;
using System.Collections;

public class TestPalette2 : MonoBehaviour {

	public Texture2D originalTexture;
	Texture2D copyTexture;
	DPSpritePalette dps;

	void Start () 
	{
		copyTexture = CopyTexture (originalTexture);
		dps = GetComponent<DPSpritePalette>();
		dps.SetPalette(copyTexture);
		// change to the palette index 1, the Base Palette (index 0) must not be changed!!
		dps.SetPaletteIndex(1);

		InvokeRepeating("UpdatePalette", 1.0f, 1.0f);
	}

	Texture2D CopyTexture (Texture2D source)
	{
		Texture2D copy = new Texture2D (source.width, source.height, source.format, false);
		copy.filterMode = FilterMode.Point;
		copy.SetPixels32 (source.GetPixels32 ());
		copy.Apply ();
		return copy;
	}

	void UpdatePalette()
	{
		for(int i=0; i < dps.GetNumColors(); ++i) {
			// sets each color of Pallete #1  to a random value
			copyTexture.SetPixel(1, i, new Color(Random.value, Random.value, Random.value, 1.0f));
		}
		copyTexture.Apply();
	}
}
