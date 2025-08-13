import type {StandInfo} from "../types/common-types.tsx";
import {getToken} from "./auth.ts";


const apiUrl = import.meta.env.VITE_API_URL;

export async function getAllStands(): Promise<StandInfo[]> {
    const res = await fetch(`${apiUrl}/api/stands/getAll`, {
        method: 'GET',
        headers: {
            Authorization: `Bearer ${getToken()}`,
            "Content-Type": "application/json"
        },
    });

    if (!res.ok) {
        const errData = await res.json();
        console.log(errData)
        //throw new Error(errData.message);
    }

    return await res.json();
}