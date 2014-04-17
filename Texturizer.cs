using UnityEngine;
using System.Collections;

	public class Texturizer
	{	
		public static Material Generate(float Base1,float Variance,float freq,int seed)
		{
			const int texSize = 256;
			Texture2D text = new Texture2D(texSize,texSize,TextureFormat.RGB24,true);
			Color aColor;

			for(int y = 0; y < text.height;++y){
				for(int x = 0; x < text.width;++x){
					aColor = Colorer.Generate(x,y,freq,Base1,Variance,seed);
					text.SetPixel(x,y, aColor);
				}}
				
			text.Apply();
			Material mat = new Material(Resources.Load<Material>("Diffuse"));
			
			mat.mainTexture = text;
			
			return mat;
		}
	}
