Shader "Custom/TransparentTint"
{
	Properties
	{
		_MainTex("Sprite Texture", 2D) = "white" {}
	}
		SubShader
	{
		Tags {
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
			"PreviewType" = "Plane"
			"CanUseSpriteAtlas" = "True"
		}

		Blend DstColor Zero

		Pass
		{
			Stencil {
				Ref 2
				Comp NotEqual
				Pass Replace
			}


			CGPROGRAM
			#pragma vertex vertexShader
			#pragma fragment fragmentShader
			#include "UnityCG.cginc"

			sampler2D _MainTex; // _MainTex from above


			struct appdata
			{
				// Vertex Shader Input
				float4 vertex : POSITION;			// Position of vertex
				float2 uv : TEXCOORD0;				// Position of vertex on texture
				half4 color : COLOR;				// Color of that vertex (Modified from texture via renderer color)
			};

			struct v2f
			{
				// Vertex Shader Output / Fragment Input
				half4 pos : POSITION;				// Position of rasterized pixel
				half2 uv : TEXCOORD0;				// Position of pixel on texture
				half4 color : COLOR;				// Color of the pixel on texture (Modified from texture via renderer color)
			};

			v2f vertexShader(appdata v)
			{
				// Copy data to v2f. Rasterize vertex positions to screen too.
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				half2 uv = MultiplyUV(UNITY_MATRIX_TEXTURE0, v.uv);
				o.uv = uv;
				o.color = v.color;

				return o;
			}

			fixed4 fragmentShader(v2f i) : SV_Target
			{
				// Modify position of fragment. 
				//i.uv.x += sin((i.uv.x + i.uv.y) * 8 + _Time.y * 1.3) * 0.1;
				//i.uv.y += cos((i.uv.x - i.uv.y) * 8 + _Time.y * 1.3) * 0.1;

				// Sample the texture for original color
				half4 color = tex2D(_MainTex, i.uv);

				if (color.a <= 0.4f) {
					// Discard a pixel from the texture if it's too transparent
					discard;
				}
				else {
					// Multiply color of texture with that of the renderer.
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
}
