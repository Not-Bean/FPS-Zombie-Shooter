Shader "Custom/Ceiling" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _Rotation ("Rotation (degrees)", Float) = 0
        _Scale ("Scale", Float) = 1
    }
    
    SubShader {
        Tags { "RenderType"="Opaque" }
        
        CGPROGRAM
        #pragma surface surf Lambert
        
        sampler2D _MainTex;
        float _Rotation;
        float _Scale;
        
        struct Input {
            float2 uv_MainTex;
        };
        
        float2 rotateUV(float2 uv, float rotation) {
            float rad = rotation * 3.14159265359 / 180;
            float s = sin(rad);
            float c = cos(rad);
            float2 center = float2(0.5, 0.5);
            uv -= center;
            uv = float2(uv.x * c - uv.y * s, uv.x * s + uv.y * c);
            uv += center;
            return uv;
        }
        
        void surf (Input IN, inout SurfaceOutput o) {
            // Apply rotation and scaling
            float2 uv = rotateUV(IN.uv_MainTex, _Rotation);
            uv = (uv - 0.5) * _Scale + 0.5;
            
            // Sample texture
            fixed4 c = tex2D(_MainTex, uv);
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}