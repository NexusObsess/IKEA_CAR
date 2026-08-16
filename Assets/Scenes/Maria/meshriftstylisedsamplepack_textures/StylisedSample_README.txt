Stylised Sample Pack
Version: v1.0
MeshRift Studios Ltd

Overview:

Stylised Sample Pack is a collection of PBR materials designed for game development, environment art dressing and other CG rendering formats. All materials are optimised and compatible with major engines.
- Optimised for real-time rendering
- Consistent and cohesive naming conventions
- Seamless tiling
- PBR accurate roughness and metallic values
- High-Quality Normal maps

License:
Stylised Sample Pack © 2026 by MeshRift Studios Ltd is licensed under CC BY 4.0. To view a copy of this license, visit https://creativecommons.org/licenses/by/4.0/

Allowed:
	- Use in commercial and non-commercial projects
	- Modify textures & materials
	- Use in video games, CG animations, film and other interactive media
Not Allowed:
	- Re-sell or redistribute the pack as is
	- Include materials in another asset pack without permission
	- Use for Generative AI

Contents:

Total Materials: 6
Total Textures: 19
Resolution: 4k
Textures File Format: TGA
Unreal Demo Project: .uproject
Maps Included:
	Base Colour
	Normal (DirectX)
	ORM (Occlusion, Roughness, Metallic)
	Height / Displacement


Engine Compatibility:

These materials are compatible with most common renderers including:
	Unreal Engine
	Unity
	Godot
	Blender (Cycles / Eevee)
	Substance Painter
	Substance Designer
	Maya (Arnold)
	Marmoset Renderer


Folder Structure:
Look at attached contents file


Installation & Setup:

Unreal:
(From FAB)
- Open the Epic Games Launcher and go to your Library
- Find the FAB plugin and Install to Engine for your specific engine version
- Launch the Unreal Project you want to install the assets to
- In Unreal, go to Window > FAB or click the FAB button found in the content drawer toolbar next to the Add (+) button
- Sign into your Epic Games account
- Navigate to your Library and find [Pack Title]
- Click ‘Add to Project’
- The pack should automatically download to its own folder at the root Content folder with no overwrites.
- All material instances are filed and named accordingly with a visual layout provided in the ‘LVL_PackTitle_Overview’ level. Drag the material instances on the meshes in your scene to apply and open the instance viewer to change around some standard supplied parameters.

(From Textures ZIP)
- Drag in to import the textures you require to your Content Browser (1 material requires a Base Colour, Normal and ORM map as well as any addition maps like Height or Emission)
- Open the ORM texture map and turn off the ‘sRGB’ option to prevent artifacting when rendering
- If you haven’t got a master material set up you can use the simple one supplied with the .uproject overview level. If not, create a new material and name it accordingly before opening up the graph view.
- Drag the textures into the graph and connect them to their corresponding channels:
	- Base Colour - drag the white pin into the Base Colour output channel
	- Normal map - drag the white pin into the Normal output channel
	- ORM map - drag the R channel (red pin) into the Ambient Occlusion output, the G channel (green pin) into the Roughness output and the B channel (blue pin) into the 	Metallic output channel.
	- Height map - drag the red pin into the Displacement output channel (to enable Nanite Tesselation, turn on ‘Enable Tesselation’ in the Material Properties panel and 	ensure that the mesh attached to the new material has Nanite enabled)
- For more optimisation: MeshRift recommends changing the sampler type for each Texture Sample to ‘Shared:Wrap’ for a more optimised loading approaching
- For higher quality: MeshRift recommends switching on ‘High Quality Reflections’ 
- For more optimization: Convert the texture samplers to parameters and create Material Instances from the graph to efficiently switch textures for each material.

Unity:
(From UnityPackage)
- Open the ‘Package Manager’ window in your Unity project
- Sign in with the same Unity ID you purchased our pack with
- Navigate to the pack in your library and add to your project
- Multiple material instances can be found in the folder structure with appropriate naming conventions. All of them are parented to a custom MeshRift Shader Graph which utilises the supplied ORM map (as opposed to the standard Lit Shader).
- A supplied overview level displays each material with corresponding labels. Make sure that TextMeshPro (TMP) is installed to properly view the level.

(From Textures ZIP)
- Drag to import the textures
	- Normal: Click to open the inspector and change ‘Texture Type’ to ‘Normal Map’ and flip the green channel
	- ORM: Click to open the inspector and turn off ‘sRGB’
	- Height: Click to open the inspector and turn off ‘sRGB’
- Create a shader graph using Lit URP as a base to make a master material that will use packed maps
- Open up the shader graph and create three Texture2D parameters by pressing the + button at the top right. Name each accordingly for each texture
- Create a ‘Sample Texture 2D’ node for each map and drag each Texture2D parameter into the SampleTexture2D’s ‘Texture(T2) input
- For the Normal Map, change the ‘Type’ of the SampleTexture2D from ‘Default’ to ‘Normal’
- Wire each RGBA output to the corresponding channel in the fragment node group
	- Base Colour RGBA output to Base Color
	- Normal RGBA output to Normal
	- ORM, R channel AmbientOcclusion, G channel to ‘OneMinus’ node and then into Smoothness, B channel to the Metallic Channel
	- Emission RGBA output to Emission
- It’s up to you how you want to use the Height channel, you can plug it into a Parallax Occlusion Mapping node and use the resulting Parallax UVs for each other map for a slight offset in engine.

Blender (Cycles):
(From Textures ZIP)
- Create a new shader graph and name it accordingly
- Drag the textures for the material into the shader graph
	- Normal Map: Set the ‘Color Space’ to ‘Non-Color’
	- ORM Map: Set the ‘Color Space’ to ‘Non-Color’
	- Height Map: Set the ‘Color Space’ to ‘Non-Color’
- Connect the pins up to the corresponding input channels in the Principled BSDF node:
	- Base Colour: drag the colour pin into the Base Colour input
	- Normal: drag the colour pin to a ‘Normal Map’ node’s colour input
	- ORM: drag the colour pin into a ‘Separate Color’ node:
	- Drag the R channel pin into a multiply node combined with the Base Colour node or add an Ambient Occlusion node setup and drag into the ‘Color’ input
	- Drag the G channel pin into the roughness input
	- Drag the B channel pin into the metallic input
	- Height: drag the colour pin into a ‘Displacement’ node’s Height input and drag the output into the purple Displacement input on the final ‘Material Output’ node. 	Note: the mesh that has the material attached must have a subdivision surface modifier for the Height map to displace the mesh

Godot:
(From Textures ZIP)
- Create a folder for the textures in the Godot FileSystem browser and drag the textures into that folder
	- Normal Maps: Select > Go to the ‘Import’ window > Select ‘Normal Map Invert Y’
	- ORM Maps: Select > Go to the ‘Import’ window > Set Channel Pack to ‘Optimized’
	- Height Maps: Select > Go to the ‘Import’ window > Set Channel Pack to ‘Optimized’
- Create a material folder and make a new resource. Select ORM Material from the provided list and name it accordingly
- Add the maps to the corresponding channels:
	- Base Colour Map to Base Colour
	- ORM Map to ORM
	- Normal Map to Normal (enable by ticking the box)
	- Height Map to Height (enable by ticking the box)

Substance Painter:
- In your Painter project, drag the assets into the Library panel to import them
- On the window pop-up, select all the textures to ‘Texture’ and import
- In a fill or paint layer, assign each map to each enabled channel
	- Base Colour map to Base Colour
	- Normal map to Normal
	- For ORM maps, use a Grayscale Conversion generator to isolate each required channel. Put the R channel into Ambient Occlusion, the G channel into the Roughness and 	B channel into the Metallic.
	- Height map to Height
			--



Support & Contact:
For any inquires, updates or support:
Email: contact@meshriftstudios.com
Website: www.meshriftstudios.com
Storefront Pages: www.meshriftstudios.com/storefronts 

