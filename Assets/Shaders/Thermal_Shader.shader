// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'


Shader "CameraShaders/Thermal_Shader"
{
	Properties
	{
		_MainTex ("Base (RGB)", 2D) = "transparent" {}
		_amplification ("Amplification", Range(0,10)) = 2
	}
	
	SubShader
	{
		
			Tags{
				"RenderPipeline" = "HDRenderPipeline"
			} 
			
			Pass
			{
				
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				
				#include "UnityCG.cginc"

				sampler2D _MainTex;
				fixed _heatCam;

				fixed _amplification;
				float _cameraNormal;

				struct appdata_t{
					float4 vertex :POSITION;
					float4 color: COLOR;
					float2 texCoord: TEXCOORD0;
					float3 normal: NORMAL;
				};

				struct v2f{
					float4 vertex :SV_POSITION;
					float4 color: COLOR;
					float2 texCoord: TEXCOORD0;
					float2 screenPos: TEXCOORD1;
					
				};

				v2f vert(appdata_t IN){
					v2f OUT;
					OUT.texCoord = IN.texCoord;
					OUT.screenPos = IN.vertex;
					OUT.vertex = UnityObjectToClipPos(IN.vertex);
					OUT.color = IN.color;

					return OUT;
				}

				fixed4 frag (v2f IN) : COLOR{
					half4 c = tex2D(_MainTex, IN.texCoord) * IN.color;

					float4 result = c;

					//Get the intensity of the white light
					float intensity = (c.r + c.g + c.b)/3;
						result.a = c.a;
					
					
						//Change the rgb values to green plus a little white light
						result.rgb  = float3(intensity *_amplification, 0,1/(intensity * _amplification));
						return result;
					
				}


				ENDCG
			}
	}
			SubShader
			{
		 	Cull Off
 			ZTest Always
			ZWrite  Off
			Tags{
				"Queue" = "Transparent"
				"RenderType" = "Thermal"
				"RenderPipeline" = "HDRenderPipeline"
			} 
		
			Pass
			{
				
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				
				#include "UnityCG.cginc"

				sampler2D _MainTex;
				fixed _heatCam;

				fixed _amplification = 1;
				float _cameraNormal;

				struct appdata_t{
					float4 vertex :POSITION;
					float4 color: COLOR;
					float2 texCoord: TEXCOORD0;
					float3 normal: NORMAL;
				};

				struct v2f{
					float4 vertex :SV_POSITION;
					float4 color: COLOR;
					float2 texCoord: TEXCOORD0;
					float2 screenPos: TEXCOORD1;
					
				};
				//taken from https://answers.unity.com/questions/399751/randomity-in-cg-shaders-beginner.html Aily, 2018-11-05
				float rand()
 				{
   					return frac(sin( 12.9898 * 43758.5453)*_Time.y);
 				}


				v2f vert(appdata_t IN){
					v2f OUT;
					float _amplitude = 0.01;
					float _period = 1;
					OUT.texCoord = IN.texCoord*_amplitude*IN.texCoord.x*IN.vertex.y*sin(_Time.y);
					
					OUT.screenPos = IN.vertex;
					OUT.vertex = UnityObjectToClipPos(IN.vertex);
					OUT.color.xyz = (float3(0.5,0,0)*(IN.normal+(float3(1,1,1)*8)));
					

					return OUT;
				}

				fixed4 frag (v2f IN) : COLOR{
					half4 c = tex2D(_MainTex, IN.texCoord) * IN.color;

					float4 result = c;

					//Get the intensity of the white light
					float intensity = (c.r + c.g + c.b)/10;

					
						//Change the rgb values to green plus a little white light
					//result.rgb  = c.rgb;
					
						//result.rgb = float3(c.r+intensity, c.g+intensity,c.b+intensity);
					
						return result;
					
				}


				ENDCG
			}
		
	
		
	}
	Fallback "Specular"
	
}
