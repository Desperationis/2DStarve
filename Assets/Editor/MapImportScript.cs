using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using SuperTiled2Unity;

#if UNITY_EDITOR
using SuperTiled2Unity.Editor;
public class MapImportScript : CustomTmxImporter
{
    public override void TmxAssetImported(TmxAssetImportedArgs args)
    {
        // Simply set the layer to background
        SuperMap map = args.ImportedSuperMap;
        TilemapRenderer renderer = map.GetComponentInChildren<TilemapRenderer>();
        renderer.sortingLayerName = "Background";
    }
}
#endif
