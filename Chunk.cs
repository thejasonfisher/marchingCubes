using UnityEngine;
using System.Collections;

public class Chunk 
{
	private int x,y,seed;
	private float freq;
	private readonly int num = 5;
	private	VolMesh[,,] meshes;
	private Vector3 local, global;
	public bool isNew = true;
	
	public void Setup(int x_, int y_, int seed_, float freq_, Material mat)
	{
		x = x_;
		y = y_;
		seed = seed_;
		freq = freq_;
		meshes = new VolMesh[num,num,num];
		global = new Vector3(x*100,0,y*100);
		for(int k = 0; k < num; k++){
		for(int j = 0; j < num; j++){
		for(int i = 0; i < num; i++){
			local = new Vector3(i*20,j*20,k*20);
			local += global;
			meshes[i,j,k] = new VolMesh();
			meshes[i,j,k].Initialize(seed,freq,local, mat);
		}}}
		if(!meshes[0,0,0].isNew){
			isNew = false;
		}
		else
		{
			isNew = true;
		}
	}
	public void Generate()
	{
		for(int k = 0; k < num; k++){
		for(int j = 0; j < num; j++){
		for(int i = 0; i < num; i++){
			meshes[i,j,k].Build();
		}}}
	}
	
	public void Load()
	{
		for(int k = 0; k < num; k++){
		for(int j = 0; j < num; j++){
		for(int i = 0; i < num; i++){
			meshes[i,j,k].Load();
		}}}
	}
	
	public void Unload()
	{
		for(int k = 0; k < num; k++){
		for(int j = 0; j < num; j++){
		for(int i = 0; i < num; i++){
			meshes[i,j,k].Delete();
		}}}
	}


}
