export type UserInfo = {
    id: string;
    username: string;
}

export enum StandState {
    On = 1,
    Off = 2
}

export enum MotorState {
    On = 1,
    Off = 2
}

export enum MotorType {
    Async = "Асинхронный",
    Valve = "Вентильный",
}

export type StandInfo = {
    id: number;
    name: string;
    description: string;
    phasesCount: number;
    frequency: number;
    power: number;
    tau: string;
    responsiblePerson: UserInfo;
    state: StandState;
};

export type StandDetails = {
    id: number;
    name: string;
    description: string;
    motors: MotorInfo[];
    state: StandState;
}

export type MotorInfo = {
    id: number;
    name: string;
    state: MotorState,
    type: MotorType
    power: number;
}

export type MotorDetails = {
    id: number;
    name: string;
    description: string;
    phasesCount: number;
    frequency: number;
    power: number;
    type: MotorType
    state: MotorState
}