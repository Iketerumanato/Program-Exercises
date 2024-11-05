Shader "Hidden/ScreenShake"
{
    Properties
    {
        _MainTex("Base (RGB)", 2D) = "white" {}
        _ShakeIntensity("Shake Intensity", Range(0, 1)) = 0
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" "Queue" = "Overlay" }
            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

                #include "UnityCG.cginc"

                struct appdata_t
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f
                {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                };

                sampler2D _MainTex;
                float _ShakeIntensity;

                v2f vert(appdata_t v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                half4 frag(v2f i) : SV_Target
                {
                    // 揺れのオフセットを生成
                    float shakeX = _ShakeIntensity * (sin(_Time.y * 10.0) * 0.01);
                    float shakeY = _ShakeIntensity * (cos(_Time.y * 10.0) * 0.01);
                    float2 shakeOffset = float2(shakeX, shakeY);

                    // テクスチャの座標に揺れオフセットを適用
                    return tex2D(_MainTex, i.uv + shakeOffset);
                }
                ENDCG
            }
        }
}