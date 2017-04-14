// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'


Shader "Gemeleon/UnlitTransparent" {
	Properties{
		_Color("Color", Color) = (0.07843138,0.3921569,0.7843137,1)
		[HideInInspector]_Cutoff("Alpha cutoff", Range(0,1)) = 0.5
	}
		SubShader{
			Tags {
				"IgnoreProjector" = "True"
				"Queue" = "Transparent"
				"RenderType" = "Transparent"
			}
			Pass {
				Name "FORWARD"
				Tags {
					"LightMode" = "ForwardBase"
				}
				Blend SrcAlpha OneMinusSrcAlpha
				ZWrite Off

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#define UNITY_PASS_FORWARDBASE
				#include "UnityCG.cginc"
				#pragma multi_compile_fwdbase
				#pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
				#pragma target 3.0
				uniform float4 _Color;
				struct VertexInput {
					float4 vertex : POSITION;
				};
				struct VertexOutput {
					float4 pos : SV_POSITION;
				};
				VertexOutput vert(VertexInput v) {
					VertexOutput o = (VertexOutput)0;
					o.pos = UnityObjectToClipPos(v.vertex);
					return o;
				}
				float4 frag(VertexOutput i) : COLOR {
					////// Lighting:
					////// Emissive:
									float3 emissive = _Color.rgb;
									float3 finalColor = emissive;
									return fixed4(finalColor,_Color.a);
								}
								ENDCG
							}
	}
		FallBack "Diffuse"
									CustomEditor "ShaderForgeMaterialInspector"
}