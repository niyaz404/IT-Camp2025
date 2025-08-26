export type UserInfo = {
    id: string;
    username: string;
}

export enum StandState {
    Unknown = 0,
    On = 1,
    Off = 2
}

export enum MotorState {
    Unknown = 0,
    On = 1,
    Off = 2
}

export enum MotorDefectStatus {
    None = "NONE", //дефектов нет
    Minor = "MINOR", //незначительные дефекты
    Critical = "CRITICAL", //критический дефект
    Unknown = "UNKNOWN"
}

export enum MotorType {
    Unknown = "Нет информации",
    Async = "Асинхронный",
    Valve = "Вентильный",
}

export const MotorTypeMap: Record<number, string> = {
    0: MotorType.Unknown,
    1: MotorType.Async,
    2: MotorType.Valve,
};

export type User = {
    id: string;
    username: string;
    firstName: string;
    lastName: string;
}

export type StandInfo = {
    id: number;
    name: string;
    description: string;
    state: StandState
    location: string;
    motorsCount: number;
    responsiblePerson: User;
    createdAt: Date
    updatedAt: Date
    defectsCount: number;
};

export type StandDetails = {
    id: number;
    name: string;
    description: string;
    motors: MotorInfo[];
    state: StandState;
    defectsCount: number;
}

export type MotorInfo = {
    id: number;
    name: string;
    state: MotorState,
    manufacturer: string;
    factoryNumber: string;
    defectStatus: MotorDefectStatus;
    type: MotorType
    ratedPower: number;
    maxSeverity: number;
}

export type MotorDetails = {
    id: number;
    standId: number;
    name: string;
    manufacturer: string;
    factoryNumber: string;
    description: string;
    phasesCount: number;
    ratedFrequency: number;
    ratedPower: number;
    ratedCurrent: number;
    voltage: number;
    type: MotorType;
    state: MotorState;
    defectStatus: MotorDefectStatus;
    maxSeverity: number;
    defects: MotorDefect[] | null;
    responsiblePerson: User;
    updatedAt: Date;
}

export enum DefectType {
    Stator = 1,     // Статор
    Rotor = 2,      // Ротор
    Bearing = 3,    // Подшипники
    Other = 4       // Прочее
}

export type MotorDefect = {
    id: number;
    motorId: number;
    defectType: DefectType;
    severity: number;
    detectionDate: string;
    detectorVersion?: string;
    note?: string;
    createdAt: string;
    updatedAt: string;
}