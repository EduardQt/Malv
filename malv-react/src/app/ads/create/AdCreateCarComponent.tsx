import {CarAd_Mod} from "../../../network/Models/CarAd_Mod";
import {Dispatch, useEffect} from "react";
import {Input, Label, Row} from "reactstrap";
import {TransmissionType_Mod} from "../../../network/Models/TransmissionType_Mod";
import {FuelType_Mod} from "../../../network/Models/FuelType_Mod";
import {CarType_Mod} from "../../../network/Models/CarType_Mod";
import {carTypes, fuelTypes, transmissionTypes} from "./CarTypeHelpers";

interface AdCreateCarComponentProps {
    carAd: CarAd_Mod | null;
    setCarAd: Dispatch<CarAd_Mod>;
}


export const AdCreateCarComponent = (props: AdCreateCarComponentProps) => {

    useEffect(() => {
        if (!props.carAd) {
            const carAd: CarAd_Mod = {
                age: new Date().getFullYear(),
                mileage: 0,
                transmissionType: TransmissionType_Mod.MANUAL,
                fuelType: FuelType_Mod.DIESEL,
                carType: CarType_Mod.SEDAN,
                allWheelDrive: false,
                horsePower: 0
            };
            props.setCarAd(carAd);
        }
    }, []);

    const changeAge = (value: string) => {
        const parsedInt = parseInt(value);
        props.setCarAd({
            ...props.carAd as CarAd_Mod,
            age: parsedInt
        });
    }

    const mileageAge = (value: string) => {
        const parsedInt = parseInt(value);
        props.setCarAd({
            ...props.carAd as CarAd_Mod,
            mileage: parsedInt
        });
    }

    const carTypeChange = (value: string) => {
        const parsedInt = parseInt(value);
        props.setCarAd({
            ...props.carAd as CarAd_Mod,
            carType: parsedInt as CarType_Mod
        });
    }

    const fuelTypeChange = (value: string) => {
        const parsedInt = parseInt(value);
        props.setCarAd({
            ...props.carAd as CarAd_Mod,
            fuelType: parsedInt as FuelType_Mod
        });
    }

    const transmissionTypeChange = (value: string) => {
        const parsedInt = parseInt(value);
        props.setCarAd({
            ...props.carAd as CarAd_Mod,
            transmissionType: parsedInt as TransmissionType_Mod
        });
    }

    return (
        <Row>
            <div className={'col col-sm-12 col-md-8 col-lg-6'}>
                <Label for="adCreateDesc">
                    Age
                </Label>
                <Input type={'number'} value={props.carAd?.age}
                       onChange={(event) => changeAge(event.target.value)}/>
            </div>
            <div className={'col col-sm-12 col-md-8 col-lg-6'}>
                <Label for="adCreateDesc">
                    Mileage
                </Label>
                <Input type={'number'} value={props.carAd?.mileage}
                       onChange={(event) => mileageAge(event.target.value)}/>
            </div>
            <div className={'col col-sm-12 col-md-8 col-lg-6'}>
                <Label for="adCreateDesc">
                    Cartype
                </Label>
                <Input type={'select'} value={props.carAd?.carType}
                       onChange={(event) => carTypeChange(event.target.value)}>
                    {carTypes().map((value, index) => {
                        return (
                            <option value={value.value} key={index}>{value.label}</option>
                        )
                    })}
                </Input>
            </div>
            <div className={'col col-sm-12 col-md-8 col-lg-6'}>
                <Label for="adCreateDesc">
                    Fueltype
                </Label>
                <Input type={'select'} value={props.carAd?.fuelType}
                       onChange={(event) => fuelTypeChange(event.target.value)}>
                    {fuelTypes().map((value, index) => {
                        return (
                            <option value={value.value} key={index}>{value.label}</option>
                        )
                    })}
                </Input>
            </div>
            <div className={'col col-sm-12 col-md-8 col-lg-6'}>
                <Label for="adCreateDesc">
                    Transmission type
                </Label>
                <Input type={'select'} value={props.carAd?.transmissionType}
                       onChange={(event) => transmissionTypeChange(event.target.value)}>
                    {transmissionTypes().map((value, index) => {
                        return (
                            <option value={value.value} key={index}>{value.label}</option>
                        )
                    })}
                </Input>
            </div>
        </Row>
    )
}
