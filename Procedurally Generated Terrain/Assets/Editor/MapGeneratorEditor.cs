using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// for button to show up in editor
[CustomEditor (typeof (MapGenerator))]
public class MapGeneratorEditor : Editor
{
    // overriding editor method
    public override void OnInspectorGUI()
    {
        // reference to map generator, cast to MapGenerator
        MapGenerator mapGen = (MapGenerator)target;
        // draw default inspector
        if(DrawDefaultInspector()) {
            if(mapGen.autoUpdate) {
                mapGen.DrawMapInEditor();
            }
        }
        // generate map on button press
        if (GUILayout.Button("Generate")) {
            mapGen.DrawMapInEditor();
        }
    }
}
