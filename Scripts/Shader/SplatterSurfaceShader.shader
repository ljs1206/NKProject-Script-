Shader "Splatter/Surface"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		_AlphaCutoff("Alpha Cutoff", Range(0.01, 1.0)) = 0.01
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

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha

		Pass
		{
			Stencil
			{
				Ref 5
				Comp Always
				Pass Replace
			}

		HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

			struct Attributes
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct Varyings
			{
				float4 vertex   : SV_POSITION;
				half4 color    : COLOR;
				half2 texcoord  : TEXCOORD0;
			};

			half4 _Color;
			half _AlphaCutoff;

			Varyings vert(Attributes IN)
			{
				Varyings OUT;
				OUT.vertex = TransformObjectToHClip(IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * _Color;

				return OUT;
			}

			sampler2D _MainTex;
			sampler2D _AlphaTex;
			float _AlphaSplitEnabled;

			half4 SampleSpriteTexture (float2 uv)
			{
				half4 color = tex2D (_MainTex, uv);
				if (_AlphaSplitEnabled)
					color.a = tex2D (_AlphaTex, uv).r;

				return color;
			}

			half4 frag(Varyings IN) : SV_Target
			{
				half4 c = SampleSpriteTexture (IN.texcoord) * IN.color;
				c.rgb *= c.a;

				// Discard pixels below cutoff so that stencil is only updated for visible pixels.
				clip(c.a - _AlphaCutoff);

				return c;
			}
		ENDHLSL
		}
	}
}