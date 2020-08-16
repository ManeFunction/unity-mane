# unity-mane
Some extensions and features to make your Unity work easier!

From version 1.2.7 finally with **npm packages**!
What means **Unity Package Manager** support with versions and updates!

To add this or any other of my packages to your project paste to **manifest.json**
my packages repository scope registry
```json
  "scopedRegistries": [
    {
      "name": "Mane Function",
      "url": "https://pkgs.dev.azure.com/manefunction/unity-mane/_packaging/unity/npm/registry/",
      "scopes": [
        "com.manefunction"
      ]
    }
  ],
```
and add this project dependency (version can vary)
```json
  "dependencies": {
    "com.manefunction.utils": "1.2.7", 
    
    }
```
**If you are using Unity 2018 or earlier, use my legacy scope:**

https://pkgs.dev.azure.com/manefunction/unity-mane/_packaging/unity-2018/npm/registry/
