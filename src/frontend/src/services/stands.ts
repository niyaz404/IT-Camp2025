import { MotorState, MotorType, type StandDetails, type StandInfo, StandState } from "../types/common-types.tsx";
import { authFetch } from "./auth.ts";

const apiUrl = import.meta.env.VITE_API_URL;

export async function getAllStands(token: string | null): Promise<StandInfo[]> {
    try {
        return await authFetch<StandInfo[]>(`${apiUrl}/stands/getAll`, token);
    } catch (err) {
        console.error("Ошибка при получении стендов:", err);
        throw err;
    }
}

export async function getStandDetails(id: string, token: string | null): Promise<StandDetails> {
    try {
        return await authFetch<StandDetails>(`${apiUrl}/stands/${id}`, token);
    } catch (err) {
        console.error("Ошибка при получении стенда:", err);
        return {
            id: Number(id),
            name: "Стенд 1",
            description: "Описание",
            state: StandState.On,
            motors: [
                { id: 1, name: "ЭДС-110-117М", state: MotorState.On, type: MotorType.Async, power: 110 },
                { id: 2, name: "ЭД-125-117М", state: MotorState.On, type: MotorType.Async, power: 125 },
                { id: 3, name: "ЭДС-125-117М", state: MotorState.Off, type: MotorType.Async, power: 125 },
                { id: 4, name: "ЭДС-140-117М", state: MotorState.Off, type: MotorType.Async, power: 140 },
                { id: 5, name: "ВДМ14-400-3.0-117/1В5", state: MotorState.On, type: MotorType.Valve, power: 140 },
            ]
        };
    }
}
