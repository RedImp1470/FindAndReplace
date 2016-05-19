## Find and replace
A small Unity3D editor extension to easily replace GameObjects and Materials.

####Take note:
> The tool replaces all GameObjectsObjects whose names contains the name of the original object.<br>
  For example: if you have three objects - car, car (1), car (2) - make sure to choose "car" as the original if you want to replace them all.<br>
> Materials: The material is replaced on ALL objects whose material equals the one you chose in the first object field.<br>
> GameObjects: The replacement includes parent-child-relations, rotation and position of the original object.<br>
> It works only for GameObjects and Materials and not for other components.<br>
> This is an extention for Unitys Editor and not thought for replacing stuff at runtime.<br>


### How to use it:

1. Open the editor window (Window -> Find and Replace)
2. Choose whether you want to replace a GameObject or a Material
3. Select ONE of the objects you want to replace or drag and drop it from the hierarchy into the first object field, OR select the material you want to replace.
4. Press "Search!"
5. Select an object OR a material (the one you want to use for replacing) in the second object field.
6. Press "Replace!"



