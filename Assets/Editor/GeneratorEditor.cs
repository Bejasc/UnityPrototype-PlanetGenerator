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
          gen.DrawMap();
        }

        if(GUILayout.Button("Force ReDraw"))
        {
           // gen.seed = Random.Range(int.MinValue, int.MaxValue);
            gen.DrawMap();

        }

        if (GUILayout.Button("New Seed"))
        {
            gen.seed = Random.Range(int.MinValue, int.MaxValue);
            gen.DrawMap();

        }


        // base.OnInspectorGUI();
    }

}
