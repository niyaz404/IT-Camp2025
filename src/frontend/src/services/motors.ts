import {
    type MotorDefect,
    MotorDefectStatus, type MotorDetails,
    MotorState,
    MotorType,
} from "../types/common-types.tsx";
import {authFetch} from "./auth.ts";

const apiUrl = import.meta.env.VITE_API_URL;

export async function getMotorDetails(id: string, token: string | null): Promise<MotorDetails | null> {
    try {
        const motor = await authFetch<MotorDetails>(`${apiUrl}/motors/${id}`, token);
        if (!motor) {
            return null;
        }

        motor.maxSeverity = getMaxSeverity(motor.defects);

        return motor;
    } catch (err) {
        console.error("Ошибка при получении стенда:", err);
        return {
            id: 1,
            standId: 1,
            name: "ЭДС-110-117М",
            manufacturer: "Электомаш",
            factoryNumber: "765433456-2",
            description: "Описание",
            phasesCount: 3,
            ratedFrequency: 25.6,
            ratedPower: 110,
            ratedCurrent: 100,
            voltage: 400,
            type: MotorType.Async,
            state: MotorState.On,
            defectStatus: MotorDefectStatus.None,
            maxSeverity: 60,
            defects: []
        }
    }
}

export function getMaxSeverity(defects: MotorDefect[] | null): number {
    if (!defects || defects.length === 0)
        return 0;
    return defects.reduce((max, d) => Math.max(max, d.severity), 0);
}
