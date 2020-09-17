1.3.3:
    - Added Random.FlipCoin() and Random.ThrowDice(d) methods for fast events randomization.

1.3.2:
    - Added RandomBetween extension for Vector2 and Vector2Int.

1.3.1:
    - Added SelfConcat collections extension that allows to concatenate collection w/ itself multiple times. 

1.3.0:
    - Added ToVectorN float extensions to ease vectors init with the same components.

1.2.9:
    - Fixed issue with updating NPM package through the UPM system.

1.2.8:
    - Added Postfix attribute (Property Drawer).

1.2.7:
    - Added npm packages.

1.2.6:
    - Renamed breakable ForEach() collection extension to ForEachCancellable() to prevent unnecessary calls 
        instead of plain ForEach() when action is an expression with single assignment 
        (what actually returns true for successful operation). 
    - Also reversed it to false!
    - CHANGELOG.md now contains full changelog, not only the last version. 
    
1.2.5: 
    - Vector2/3.Average() doesn't return NaN vectors anymore.
    
1.2.4:
    - Added option to IEnumerable.GetRandom() to allows getting extended lists with duplicates.
    
1.2.3:
    - Some improvements in Property Drawers.
    
1.2.2:
    - Added DropdownList attribute (Property Drawer). You can use with string or int values.
    
1.2.1:
    - Added fully-functional Layer attribute (Property Drawer).
    
1.2.0:
    - Added Average() extension for the Vectors collections.
    - Removed all C# 7.0+ expressions to make the code compatible with earlier version
        (full 7.0 translation will be applied later with Unity 2018 support drop) 
      
1.1.1:
    - Added Sphere Gizmo to help you position objects without graphics.
    
1.1.0:
    - Added ReadOnly attribute (Property Drawer) for inspector properties.
    
1.0.17:
    - Close polygon automatically in Vector3.IsInPolygon() extension without input points duplicating.
    
1.0.16:
    - Added Vector3.ClosestPointOnLine() extension.
    
1.0.15:
    - Added Vector3.IsInPolygon() extension to check is point inside convex polygon or not.
    
1.0.14:
    - Added float.Map() extension to remap value from one range to another.
    
1.0.13:
    - Added Gizmos class with Plane gizmo.
    
1.0.12:
    - Added Vector2.AddZ() extension, returning new Vector3.
    
1.0.11:
    - Added string Extension to Reverse() text.
    
1.0.10:
    - Added simplified ForeEach() extension loop for IEnumerable (w/o break).
    
1.0.9:
    - Fixed rotation issue within Transform.Reset() extension.
    
1.0.8:
    - Added Clear() method for arrays, that fills array with default values.
    
1.0.7:
    - Added Duplicate() method for GameObject itself, not only the Component.
    
1.0.6:
    - Added separate Translate() methods for Vector extensions.
    
1.0.5:
    - Added Duplicate() method for Unity Components (similar to Cmd+D but for scripts).
    
1.0.4:
    - Added RandomOrDefault<T>() method for IEnumerable<T>.
    
1.0.3:
    - Added GetRandom() and GetIndexOf() extensions for collection types.
        GetRandom() also includes variation for getting random sub-arrays without duplicates.
    - Added legacy support for Unity 2018.x and earlier (different branch in repo)
        
1.0.2:
    - You can count this as an INIT version.
    - Rearrange project depends on Unity guidelines.
    
1.0.1:
    - Basically the test version increase to check Package Manager things.
    
1.0.0:
    - Init repo + some stuff.