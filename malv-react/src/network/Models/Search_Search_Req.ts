
import { Search_Order_Mod } from './Search_Order_Mod';
import { Search_CarFilter_Req } from './Search_CarFilter_Req';

export interface Search_Search_Req  {
    query: string;
    municipalityId?: number | null;
    categoryId?: number | null;
    order: Search_Order_Mod;
    carFilter: Search_CarFilter_Req;
}