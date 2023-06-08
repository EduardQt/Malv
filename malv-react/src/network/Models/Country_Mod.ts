
import { Municipality_Mod } from './Municipality_Mod';

export interface Country_Mod {
    id: number;
    name: string;
    municipalities: Municipality_Mod[];
}