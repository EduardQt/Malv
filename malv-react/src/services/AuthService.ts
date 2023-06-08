import {HttpService} from "./HttpService";
import {UserToken_Res} from "../network/Models/UserToken_Res";
import {AuthLogout_URL, LogIn_URL, Register_URL, ValidateRefreshToken_URL, VerifyMail_URL} from "../network/APIData";
import {Auth_Register_Req} from "../network/Models/Auth_Register_Req";
import {Auth_ValidateRefreshToken_Req} from "../network/Models/Auth_ValidateRefreshToken_Req";
import { Auth_LogIn_Req } from "../network/Models/Auth_LogIn_Req";
import {Auth_VerifyMail_Req} from "../network/Models/Auth_VerifyMail_Req";

export class AuthService {
    constructor(
        private readonly _httpService: HttpService
    ) {
    }

    public authFromRefreshToken(req: Auth_ValidateRefreshToken_Req): Promise<UserToken_Res> {
        return this._httpService.post<UserToken_Res, Auth_ValidateRefreshToken_Req>(ValidateRefreshToken_URL, req);
    }

    public register(req: Auth_Register_Req): Promise<void> {
        return this._httpService.postVoid<Auth_Register_Req>(Register_URL, req);
    }

    public logIn(req: Auth_LogIn_Req): Promise<UserToken_Res> {
        return this._httpService.post<UserToken_Res, Auth_LogIn_Req>(LogIn_URL, req);
    }

    public verifyMail(req: Auth_VerifyMail_Req): Promise<void> {
        return this._httpService.postVoid<Auth_VerifyMail_Req>(VerifyMail_URL, req);
    }

    public signOut(): Promise<void> {
        return this._httpService.postVoid(AuthLogout_URL, {});
    }
}
