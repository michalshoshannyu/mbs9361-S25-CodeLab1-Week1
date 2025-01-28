Shader "Custom/SpongeShader"
{
    Properties
    {
        _BaseColor ("Base Color", Color) = (1, 0.9, 0.5, 1) // Sponge-like yellow color
        _Porosity ("Porosity Amount", Float) = 0.5         // Strength of holes
        _HoleSize ("Hole Size", Float) = 1.0               // Size of sponge pores
        _DisplacementStrength ("Displacement Strength", Float) = 0.1 // Strength of displacement effect
        _NoiseScale ("Noise Scale", Float) = 1.0           // Noise texture scale
        _MainTex ("Texture", 2D) = "white" {}              // Optional main texture
        _BumpMap ("Bump Map", 2D) = "bump" {}              // Optional bump map (for better depth)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Pass
        {
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
            float4 _BaseColor;
            float _Porosity;
            float _HoleSize;
            float _DisplacementStrength;
            float _NoiseScale;
            sampler2D _MainTex;
            sampler2D _BumpMap;

            // Noise generation function (simplified)
            float rand(float2 co)
            {
                return frac(sin(dot(co.xy, float2(12.9898,78.233))) * 43758.5453);
            }

            // Vertex Shader: Displace the vertex positions based on noise
            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex); // Convert vertex position to clip space
                o.uv = v.uv * _NoiseScale; // Apply noise scale for UV
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz; // World position of the vertex

                // Generate noise for displacement based on UV coordinates
                float noise = rand(o.uv * _HoleSize); // Generate noise based on UV
                float porous = step(_Porosity, noise); // Create holes (porous areas) based on porosity
                float displacement = lerp(0.0, _DisplacementStrength, porous); // Displace based on noise

                // Displace the Y-axis of the vertices (up/down displacement)
                v.vertex.y += displacement;

                // Output the final position in clip space
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            // Fragment Shader: Sample the texture and apply base color
            fixed4 frag(v2f i) : SV_Target
            {
                // Sample the texture if available
                fixed4 texColor = tex2D(_MainTex, i.uv);
                fixed4 finalColor = texColor * _BaseColor; // Apply base color to the texture color

                return finalColor;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
