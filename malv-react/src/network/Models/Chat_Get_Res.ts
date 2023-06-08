
import { Chat_Mod } from './Chat_Mod';
import { ChatMessage_Mod } from './ChatMessage_Mod';

export interface Chat_Get_Res {
    chat: Chat_Mod;
    chatMessages: ChatMessage_Mod[];
}