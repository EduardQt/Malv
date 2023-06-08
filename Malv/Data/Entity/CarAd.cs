using System.ComponentModel.DataAnnotations;

namespace Malv.Data.EF.Entity;

public class CarAd
{
    [Key]
    public int Id { get; set; }
    
    public int AdId { get; set; }
    
    public int Age { get; set; }
    
    public int Mileage { get; set; }
    
    public TransmissionType TransmissionType { get; set; }
    
    public FuelType FuelType { get; set; }
    
    public CarType CarType { get; set; }
    
    public Ad Ad { get; set; }
}

public enum TransmissionType
{
    Manual,
    Automatic
}

public enum FuelType
{
    Petrol,
    Diesel,
    Hybrid
}

public enum CarType
{
    Small,
    Sedan,
    Hatchback,
    Combi,
    Coupe,
    Cab,
    Suv,
    FamilyBus,
    Transport
}