
import { CategoryType_Mod } from './CategoryType_Mod';

export interface Category_Mod {
    id: number;
    title: string;
    selectable: boolean;
    type: CategoryType_Mod;
    children: Category_Mod[];
}