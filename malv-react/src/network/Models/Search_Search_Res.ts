
import { Ad_Mod } from './Ad_Mod';
import { Category_Mod } from './Category_Mod';

export interface Search_Search_Res {
    ads: Ad_Mod[];
    categories: Category_Mod[];
    rootCategory: Category_Mod;
}