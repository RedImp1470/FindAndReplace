## Find and replace
A small Unity3D editor extension to easily replace GameObjects.
The tool finds all the objects with a certain name and replaces them with another prefab in your assets.

####ATTENTION:
> The tool replaces all objects whose names contains the name of the original object.<br>
  For example: if you have three objects - car, car (1), car (2) - make sure to choose "car" as the original if you want to replace them all.<br>
> It works only for GameObjects and not for other components.<br>
> This is an extention for Unitys Editor and not thought for replacing GameObjects at runtime.


### How to use it:

1. Open the editor window (Window -> Replace object)
2. Select ONE of the objects you want to replace or drag and drop it from the hierarchy into the first object field.
3. Press "Search!"
4. Select an object (the one you want to use for replacing) in the second object field.
5. Press "Replace!"

>All objects with the same name as the one chosen in the first object field should be replaced by the one you chose in the second field.<br>
>The replacement includes parent-child-relations, rotation and position of the original object.
