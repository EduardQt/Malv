import {HttpService} from "./HttpService";
import {
    AdAddAdWatch_URL,
    AdCreate_URL,
    AdCreateGet_URL,
    AdGet_URL, AdGetAdWatches_URL, HomeBaseCategories_URL, HomeLastAds_URL, SearchSearch_URL
} from "../network/APIData";
import {Category_Mod} from "../network/Models/Category_Mod";
import {Ad_Create_Req} from "../network/Models/Ad_Create_Req";
import {Ad_Mod} from "../network/Models/Ad_Mod";
import {Ad_CreateGet_Res} from "../network/Models/Ad_CreateGet_Res";
import {Home_LastAds_Res} from "../network/Models/Home_LastAds_Res";
import { Search_Search_Req } from "../network/Models/Search_Search_Req";
import { Search_Search_Res } from "../network/Models/Search_Search_Res";
import { Ad_GetAdWatches_Res } from "../network/Models/Ad_GetAdWatches_Res";
import {Ad_AddAdWatch_Req} from "../network/Models/Ad_AddAdWatch_Req";

export class AdService {
    constructor(
        private readonly _httpService: HttpService
    ) {
    }

    public lastAds(): Promise<Home_LastAds_Res> {
        return this._httpService.get<Home_LastAds_Res>(HomeLastAds_URL);
    }

    public get(id: number): Promise<Ad_Mod> {
        return this._httpService.get<Ad_Mod>(AdGet_URL + "?id=" + id);
    }

    public getCreate(): Promise<Ad_CreateGet_Res> {
        return this._httpService.get<Ad_CreateGet_Res>(AdCreateGet_URL);
    }

    public adCreate(req: Ad_Create_Req): Promise<Category_Mod[]> {
        const formData = new FormData();
        formData.append('title', req.title);
        formData.append('description', req.description);
        if (req.carAdMod) {
            formData.append('carAdMod', req.carAdMod);
        }
        formData.append('categoryId', req.categoryId as any);
        for (let i = 0; i < req.files.length; i++) {
            formData.append("files", req.files[i]);
        }
        formData.append('municipalityId', req.municipalityId.toString(10));
        return this._httpService.postFormData<any>(AdCreate_URL, formData);
    }

    public search(req: Search_Search_Req): Promise<Search_Search_Res> {
        return this._httpService.post<Search_Search_Res, Search_Search_Req>(SearchSearch_URL, req);
    }

    public getWatches(): Promise<Ad_GetAdWatches_Res> {
        return this._httpService.get<Ad_GetAdWatches_Res>(AdGetAdWatches_URL);
    }

    public addAdWatch(req: Ad_AddAdWatch_Req): Promise<void> {
        return this._httpService.postVoid(AdAddAdWatch_URL, req);
    }

    public baseCategories(): Promise<Category_Mod[]> {
        return this._httpService.get(HomeBaseCategories_URL);
    }
}
