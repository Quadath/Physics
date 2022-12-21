public class Material
{
    public readonly string Name;
    public readonly float Density;
    public readonly Aggregation Aggregation;

    public Material(string name, float density)
    { 
        Name = name;
        Density = density;
        // Aggregation = aggregation;
    }
}
