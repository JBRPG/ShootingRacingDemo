using UnityEngine;
using System.Collections;

public class TestPalette : MonoBehaviour {

	public Texture2D Palette1;
	public Texture2D Palette2;

	private DPSpritePalette dps;
	void Start () {
		dps = GetComponent<DPSpritePalette>();
	}

	void Update () {
		// Example usages:
		// Prev, Next Palette
		if(Input.GetKeyDown(KeyCode.LeftArrow)) dps.PrevPalette();
		if(Input.GetKeyDown(KeyCode.RightArrow)) dps.NextPalette();

		// Change to a specific palette
		if(Input.GetKeyDown(KeyCode.Alpha1)) dps.SetPaletteIndex(0);
		if(Input.GetKeyDown(KeyCode.Alpha2)) dps.SetPaletteIndex(1);

		// Change the pallette texture
		if(Input.GetKeyDown(KeyCode.Alpha3)) dps.SetPalette(Palette1);
		if(Input.GetKeyDown(KeyCode.Alpha4)) dps.SetPalette(Palette2);
	}


}
