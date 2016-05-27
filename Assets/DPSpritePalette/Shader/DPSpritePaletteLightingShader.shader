Shader "DP Shaders/Sprite Palette Lighting"
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

		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Lambert vertex:vert nofog keepalpha
		#pragma multi_compile _ PIXELSNAP_ON

		sampler2D _MainTex;
		fixed4 _Color;
		sampler2D _AlphaTex;
		float _AlphaSplitEnabled;
		sampler2D _PaletteTex;
		float4 _PaletteTextureSize;
		
		float2 unmap(float val) {
				float2 res;
				val *= 256.0f;
				res.x = floor(val / 16);
				res.y = fmod(val, 16);
				res /= 15.0f;
				return res;
			}


		struct Input
		{
			float2 uv_MainTex;
			fixed4 color;
			float index;
		};
		
		void vert (inout appdata_full v, out Input o)
		{
			#if defined(PIXELSNAP_ON)
			v.vertex = UnityPixelSnap (v.vertex);
			#endif
			
			UNITY_INITIALIZE_OUTPUT(Input, o);			
			
			float2 colorgb = unmap(v.color.g);
			float4 INcolor = float4(v.color.r, colorgb.x, colorgb.y, v.color.a);
			o.color = INcolor * _Color;
			o.index = v.color.b;
		}

		fixed4 SampleSpriteTexture (float2 uv, float index)
		{
			half4 source = tex2D(_MainTex, uv);
			int maxColors = 32;	
			if(source.a < 1.0/255.0) return fixed4(0,0,0,0);
				
			half4 final = source;
			half indexYFinal = 0;
			half _PalleteIndex = index.x;
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

#if UNITY_TEXTURE_ALPHASPLIT_ALLOWED
			if (_AlphaSplitEnabled)
				final.a = tex2D (_AlphaTex, uv).r;
#endif //UNITY_TEXTURE_ALPHASPLIT_ALLOWED

			return final;
		}

		void surf (Input IN, inout SurfaceOutput o)
		{
			fixed4 c = SampleSpriteTexture (IN.uv_MainTex, IN.index) * IN.color;
			o.Albedo = c.rgb * c.a;
			o.Alpha = c.a;
		}
		ENDCG
	}

Fallback "Transparent/VertexLit"
}
