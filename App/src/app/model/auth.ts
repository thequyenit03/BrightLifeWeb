import { user } from "./user";

export interface auth{    
    token: string | null,
    status: boolean,
    message: string
}