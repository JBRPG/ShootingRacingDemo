-------------------------------------------------------------------------------------------------------
DPSpritePalette
v1.2
-------------------------------------------------------------------------------------------------------

Created by Davi Santos (davisan2@gmail.com)
Website: https://davisan.wordpress.com/unity-assets/
Support: http://bit.ly/DaviSantosAssetsSupport


-----
About
-----

DPSpritePalette is a set of scripts that allows palette swapping of pixel art style sprites in run-time!
- Very simple to setup!
- Uses Unity's SpriteRenderer!
- Works with Spritesheets or individual sprites!
- No need to create a new material for each sprite.
- It two or more sprites has the same palette texture, the sprites will be dynamically batched!
- (v1.1) Now it has a palette builder! Just bring your palettes files (.act) from photoshop!


---------
Changelog
---------

v1.2.1:
- Updated the Palette Builder script to be compatible with older versions of Unity.

v1.2:
- Added a surface shader version. So you can use the Unity default lighting! To use it just assign the Material "SpritePaletteLightingMaterial" to the "Palette Material" field of the DPSpritePalette component.

v1.1.1:
- Fixed an exception in the PaletteBuilder


----------
How to Use
----------
	See the tutorial at https://youtu.be/q4kyAjQdqGM

1) Add the componenet DPSpritePalette to Game Object with the SpriteRenderer component. All the sprites used must have the following configuration:
		Disable "Generate Mip Maps"
		Filter Mode set to "Point"
		Format set to "True Color"
2) Assign the paletteTexture field with the palette texture, generated by the PaletteBuilder window.
3) You can chage the current palette index or the palette texture in run-time with the methods:
		SetPaletteIndex(int paletteIndex)
        SetPalette(Texture2D newNexture)

   See the file "Example/TestPalette.cs"


Optional:
4) If you want change the colors in palette texture in run-time, I recomend to pass to the paletteTexture field a copy, then change the colors of the copy.

   See the file "Example/TestPalette2.cs"
   
-----------------------------
Palette Builder
-----------------------------

	Palette Builder is a tool to import the palettes in .act format created in Photoshop to create the Palette Texture.
	See the tutorial at https://youtu.be/q4kyAjQdqGM

-----------------------------
Scheme of the Palette Texture
-----------------------------

    Image size = Number of Paletes x Number of Colors
    The first column must have the same color of the sprite!!
    Each column represents a palette.

    Base Colors | Palette#1                | Palette#2                | ... | Palette#M
    ------------+--------------------------+--------------------------+-----+--------------------------
    Color#1     | Replacement for Color #1 | Replacement for Color #1 | ... | Replacement for Color #1
    Color#2     | Replacement for Color #2 | Replacement for Color #2 | ... | Replacement for Color #2
    Color#3     | Replacement for Color #3 | Replacement for Color #3 | ... | Replacement for Color #3 
    ...         | ...                      | ...                      | ... | ...
    Color#N     | Replacement for Color #N | Replacement for Color #N | ... | Replacement for Color #N
    

--------------------------------------------------------------------
Configuration of the Palette Texture Import Settings in Unity Editor
--------------------------------------------------------------------
    Texture Type          = ADVANCED
    Non Power of 2        = NONE
    Import Type	          = DEFAULT
    Read/Write Enabled    = any value
    Alpha from Greyscale  = unchecked
    Alpha is Transparency = CHECKED
    Bypass sRGB Sampling  = unchecked
    Encode as RGBM        = auto
    Sprite Mode           = none
    Generate Mipmaps      = UNCHECKED
    Wrap Mode             = CLAMP
    Filter Mode           = POINT
    Format                = Automatic True Color
    
 
------------------------------------------------------------
Configuration of the Sprites Import Settings in Unity Editor
------------------------------------------------------------
	Texture Type     = Sprite
	Sprite Mode      = Any mode!
	Generate Mipmaps = UNCHECKED
	Filter Mode      = POINT
	Format           = True Color


----------------------------------------------------
Changing the maximum number os colors in the palette
----------------------------------------------------

By default, the color limit is hardcoded to 32 colors per palette.
You can increase/decrease this value by changing two values:
	1) In "DPSpritePalette.cs", change the variable "MAX_PALETTE_COLORS" (line 12)
	2) In "Shader/DPSpritePaletteShader.shader", change the variable "maxColors" (line 86)


----------
Questions?
----------
Contact Us: davisan2@gmail.com