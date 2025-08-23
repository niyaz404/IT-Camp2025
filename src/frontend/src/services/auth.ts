export function getToken(): string | null {
    return localStorage.getItem("token");
}

export function getUser(): any | null {
    const raw = localStorage.getItem("user");
    return raw ? JSON.parse(raw) : null;
}

export function clearAuth() {
    localStorage.removeItem("token");
    localStorage.removeItem("user");
}

export async function authFetch<T>(
    url: string,
    token: string | null,
    options: RequestInit = {}
): Promise<T> {
    const headers = new Headers(options.headers || {});
    if (token) headers.set("Authorization", `Bearer ${token}`);

    const res = await fetch(url, {...options, headers});
    if (!res.ok) {
        const text = await res.text();
        try {
            throw new Error(JSON.parse(text).message || text);
        } catch {
            throw new Error(text || "Unknown error");
        }
    }
    return res.json();
}
