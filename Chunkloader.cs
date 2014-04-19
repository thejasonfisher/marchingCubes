using UnityEngine;
using System.Collections;

public class Chunkloader : MonoBehaviour
{
	private Chunk[,] activeChunks;
	private const int numChunks = 3;
	private const int seed = 5;
	private float freq = 80.0f;
	private float playerX,playerZ;
	private int cLX = 100,cLZ = 100;
	private bool moveflag = false;
	private Material mat;
	
	public void Start()
	{
		mat = Texturizer.Generate(0.0f, 1.0f, 2.0f, seed);
		activeChunks = new Chunk[numChunks,numChunks];
		for(int y = 0; y < numChunks; y++){
		for(int x = 0; x < numChunks; x++){
			activeChunks[x,y] = new Chunk();
			activeChunks[x,y].Setup(x,y,seed,freq, mat);
			if(activeChunks[x,y].isNew){
				activeChunks[x,y].Generate();
			}
			else
			{
				activeChunks[x,y].Load();
			}
		}}
	}
	
	public void Update()
	{
		playerX = GameObject.Find("Player").transform.position.x;
		playerZ = GameObject.Find("Player").transform.position.z;
		
		if(playerX > cLX + 100){
			cLX += 100;
			moveflag = true;
		}
		else if(playerX < cLX){
			cLX -= 100;
			moveflag = true;
		}
		if(playerZ > cLZ + 100){
			cLZ += 100;
			moveflag = true;
		}
		else if(playerZ < cLZ){
			cLZ -= 100;
			moveflag = true;
		}
		if(moveflag)
		{
			Load(cLX,cLZ);
			moveflag = false;
		}
		
	}
	
	private void Load(int cLX, int cLZ)
	{
		for(int b = 0; b < 3; b++){
		for(int a = 0; a < 3; a++){
			activeChunks[a,b].Unload();
			activeChunks[a,b].Setup((cLX/100)-1+a,(cLZ/100)-1+b,seed,freq, mat);
			if(activeChunks[a,b].isNew){
				activeChunks[a,b].Generate();
			}
			else{
				activeChunks[a,b].Load();
			}
		}}
	}
}
