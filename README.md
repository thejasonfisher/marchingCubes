marchingCubes
=============

C# source files for Unity that generate a nearly-infinite terrain of volumetric meshes.


All C# (.cs) files belong in a folder called Plugins EXCEPT for the Chunkloader.cs file, which belongs in an empty game object in your scene.

The ideal usage is for there to exist a directional light, and either a prefab character controller or scripted camera in your scene.

The character controller prefab or camera must be named "Player" in order for the Chunkloader to recognize it and update.

One may use flycam.js on a camera for an easy flying view.

The first use may cause Unity to become unresponsive for a couple of minutes due to many meshes being built and stored on your machine for the first time.
