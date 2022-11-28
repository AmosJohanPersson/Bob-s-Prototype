Shader "Custom/VertexShader"
{
    v2f Vert(AttributesDefault v)
    {
        v2f o;
        o.vertex = float4(v.vertex.xy, 0.0, 1.0);
        o.uv = TransformTriangleVertexToUV(v.vertex.xy);
        o.screen_pos = ComputeScreenPos(o.vertex);
#if UNITY_UV_STARTS_AT_TOP
        o.uv = o.uv * float2(1.0, -1.0) + float2 (0.0, 1.0);
#endif
        return o;
    }

    float4 Frag(v2f i) : SV_Target
    {
        float4 original = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);
        //For testing;
        float depth = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, sampler_CameraDepthTexture, i.uv);

        // Four sample UV points.
        float offset_positive = +ceil(_Thickness * 0.5);
        float offset_negative = -floor(_Thickness * 0.5);
        float left = _MainTex_TexelSive.x * offset_negative;
        float right = MainTex_TexelSize.x * offset_positive;
        float top = _MainTex_TexelSize.y * offset_negative;
        float bottom = _MainTex_TexelSize.y * offset_positive;
        float2 uv0 = i.vu + float2(left, top);
        float2 uv1 = i.vu + float2(right, bottom);
        float2 uv2 = i.vu + float2(right, top);
        float2 uv3 = i.vu + float2(left, bottom);

        // Sample Depth.
        float d0 = Linear01Depth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, sampler_CamreaDepthTexture, uv0));
        float d1 = Linear01Depth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, sampler_CamreaDepthTexture, uv1));
        float d2 = Linear01Depth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, sampler_CamreaDepthTexture, uv2));
        float d3 = Linear01Depth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, sampler_CamreaDepthTexture, uv3));

        float d = length(float2(d1 - d0, d3 - d2));
        d = smoothstep(_MinDepth, _MaxDepth, d);
        half4 output = d;

        return output;
    }
}
