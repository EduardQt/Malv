
import { TransmissionType_Mod } from './TransmissionType_Mod';
import { FuelType_Mod } from './FuelType_Mod';
import { CarType_Mod } from './CarType_Mod';

export interface CarAd_Mod {
    age: number;
    mileage: number;
    transmissionType: TransmissionType_Mod;
    fuelType: FuelType_Mod;
    carType: CarType_Mod;
}