import {CarAd_Mod} from "../../../../network/Models/CarAd_Mod";
import {TransmissionType_Mod} from "../../../../network/Models/TransmissionType_Mod";
import {FuelType_Mod} from "../../../../network/Models/FuelType_Mod";
import {CarType_Mod} from "../../../../network/Models/CarType_Mod";

export const getTransmissionType = (carAd: CarAd_Mod) => {
    switch (carAd.transmissionType) {
        case TransmissionType_Mod.AUTOMATIC:
            return 'Automatic';
        case TransmissionType_Mod.MANUAL:
            return 'Manual';
        default:
            return '';
    }
}

export const getFuelType = (carAd: CarAd_Mod) => {
    switch (carAd.fuelType) {
        case FuelType_Mod.PETROL:
            return 'Petrol';
        case FuelType_Mod.DIESEL:
            return 'Diesel';
        case FuelType_Mod.HYBRID:
            return 'Hybrid';
        default:
            return '';
    }
}

export const getCarType = (carAd: CarAd_Mod) => {
    switch (carAd.carType) {
        case CarType_Mod.SMALL:
            return 'Smallcar';
        case CarType_Mod.SEDAN:
            return 'Sedan';
        case CarType_Mod.HATCHBACK:
            return 'Hatchback';
        case CarType_Mod.COMBI:
            return 'Combi';
        case CarType_Mod.COUPE:
            return 'Coupe';
        case CarType_Mod.CAB:
            return 'Cabriolet';
        case CarType_Mod.SUV:
            return 'SUV';
        case CarType_Mod.FAMILY_BUS:
            return 'Familybus';
        case CarType_Mod.TRANSPORT:
            return 'Transport';
        default:
            return '';
    }
}

export const getCarDriveType = (carAd: CarAd_Mod) => {
    if (carAd.allWheelDrive) return 'All-wheel drive';
    return 'Two-wheel drive';
}
