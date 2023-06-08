import {UserService} from "./UserService";
import {HttpService} from "./HttpService";
import {AuthService} from "./AuthService";
import {AdService} from "./AdService";
import {CountryService} from "./CountryService";
import {ChatService} from "./ChatService";

const userService = new UserService();
const httpService = new HttpService(userService);
const authService = new AuthService(httpService);
const adService = new AdService(httpService);
const countryService = new CountryService(httpService);
const chatService = new ChatService(httpService);

export class ServiceContainer {
    public static get userService(): UserService {
        return userService;
    }

    public static get httpService(): HttpService {
        return httpService;
    }

    public static get authService(): AuthService {
        return authService;
    }

    public static get adService(): AdService {
        return adService;
    }

    public static get countryService(): CountryService {
        return countryService;
    }

    public static get chatService(): ChatService {
        return chatService;
    }
}
