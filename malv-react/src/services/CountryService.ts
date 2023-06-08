import {HttpService} from "./HttpService";
import {CountryGetCountry_URL} from "../network/APIData";
import { Country_GetCountry_Res } from "../network/Models/Country_GetCountry_Res";

export class CountryService {
    constructor(
        private readonly _httpService: HttpService
    ) {
    }

    public getCountry(): Promise<Country_GetCountry_Res> {
        return this._httpService.get<Country_GetCountry_Res>(CountryGetCountry_URL);
    }
}
