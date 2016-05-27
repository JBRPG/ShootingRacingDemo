Shader "DP Shaders/Sprite Palette"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		[PerRendererData] _PaletteTex ("Palette Texture", 2D) = "white" {}
		[PerRendererData] _PaletteTextureSize ("Palette Texture Size", Vector) = (0,0,0,0)
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha

		Pass
		{
		CGPROGRAM
			#pragma target 3.0
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile _ PIXELSNAP_ON
			#include "UnityCG.cginc"
			
			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				half2 texcoord  : TEXCOORD0;
				float index     : TEXCOORD1;
			};
			
			fixed4 _Color;
			
			float2 unmap(float val) {
				float2 res;
				val *= 256.0f;
				res.x = floor(val / 16);
				res.y = fmod(val, 16);
				res /= 15.0f;
				return res;
			}
			
			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = mul(UNITY_MATRIX_MVP, IN.vertex);
				OUT.texcoord = IN.texcoord;
				float2 colorgb = unmap(IN.color.g);
				float4 INcolor = float4(IN.color.r, colorgb.x, colorgb.y, IN.color.a);
				OUT.color = INcolor * _Color;
				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap (OUT.vertex);
				#endif
				OUT.index = IN.color.b;
				
				return OUT;
			}

			sampler2D _MainTex;
			sampler2D _PaletteTex;
			float4 _PaletteTextureSize;

			fixed4 frag(v2f IN) : SV_Target
			{
				half4 source = tex2D(_MainTex, IN.texcoord);
				int maxColors = 32;
				
				if(source.a < 1.0/255.0) return fixed4(0,0,0,0);
				
				half4 final = source;
				half indexYFinal = 0;
				half _PalleteIndex = IN.index.x;
				half halfTextelSizeX = _PaletteTextureSize.x * 0.5f;
				fixed found = 0;
				
				for(int i = 0 ; i < maxColors; i++) {
					half indexY = (i + 0.5f) / _PaletteTextureSize.w;
					half4 corD = tex2D(_PaletteTex, float2(halfTextelSizeX, indexY));
					half4 diff = source-corD;
					half dist = dot(diff, diff);
					if(dist <= 0.000016f) {	// <--- change the precision here
						indexYFinal = indexY;
						found = 1;
						break;
					}
				}
				final = (found > 0)? tex2D(_PaletteTex, half2(_PalleteIndex, indexYFinal)) : final;
				final *= IN.color;
				final.rgb *= final.a;
				return final;
			}
		ENDCG
		}
	}
}
