Shader "Custom/NonAdditiveStencilExample" {
	Properties{
		// User-Defined properties
		//("name in editor", Type) = Default value

		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		[MaterialToggle] PixelSnap("Pixel snap", Float) = 0
		[HideInInspector] _RendererColor("RendererColor", Color) = (1,1,1,1)
		[HideInInspector] _Flip("Flip", Vector) = (1,1,1,1)
		[PerRendererData] _AlphaTex("External Alpha", 2D) = "white" {}
		[PerRendererData] _EnableExternalAlpha("Enable External Alpha", Float) = 0

		// These are literally variables
		_AVEryCoolVariable("Cool variable", Float) = 0.5
	}

		// This is a list of shaders that will run on the graphics card.
		// Multiple can be defined, but only one will run.
		// The first SubShader that is comaptible with the graphics card will run.
		SubShader{
		   Tags { 
			// Identifying tags used by Unity
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
			"PreviewType" = "Plane"
			"CanUseSpriteAtlas" = "True"
			}

			Cull Off
			Lighting Off
			ZWrite Off // This object won't obsure other objects behind it.
			Blend SrcAlpha OneMinusSrcAlpha // Blend this texture with the objects behind it using this algorithm
			//ColorMask BGA // Determines what color channel is rendered. Default: RGBA


			// A series of shader functions that will run consecutively to 
			// Render an image. This is from vertex -> fragment.
			// Multiple can be defined, and they will all run based on order in 
			// this script.
		   Pass {

			   // Replace a pixel if the shader hasn't written 2 in
			   // the stencil buffer yet. 
			   Stencil {
					// Stencil Test - This determines whether the fragment shader will run or not.
					// Stencil Buffer - A buffer that stores a number for each pixel on screen. Default is 255 for each

				   Ref 2			// Use Comp (Comparision) against this value of a pixel. 
				   Comp NotEqual	// Only run if this pixel does not already have a value of two. Comp(stencilBufferValue & readMask)


					// If the comparison passes, Replace the value of this pixel in the stencil buffer to the reference Value; 2
				   Pass Replace
			   }



				// A shader program that uses the CG Language
				// ; Everything above this line of code was using 
				// the ShaderLab Language.
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag


				// Functions for fragment and vertex shaders
				#include "UnityCG.cginc"

				// Main Texture and Tint
				// This is a variable that corresposends to one of 
				// the properties above. This is due so the CG language code
				// can use the properties defined by ShaderLab.
				// MUST BE THE EXACT CASE TO WORK
				uniform sampler2D _MainTex;

				// sampler2D = texture image
				// fixed / half / float = numbers of increasing precision
				// E.x. fixed4 = a vector with 4 values.


				// THESE STRUCTS CAN HAVE ANY NAME. LITERALLY. 
				struct appdata {
					// Assign Unity-defined input values to a 
					// struct. 
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0; //Texture coors of the current pixel in (x, y)
					half4 color : COLOR;   // Vertex Color
				};

				struct v2f {
					// Assign Unity-defined input values to a 
					// struct. 
					half4 pos : POSITION;
					half2 uv : TEXCOORD0;
					half4 color : COLOR;
				};

				// Vertex Shader
				// Computes the positions of specific vertices in an
				// image. 
				// Vertex -> Fragment
				// 3D -> 2D screen
				v2f vert(appdata v) {
					v2f o;
					o.pos = UnityObjectToClipPos(v.vertex);
					half2 uv = MultiplyUV(UNITY_MATRIX_TEXTURE0, v.uv);
					o.uv = uv;
					o.color = v.color;

					// Spit out v2f. This structure directly feeds into the fragment
					// shader below.
					return o;
				}

				// Fragment Shader; Discard any transparent pixels + tint. 
				// This function outputs the color of a single pixel
				// "Pixel Shader"
				half4 frag(v2f i) : SV_TARGET {

					// distort the UVs
					// _Time is a variable in #include "UnityCG.cginc"
					// _Time is a Vector4 with each having different scales (milli, sec, min, ect.)
					// for convienince
					//i.uv.x += sin((i.uv.x + i.uv.y) * 8 + _Time.y * 1.3) * 0.1;
					//i.uv.y += cos((i.uv.x - i.uv.y) * 8 + _Time.y * 1.3) * 0.1;


					
					// tex2D() looks up the color of a pixel's (R, G, B, A) values 
					// given a texture image and position (uv)
					half4 color = tex2D(_MainTex, i.uv);


					if (color.a <= 0.4f) {
						// Discard a pixel from the texture if it's too transparent
						discard;
					}
					else {
						color.r *= i.color.r;
						color.g *= i.color.g;
						color.b *= i.color.b;
						color.a *= i.color.a;
					}
					return color;
				}
				ENDCG
		   }

	}

		Fallback off
}