using UnityEngine;
using System.Collections;

public static class PlanetTypes {

    public static PlanetBase GetPlanetType(Generator.TypePlanets planet)
    {
        switch(planet)
        {
            case Generator.TypePlanets.Earth:
                return EarthLikePlanet();

        }

        return null;
    }

    public static PlanetBase EarthLikePlanet()
    {
        PlanetBase planet = new PlanetBase();

        planet.Name = "Earthlike";

        planet.regions = new TerrainType[7];

        planet.regions[0] = new TerrainType { name = "DeepOcean", height = 0.35f, color = new Color32(18, 28, 64, 255) };
        planet.regions[1] = new TerrainType { name = "Ocean", height = 0.45f, color = new Color32(29, 39, 75, 255) };
        planet.regions[2] = new TerrainType { name = "ShallowOcean", height = 0.55f, color = new Color32(34, 44, 79, 255) };
        planet.regions[3] = new TerrainType { name = "Shore", height = 0.6f, color = new Color32(103, 92, 46, 255) };
        planet.regions[4] = new TerrainType { name = "Grass", height = 0.75f, color = new Color32(52, 69, 49, 255) };
        planet.regions[5] = new TerrainType { name = "Mountain", height = 0.85f, color = new Color32(79, 79, 79, 255) };
        planet.regions[6] = new TerrainType { name = "MountainTop", height = 1f, color = new Color32(92, 92, 92, 255) };


        return planet;
    }
}
