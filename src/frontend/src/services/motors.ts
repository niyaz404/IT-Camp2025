import {
    MotorDefectStatus, type MotorDetails,
    MotorState,
    MotorType,
} from "../types/common-types.tsx";
import {authFetch} from "./auth.ts";

const apiUrl = import.meta.env.VITE_API_URL;

export async function getMotorDetails(id: string, token: string | null): Promise<MotorDetails> {
    try {
        return await authFetch<MotorDetails>(`${apiUrl}/motors/${id}`, token);
    } catch (err) {
        console.error("Ошибка при получении стенда:", err);
        return {
            id: 1,
            standId: 1,
            name: "ЭДС-110-117М",
            description: "Описание",
            phasesCount: 3,
            frequency: 25.6,
            power: 110,
            type: MotorType.Async,
            state: MotorState.On,
            defectStatus: MotorDefectStatus.None
        }
    }
}
