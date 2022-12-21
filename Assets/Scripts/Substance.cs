
public class Substance
{
    public float Mass;
    public Material Material;
    public Aggregation Aggregation;

    public Substance(float mass, Material material, Aggregation aggregation)
    {
        Mass = mass;
        Material = material;
        Aggregation = aggregation;
    }

    public void AddMass(float m)
    {
        Mass += m;
    }
}
