Find and Replace GameObjects
============================

"Find and Replace" is a small level editing tool that allows you to easily replace objects in your scenes hierarchy.
The tool finds all the objects with a certain name and replaces them with another prefab in our assets.

ATTENTION:
> The tool will check for all objects whose name contains the name of the original object.
  For example: if you have three objects - car, car (1), car (2) - make sure to choose "car" as the original if you want to replace them all.
> It will only work for GameObjects and not for other components.
> This is a extention for Unitys Editor and not thought for replacing GameObjects at runtime.


How to use it:
===============

1. Open the editor window (Window -> Replace object)
2. Select ONE of the objects you want to replace or drag and drop it from the hierarchy into the first object field.
3. Press "Search!"
4. Select a object (the one you want to use for replacing) in the second object field.
5. Press "Replace!"

--> All objects with the same name as the one chosen in the first object field should be replaced by the one you chose in the second field
--> The replacement includes parent-child-relations, rotation and position of the original object.

