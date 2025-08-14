import type {StandInfo} from "../types/common-types.tsx";
import {authFetch, getToken} from "./auth.ts";


const apiUrl = import.meta.env.VITE_API_URL;

export async function getAllStands(): Promise<StandInfo[]> {
    const data = await authFetch(`${apiUrl}/api/stands/getAll`);

    return data;
}