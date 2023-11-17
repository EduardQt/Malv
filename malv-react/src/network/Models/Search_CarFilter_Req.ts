
import { TransmissionType_Mod } from './TransmissionType_Mod';
import { FuelType_Mod } from './FuelType_Mod';
import { CarType_Mod } from './CarType_Mod';

export interface Search_CarFilter_Req  {
    minAge?: number | null;
    maxAge?: number | null;
    minMileage?: number | null;
    maxMileage?: number | null;
    transmission?: TransmissionType_Mod | null;
    fuel?: FuelType_Mod | null;
    carType?: CarType_Mod | null;
    driveTrain?: boolean | null;
}