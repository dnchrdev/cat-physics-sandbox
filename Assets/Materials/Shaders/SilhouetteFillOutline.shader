Shader "Custom/SilhouetteFillOutline_Clean_URP"
{
    Properties
    {
        _MainTex ("Pattern (RGBA)", 2D) = "white" {}
        _FillColor ("Fill Color", Color) = (1,1,1,1)

        _PatternScale ("Pattern Scale (repeats)", Float) = 5
        _PatternOffsetX ("Pattern Offset X", Float) = 0
        _PatternOffsetY ("Pattern Offset Y", Float) = 0

        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
        _OutlineWidth ("Outline Width (world units)", Float) = 0.02

        [Enum(UnityEngine.Rendering.CompareFunction)] _ZTest ("ZTest", Float) = 4
    }

    SubShader
    {
        Tags
        {
            "RenderType"="Transparent"
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderPipeline"="UniversalPipeline"
        }

        // Note: the original BIRP shader had a "DepthOnly" pass here that
        // existed only to support a commented-out Outline pass. It was
        // removed because two separately compiled passes writing/testing
        // depth against the same mesh are not guaranteed bit-identical
        // (no `invariant` qualifier), which made SilhouetteFill fail its
        // ZTest and rendered the whole object invisible.

        Pass
        {
            Name "SilhouetteFill"
            Tags { "LightMode"="UniversalForward" }
            Cull Back
            ZWrite Off
            ZTest [_ZTest]
            Blend SrcAlpha OneMinusSrcAlpha

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            CBUFFER_START(UnityPerMaterial)
                half4 _FillColor;
                float _PatternScale;
                float _PatternOffsetX;
                float _PatternOffsetY;
            CBUFFER_END

            struct appdata { float4 vertex : POSITION; };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float4 screenPos : TEXCOORD0;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = TransformObjectToHClip(v.vertex.xyz);
                o.screenPos = ComputeScreenPos(o.pos);
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                float2 screenUV = i.screenPos.xy / i.screenPos.w;
                screenUV.x *= _ScreenParams.x / _ScreenParams.y;

                float2 patternUV = screenUV * _PatternScale + float2(_PatternOffsetX, _PatternOffsetY);
                half4 pat = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, patternUV);

                half4 col;
                col.rgb = _FillColor.rgb;
                col.a = pat.a * _FillColor.a;
                return col;
            }
            ENDHLSL
        }
    }

    FallBack Off
}
