import type { UserInfo } from "../types/common-types.tsx";

type LoginParams = { login: string; password: string };
type LoginResponse = { token: string; user: UserInfo };
type RegisterParams = { userName: string | null; login: string; password: string };

const apiUrl = import.meta.env.VITE_API_URL;

export async function getCurrentUserInfo(): Promise<UserInfo> {
    return await authFetch(`${apiUrl}/users/me`);
}

export async function login({ login, password }: LoginParams): Promise<LoginResponse> {
    const res = await fetch(`${apiUrl}/auth/login`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ login, password }),
        credentials: 'include'
    });

    if (!res.ok) {
        const errData = await res.json().catch(() => ({ message: 'Ошибка авторизации' }));
        throw new Error(errData.message);
    }

    const data = await res.json();
    saveToken(data.accessToken);

    const userInfo = await getCurrentUserInfo();
    saveUser(userInfo);

    return { token: data.accessToken, user: userInfo };
}

export async function register({ userName, login, password }: RegisterParams): Promise<LoginResponse> {
    const res = await fetch(`${apiUrl}/auth/register`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ userName, login, password }),
        credentials: 'include'
    });

    if (!res.ok) {
        const errData = await res.json().catch(() => ({ message: 'Ошибка регистрации' }));
        throw new Error(errData.message);
    }

    const data = await res.json();
    saveToken(data.accessToken);

    const userInfo = await getCurrentUserInfo();
    saveUser(userInfo);

    return { token: data.accessToken, user: userInfo };
}

export async function authFetch(url: string, options: RequestInit = {}) {
    const token = getToken();
    if (token) {
        options.headers = {
            ...options.headers,
            'Authorization': `Bearer ${token}`,
        };
    }

    let res = await fetch(url, { ...options, credentials: 'include' });

    if (res.status === 401) {
        const refreshRes = await fetch(`${apiUrl}/auth/refreshtoken`, {
            method: 'POST',
            credentials: 'include',
        });

        if (refreshRes.ok) {
            const data = await refreshRes.json();
            saveToken(data.accessToken);

            options.headers = {
                ...options.headers,
                'Authorization': `Bearer ${data.accessToken}`,
            };
            res = await fetch(url, { ...options, credentials: 'include' });
        } else {
            removeToken();
            removeUser();
            throw new Error('Сессия истекла');
        }
    }

    if (!res.ok) {
        const errData = await res.json().catch(() => ({ message: 'Unknown error' }));
        throw new Error(errData.message);
    }

    return res.json();
}

export async function resetPassword({ login, password }: LoginParams): Promise<void> {
    const res = await fetch(`${apiUrl}/auth/resetpassword`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ login, password }),
        credentials: 'include',
    });

    if (!res.ok) {
        const errData = await res.json().catch(() => ({ message: 'Ошибка' }));
        throw new Error(errData.message);
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

export function saveUser(user: UserInfo) {
    localStorage.setItem('currentUser', JSON.stringify(user));
}

export function getUser(): UserInfo | null {
    const user = localStorage.getItem('currentUser');
    return user ? JSON.parse(user) : null;
}

export function removeUser() {
    localStorage.removeItem('currentUser');
}
