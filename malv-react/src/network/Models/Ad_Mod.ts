
import { CarAd_Mod } from './CarAd_Mod';

export interface Ad_Mod {
    id: number;
    title: string;
    description: string;
    author: string;
    images: string[];
    price: number;
    carAd: CarAd_Mod;
}