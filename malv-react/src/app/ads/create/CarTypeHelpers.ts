import {TransmissionType_Mod} from "../../../network/Models/TransmissionType_Mod";
import {FuelType_Mod} from "../../../network/Models/FuelType_Mod";
import {CarType_Mod} from "../../../network/Models/CarType_Mod";

export const transmissionTypes = () => {
    return [
        {
            value: TransmissionType_Mod.MANUAL,
            label: 'Manual'
        },
        {
            value: TransmissionType_Mod.AUTOMATIC,
            label: 'Automatic'
        }
    ]
}

export const fuelTypes = () => {
    return [
        {
            value: FuelType_Mod.DIESEL,
            label: 'Diesel'
        },
        {
            value: FuelType_Mod.PETROL,
            label: 'Petrol'
        },
        {
            value: FuelType_Mod.HYBRID,
            label: 'Hybrid'
        }
    ]
}

export const carTypes = () => {
    return [
        {
            value: CarType_Mod.SEDAN,
            label: 'Sedan'
        },
        {
            value: CarType_Mod.CAB,
            label: 'Cabriolet'
        },
        {
            value: CarType_Mod.COMBI,
            label: 'Combi'
        },
        {
            value: CarType_Mod.SUV,
            label: 'SUV'
        },
        {
            value: CarType_Mod.COUPE,
            label: 'Coupe'
        },
        {
            value: CarType_Mod.SMALL,
            label: 'Small'
        }
    ]
}
