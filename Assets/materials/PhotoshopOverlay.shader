// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/UI/PhotoshopOverlay"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		
		_StencilComp ("Stencil Comparison", Float) = 8
		_Stencil ("Stencil ID", Float) = 0
		_StencilOp ("Stencil Operation", Float) = 0
		_StencilWriteMask ("Stencil Write Mask", Float) = 255
		_StencilReadMask ("Stencil Read Mask", Float) = 255

		_ColorMask ("Color Mask", Float) = 15

		[Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}
		
		Stencil
		{
			Ref [_Stencil]
			Comp [_StencilComp]
			Pass [_StencilOp] 
			ReadMask [_StencilReadMask]
			WriteMask [_StencilWriteMask]
		}

		Cull Off
		Lighting Off
		ZWrite Off
		ZTest [unity_GUIZTestMode]
		Blend SrcAlpha OneMinusSrcAlpha
		ColorMask [_ColorMask]

		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
			#include "UnityUI.cginc"

			#pragma multi_compile __ UNITY_UI_ALPHACLIP
			
			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				half2 texcoord  : TEXCOORD0;
				float4 worldPosition : TEXCOORD1;
			};
			
			fixed4 _Color;
			fixed4 _TextureSampleAdd;
			float4 _ClipRect;
			sampler2D _MainTex;

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.worldPosition = IN.vertex;
				OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);

				OUT.texcoord = IN.texcoord;
				
				#ifdef UNITY_HALF_TEXEL_OFFSET
				OUT.vertex.xy += (_ScreenParams.zw-1.0)*float2(-1,1);
				#endif
				
				OUT.color = IN.color * _Color;
				return OUT;
			}

			float when_lt(fixed4 x, fixed4 y)
			{
				return max(sign(y - x), 0.0);
			}

			fixed4 frag(v2f IN) : COLOR
			{
				// Get the raw texture value
				float4 texColor = tex2D(_MainTex, IN.texcoord);

				// Calculate the twiceLuminance of the texture color
				float twiceLuminance = dot(texColor, fixed4(0.2126, 0.7152, 0.0722, 0)) * 2;

				// Determine the start/end ranges for Lerping based on the luminance
				fixed4 useOverlay = when_lt(1, twiceLuminance);
				fixed4 startColor = (fixed4(0, 0, 0, 0) * (1 - useOverlay)) + (IN.color * useOverlay);
				fixed4 endColor = (IN.color * (1 - useOverlay)) + (fixed4(1, 1, 1, 1) * useOverlay);

				fixed4 output = lerp(startColor, endColor, twiceLuminance-useOverlay);
				output.a = texColor.a * IN.color.a * UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);

				#ifdef UNITY_UI_ALPHACLIP
					clip (output.a - 0.001);
				#endif

				return output;
			}
		ENDCG
		}
	}
}
