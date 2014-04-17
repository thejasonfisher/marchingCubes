using UnityEngine;
using System.Collections;


public class Colorer 
{
		public static Color Generate(float x, float y,float freq, float colorBase, float colorVariance,int seed)
		{
			Color color;
			
			float index,bright,red,green,blue;
			
			PerlinNoise myPerlin = new PerlinNoise(seed);
			
			index = colorBase + myPerlin.FractalNoise2D(x,y,3, freq, colorVariance);

			while(index > 6.0f){
				index -= 6.0f;
			}
			while(index < 0.0f){
				index += 6.0f;
			}
			
			bright = 0.5f + myPerlin.FractalNoise2D(x,y,3, freq, 0.5f);
			
			while(bright > 1.0f){
				bright -= 0.01f;
			}
			while(bright < 0.0f){
				bright += 0.01f;
			}
			
			red=0.0f;
			green=0.0f;
			blue=0.0f;
			
			if(index >= 2.0f && index < 4.0f){
				 red = 0.0f;
			}			
			else if(index >= 0.0f && index < 1.0f){
				 red = bright;
			}
			else if(index >= 1.0f && index < 2.0f){
				 red = bright * (2.0f - index);
			}
			else if(index >= 4.0f && index < 5.0f){
				 red = bright * (index - 4.0f);
			}
			else if(index >= 5.0f && index <= 6.0f){
				 red = bright;
			}
			
			if(index >= 4.0f && index < 6.0f){
				 green = 0.0f;
			}	
			else if(index >= 1.0f && index < 3.0f){
				 green = bright;
			}
			else if(index >= 3.0f && index < 4.0f){
				 green = bright * (4.0f - index);
			}			
			else if(index >= 0.0f && index < 1.0f){
				 green = bright * index;
			}

			if(index >= 0.0f && index < 2.0f){
				 blue = 0.0f;
			}
			else if(index >= 3.0f && index < 5.0f){
				 blue = bright;
			}
			else if(index >= 2.0f && index < 3.0f){
				 blue = bright * (index - 2.0f);
			}
			else if(index >= 5.0f && index <= 6.0f){
				 blue = bright * (6.0f - index);
			}
			
			color = new Vector4(red,green,blue);
		
			return color;
		}
	}
