type LoginParams = { login: string; password: string };
type LoginResponse = { token: string; user: any };
type RegisterParams = { userName: string; login: string; password: string };

export async function login({ login, password }: LoginParams): Promise<LoginResponse> {
    const res = await fetch('http://localhost:5157/api/auth/login', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ login, password }),
    });

    if (!res.ok) {
        const errData = await res.json();
        throw new Error(errData.message || 'Ошибка авторизации');
    }

    return { token: res, user: null };
}

export async function register({ userName, login, password }: RegisterParams): Promise<LoginResponse> {
    const res = await fetch('http://localhost:5157/api/auth/register', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ userName, login, password }),
    });

    if (!res.ok) {
        const errData = await res.json();
        throw new Error(errData.message || 'Ошибка регистрации');
    }

    return res.json();
}

export async function resetPassword({ login, password }: LoginParams): Promise {
    const res = await fetch('http://localhost:5157/api/auth/resetpassword', {
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