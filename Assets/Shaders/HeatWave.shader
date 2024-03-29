﻿Shader "Custom/Distort"
{
	Properties
	{
	[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
	_NoiseTex("Texture (R,G=X,Y Distortion; B=Mask; A=Unused)", 2D) = "white" {}
	_Intensity("Intensity", Float) = 0.1
	_Color("Tint", Color) = (1,1,1,1)
		_Speed("Speed", Float) = 3
	}
		SubShader
	{
		// Draw ourselves after all opaque geometry
		Tags{ "Queue" = "Overlay" }

		// Grab the screen behind the object into _BackgroundTexture
		GrabPass
	{
		"_BackgroundTexture"
	}

		Cull Off
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha
		AlphaTest Off
		// Render the object with the texture generated above, and invert the colors
		Pass
	{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"

	struct v2f
	{
		float4 grabPos : TEXCOORD0;
		float4 pos : SV_POSITION;
	};

	sampler2D _BackgroundTexture;
	sampler2D _NoiseTex;
	float _Intensity;
	float4 _NoiseTex_ST;
	float _Speed;

	v2f vert(appdata_base v) {
		v2f o;
		// use UnityObjectToClipPos from UnityCG.cginc to calculate 
		// the clip-space of the vertex
		o.pos = UnityObjectToClipPos(v.vertex);
		// use ComputeGrabScreenPos function from UnityCG.cginc
		// to get the correct texture coordinate
		o.grabPos = ComputeGrabScreenPos(o.pos);

		float noise = tex2Dlod(_NoiseTex, o.grabPos).r;
		o.grabPos.x += cos(noise * _Time.y * _Speed) * _Intensity / 1000;
		o.grabPos.y += sin(noise * _Time.y * _Speed) * _Intensity / 1000;

		return o;
	}


	half4 frag(v2f i) : SV_Target
	{
		half4 noise = tex2D(_NoiseTex, i.grabPos);
		float4 p = i.grabPos;

		half4 bgcolor = tex2Dproj(_BackgroundTexture, p);



		return bgcolor;
	}
		ENDCG
	}

	}
}