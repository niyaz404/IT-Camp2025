import {Routes, Route, Navigate} from 'react-router-dom';
import LoginPage from './pages/Auth/LoginPage.tsx';
import {Theme, presetGpnDefault} from '@consta/uikit/Theme';
import Header from "./components/share/header/header.tsx";
import './App.css'
import PrivateRoute from "./components/Auth/private-route/private-route.tsx";
import MainPage from "./pages/Main/MainPage.tsx";
import {getToken, getUser} from "./services/auth.ts";
import {useState} from "react";
import type {UserInfo} from "./types/common-types.tsx";

function App() {
    const [user, setUser] = useState<UserInfo | null>(getUser());
    const token = getToken();

    return (
        <Theme preset={presetGpnDefault}>
            <div className="app">
                <Header userInfo={user} setUserInfo={setUser} />
                <Routes>
                    <Route
                        path="/login"
                        element={token ? <Navigate to="/" /> : <LoginPage setUser={setUser} />}
                    />

                    <Route
                        path="/"
                        element={
                            <PrivateRoute>
                                <MainPage />
                            </PrivateRoute>
                        }
                    />

                    <Route path="*" element={<Navigate to="/" />} />
                </Routes>
            </div>
        </Theme>
    );
}

export default App;
