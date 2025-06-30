import { Messages } from "./MessagesVN";
import { EN } from "./MessagesEN";

export const MessagesVN = new Messages();
export const MessagesEN = new EN();
let currentLanguage: string = 'vi';
export function getMessages(language: string): Messages {
    if (currentLanguage === "vi") {
        return MessagesVN;
    } else {
        return MessagesEN;
    }
}