import {LocalStorageRefreshToken} from "./LocalStorageKeys";
import {UserToken_Res} from "../network/Models/UserToken_Res";

export class UserService {

    private _authToken: UserToken_Res | null = null;
    private _refreshToken: string = '';

    public get authToken(): UserToken_Res | null {
        return this._authToken;
    }

    public get authTokenAbsolute(): UserToken_Res {
        return this._authToken as UserToken_Res;
    }

    public set authToken(userToken: UserToken_Res | null) {
        this._authToken = userToken;
    }

    public get refreshToken(): string | null {
        if (this._refreshToken !== '') return this._refreshToken;

        const saved = localStorage.getItem(LocalStorageRefreshToken);
        if (saved) {
            this._refreshToken = saved;
            return this._refreshToken;
        }

        return null;
    }

    public set refreshToken(refreshToken: string | null) {
        if (!refreshToken) throw new Error("Auth token can't be null!");
        this._refreshToken = refreshToken;
        localStorage.setItem(LocalStorageRefreshToken, refreshToken);
    }

    public signOut() {
        if (localStorage.getItem(LocalStorageRefreshToken)) {
            localStorage.removeItem(LocalStorageRefreshToken);
        }

        this._refreshToken = '';
        this._authToken = null;
    }
}
