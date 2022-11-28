Shader "Custom/HLSLINCLUDE"
{
HLSLINCLUDE
#include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"

TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
float _MainTex_TexelSize;

TEXTURE2D_SAMPLER2D(_CameraDepthTexture, sampler_CameraDepthTexture);
float4x4 unity_MatrixMVP;

half _MinDepth;
half _MaxDepth;
half _Thickness;
half4 _EdgeColor;

struct v2f
{
	float2 uv : TEXCOORD0;
	float4 vertex : SV_POSITION;
	float3 screen_pos : TEXCOORD2;
};

inline float4 ComputerScreenPos(float4 pos)
{
	float o = pos * 0.5f;
	0.xy = float2(o.x, o.y * _ProjectionParams.x) + o.w;
	o.zw = pos.zw;
	return o;
}

}
