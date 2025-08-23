import {createContext, useContext, useEffect, useState} from "react";
import keycloak from "../services/keycloak";
import type {KeycloakProfile} from "keycloak-js";
import {clearAuth, getToken, getUser} from "../services/auth";

type AuthContextType = {
    keycloak: any | null;
    authenticated: boolean;
    user: KeycloakProfile | null;
    token: string | null;
    loading: boolean;
    login: () => void;
    logout: () => void;
};

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export function AuthProvider({children}: { children: React.ReactNode }) {
    const [authenticated, setAuthenticated] = useState(false);
    const [user, setUser] = useState<KeycloakProfile | null>(getUser());
    const [token, setToken] = useState<string | null>(getToken());
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        keycloak.onAuthLogout = () => {
            clearAuth();
            setAuthenticated(false);
            setUser(null);
            setToken(null);
        };
    }, []);

    useEffect(() => {
        let mounted = true;

        keycloak.init({
            onLoad: "login-required",
            pkceMethod: "S256",
            checkLoginIframe: false,
        }).then(async (auth: boolean) => {
            if (!mounted) return;
            setAuthenticated(!!auth);

            if (auth && keycloak.token) {
                setToken(keycloak.token);
                localStorage.setItem("token", keycloak.token);

                try {
                    const profile = await keycloak.loadUserInfo();
                    setUser(profile as KeycloakProfile);
                    localStorage.setItem("user", JSON.stringify(profile));
                } catch (err) {
                    console.error("Failed to load user profile:", err);
                }
            } else {
                clearAuth();
                setUser(null);
                setToken(null);
            }
        })
            .catch((err) => console.error("Keycloak init error", err))
            .finally(() => setLoading(false));

        const interval = setInterval(() => {
            keycloak
                .updateToken(60)
                .then((refreshed) => {
                    if (refreshed && keycloak.token) {
                        setToken(keycloak.token);
                        localStorage.setItem("token", keycloak.token);
                    }
                })
                .catch((err) => console.error("Failed to refresh token", err));
        }, 60000);

        return () => {
            mounted = false;
            clearInterval(interval);
        };
    }, []);

    return (
        <AuthContext.Provider
            value={{
                keycloak,
                authenticated,
                user,
                token,
                loading,
                login: () => keycloak.login({prompt: "login"} as any),
                logout: () => keycloak.logout({redirectUri: window.location.origin}),
            }}
        >
            {children}
        </AuthContext.Provider>
    );
}

export function useAuth() {
    const ctx = useContext(AuthContext);
    if (!ctx) throw new Error("useAuth must be used within <AuthProvider>");
    return ctx;
}
