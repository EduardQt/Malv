namespace Malv.MobileDe;

public class FinalBrand
{
    public string Name { get; set; }
    public FinalModels Models { get; set; }
}

public class FinalModels
{
    public IList<FinalModel> Data { get; set; }
}

public class FinalModel
{
    public int Value { get; set; }
    public string Label { get; set; }
    public string OptgroupLabel { get; set; }
    public bool Isgroup { get; set; }
    public IList<FinalModel> Items { get; set; }
}