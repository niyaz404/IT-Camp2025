import {jwtDecode, type JwtPayload} from "jwt-decode";

type LoginParams = { login: string; password: string };
type LoginResponse = { token: string; user: any };
type RegisterParams = { userName: string; login: string; password: string };

const apiUrl = import.meta.env.VITE_API_URL;

export async function getCurrentUserInfo(): Promise<void> {
    const profileRes = await fetch(`${apiUrl}/api/users/me`, {
        headers: {
            Authorization: `Bearer ${getToken()}`,
            "Content-Type": "application/json"
        }
    });

    if (!profileRes.ok) {
        const err = await profileRes.json().catch(()=>({ message: 'Unknown error' }));
        throw err;
    }

    const userInfo = await profileRes.json();

    return userInfo
}

export async function login({ login, password }: LoginParams): Promise<LoginResponse> {
    const res = await fetch(`${apiUrl}/api/auth/login`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ login, password }),
    });

    if (!res.ok) {
        const errData = await res.json();
        throw new Error(errData.message || 'Ошибка авторизации');
    }

    const data = await res.json();
    const token = data.token;
    saveToken(token);

    const userInfo = getCurrentUserInfo();

    return { token: data.token, user: userInfo };
}

export async function register({ userName, login, password }: RegisterParams): Promise<LoginResponse> {
    const res = await fetch(`${apiUrl}/api/auth/register`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ userName, login, password }),
    });

    if (!res.ok) {
        const errData = await res.json();
        throw new Error(errData.message || 'Ошибка регистрации');
    }

    const data = await res.json();
    const token = data.token;
    saveToken(token);

    const userInfo = getCurrentUserInfo();

    return { token: data.token, user: userInfo };
}

export async function resetPassword({ login, password }: LoginParams): Promise<void> {
    const res = await fetch(`${apiUrl}/api/auth/resetpassword`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ login, password }),
    });

    if (!res.ok) {
        const errData = await res.json();
        throw new Error(errData.message || 'Ошибка авторизации');
    }
}

export function saveToken(token: string) {
    localStorage.setItem('jwtToken', token);
}

export function getToken(): string | null {
    return localStorage.getItem('jwtToken');
}

export function removeToken() {
    localStorage.removeItem('jwtToken');
}