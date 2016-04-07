using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof(Generator))]
public class GeneratorEditor : Editor {

    public bool autoUpdate;
    public override void OnInspectorGUI()
    {
        Generator gen = (Generator)target;
        
        if(DrawDefaultInspector())
        {
          gen.GenerateMap();
        }

        if(GUILayout.Button("Force ReDraw"))
        {
           // gen.seed = Random.Range(int.MinValue, int.MaxValue);
            gen.GenerateMap();

        }

        if (GUILayout.Button("New Seed"))
        {
            gen.seed = Random.Range(int.MinValue, int.MaxValue);
            gen.GenerateMap();

        }


        // base.OnInspectorGUI();
    }

}
