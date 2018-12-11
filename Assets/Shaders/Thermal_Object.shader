Shader "Custom/Thermal_Object" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		 ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

		Tags { "Queue" = "Transparent"
		"RenderType"="Thermal" 
		"RenderPipeline" = "HDRenderPipeline"}
		LOD 200
		Pass{
		CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag

        #include "UnityCG.cginc"



		sampler2D _MainTex;
		 struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
		 struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		  v2f vert (appdata v)
            {
                v2f o;
               // v.vertex.x += sin(_Time.y  + v.vertex.y * _Amplitude) * _Distance * _Amount;
                o.vertex = UnityObjectToClipPos(v.vertex);
               // o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

	  fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                col.a = 0;
                return col;
          }
		ENDCG
		}
	}
	
}
