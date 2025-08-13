export type UserInfo = {
    id: string;
    username: string;
}

export enum StandState {
    On = 1,
    Off = 2
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