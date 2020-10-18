Shader "Custom/NonAdditive" {
	Properties{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		[MaterialToggle] PixelSnap("Pixel snap", Float) = 0
		[HideInInspector] _RendererColor("RendererColor", Color) = (1,1,1,1)
		[HideInInspector] _Flip("Flip", Vector) = (1,1,1,1)
		[PerRendererData] _AlphaTex("External Alpha", 2D) = "white" {}
		[PerRendererData] _EnableExternalAlpha("Enable External Alpha", Float) = 0
	}

		SubShader{
		   Tags { 
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
			"PreviewType" = "Plane"
			"CanUseSpriteAtlas" = "True"
			}

			Cull Off
			Lighting Off
			ZWrite Off
			Blend One OneMinusSrcAlpha

		   Pass {

			   // Replace a pixel if the shader hasn't written 2 in
			   // the stencil buffer yet. 
			   Stencil {
				   Ref 2
				   Comp NotEqual
				   Pass Replace
			   }

				Blend SrcAlpha OneMinusSrcAlpha

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"

				// Main Texture and Tint
				uniform sampler2D _MainTex;

				struct appdata {
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
					half4 color : COLOR;
				};

				struct v2f {
					half4 pos : POSITION;
					half2 uv : TEXCOORD0;
					half4 color : COLOR;
				};

				// Vertex Shader
				v2f vert(appdata v) {
					v2f o;
					o.pos = UnityObjectToClipPos(v.vertex);
					half2 uv = MultiplyUV(UNITY_MATRIX_TEXTURE0, v.uv);
					o.uv = uv;
					o.color = v.color;
					return o;
				}

				// Fragment Shader; Discard any transparent pixels + tint. 
				half4 frag(v2f i) : SV_TARGET {
					half4 color = tex2D(_MainTex, i.uv);
					if (color.a <= 0.4f) {
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