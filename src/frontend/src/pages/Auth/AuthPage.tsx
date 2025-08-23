import { useAuth } from "../../context/AuthContext";

export default function AuthPage() {
    const { login, loading, authenticated } = useAuth();
    if (loading) return <div>Загрузка...</div>;
    if (authenticated) return <div>Вы уже авторизованы</div>;

    return (
        <div style={{ padding: 24 }}>
            <h2>Вы вышли из системы</h2>
            <button onClick={login}>Войти</button>
        </div>
    );
}