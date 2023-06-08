
import { Category_Mod } from './Category_Mod';
import { Country_Mod } from './Country_Mod';

export interface Ad_CreateGet_Res {
    categories: Category_Mod[];
    country: Country_Mod;
}