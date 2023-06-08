namespace Malv.Models;

public class CarAd_Mod
{
    public int Age { get; set; }
    
    public int Mileage { get; set; }
    
    public TransmissionType_Mod TransmissionType { get; set; }
    
    public FuelType_Mod FuelType { get; set; }
    
    public CarType_Mod CarType { get; set; }
}