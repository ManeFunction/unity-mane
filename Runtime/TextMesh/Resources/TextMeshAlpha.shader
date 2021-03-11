// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "TextMesh/Alpha" 
{
	Properties
	{
		_MainTex("Base (RGB) Trans (A)", 2D) = "white" {}
		_AlphaColor("Alpha", Range(0.0, 1.0)) = 1.0
	}

	SubShader
	{
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		ZWrite Off Lighting Off Cull Off Fog{ Mode Off } Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM

#pragma vertex vert
#pragma fragment frag
#pragma fragmentoption ARB_precision_hint_fastest

#include "UnityCG.cginc"

			uniform float _AlphaColor;
			sampler2D _MainTex;
			float4 _MainTex_ST;

			struct attribute_t
			{
				float4 vertex : POSITION;
				fixed4 color : COLOR;
				float2 uv : TEXCOORD0;
			};

			struct varying_t
			{
				float4 vertex : SV_POSITION;
				fixed4 color : COLOR;
				float2 uv : TEXCOORD0;
			};

			varying_t vert(attribute_t v)
			{
				varying_t o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.color = v.color;
				return o;
			}

			fixed4 frag(varying_t i) : COLOR
			{
				i.color.a *= tex2D(_MainTex, i.uv).a * _AlphaColor;
				return i.color;
			}

			ENDCG
		}
	}
}