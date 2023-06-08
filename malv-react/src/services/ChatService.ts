import { Chat_Mod } from "../network/Models/Chat_Mod";
import {HttpService} from "./HttpService";
import {ChatGet_URL, ChatMyChats_URL, ChatSend_URL, ChatUnreadMessages_URL} from "../network/APIData";
import {Chat_Get_Req} from "../network/Models/Chat_Get_Req";
import {Chat_Get_Res} from "../network/Models/Chat_Get_Res";
import {Chat_SendChat_Req} from "../network/Models/Chat_SendChat_Req";
import {Chat_SendChat_Res} from "../network/Models/Chat_SendChat_Res";
import {Chat_UnreadMessages_Res} from "../network/Models/Chat_UnreadMessages_Res";

export class ChatService {
    constructor(
        private readonly _httpService: HttpService
    ) {
    }

    public myChats(): Promise<Chat_Mod[]> {
        return this._httpService.get<Chat_Mod[]>(ChatMyChats_URL);
    }

    public getChat(req: Chat_Get_Req): Promise<Chat_Get_Res> {
        return this._httpService.post<Chat_Get_Res, Chat_Get_Req>(ChatGet_URL, req);
    }

    public sendChatMessage(req: Chat_SendChat_Req): Promise<Chat_SendChat_Res> {
        return this._httpService.post<Chat_SendChat_Res, Chat_SendChat_Req>(ChatSend_URL, req);
    }

    public unreadMessages(): Promise<Chat_UnreadMessages_Res> {
        return this._httpService.get<Chat_UnreadMessages_Res>(ChatUnreadMessages_URL);
    }
}
