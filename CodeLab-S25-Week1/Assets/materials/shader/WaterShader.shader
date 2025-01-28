Shader "Custom/WaterShader"
{
    Properties
    {
        _Color ("Water Color", Color) = (0, 0.5, 1, 0.5)
        _WaveSpeed ("Wave Speed", Float) = 1.0
        _WaveStrength ("Wave Strength", Float) = 0.1
        _MainTex ("Water Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 200

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Back
            Lighting Off
            ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
            };

            // Properties
            float4 _Color;
            float _WaveSpeed;
            float _WaveStrength;
            sampler2D _MainTex;

            // Vertex Shader
            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;

                // Animate the vertex position for waves
                float wave = sin(v.vertex.x * 0.1 + _Time.y * _WaveSpeed) *
                             cos(v.vertex.z * 0.1 + _Time.y * _WaveSpeed);
                o.pos.y += wave * _WaveStrength;

                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            // Fragment Shader
            fixed4 frag (v2f i) : SV_Target
            {
                // Apply wave distortion to UVs
                float wave = sin(i.worldPos.x * 0.1 + _Time.y * _WaveSpeed) *
                             cos(i.worldPos.z * 0.1 + _Time.y * _WaveSpeed);
                float2 distortedUV = i.uv + wave * _WaveStrength;

                // Sample the texture
                fixed4 texColor = tex2D(_MainTex, distortedUV);

                // Blend with water color
                fixed4 finalColor = texColor * _Color;
                finalColor.a = _Color.a; // Maintain transparency

                return finalColor;
            }
            ENDCG
        }
    }
    FallBack "Transparent/Diffuse"
    
}
