
import { Municipality_Mod } from './Municipality_Mod';

export interface District_Mod {
    id: number;
    name: string;
    municipalities: Municipality_Mod[];
}