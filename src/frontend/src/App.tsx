import { Routes, Route, Navigate } from "react-router-dom";
import MainPage from "./pages/Main/MainPage";
import StandDetailsPage from "./pages/StandDetails/StandDetailsPage";
import Header from "./components/share/header/header.tsx";
import AppBreadcrumbs from "./components/share/breadcrumbs/breadcrumbs.tsx";
import { BreadcrumbsProvider } from "./context/BreadcrumbsContext.tsx";
import { presetGpnDefault, Theme } from "@consta/uikit/Theme";
import MotorDetailsPage from "./pages/MotorDetails/MotorDetailsPage.tsx";

import "./App.css";

function App() {
    return (
        <Theme preset={presetGpnDefault}>
            <BreadcrumbsProvider>
                <div className="app">
                    <Header />
                    <AppBreadcrumbs />
                    <Routes>
                        <Route path="/stands" element={<MainPage />} />
                        <Route path="/stands/:standId" element={<StandDetailsPage />} />
                        <Route path="/stands/:standId/motors/:motorId" element={<MotorDetailsPage />} />
                        <Route path="/" element={<Navigate to="/stands" replace />} />
                        <Route path="*" element={<Navigate to="/stands" replace />} />
                    </Routes>
                </div>
            </BreadcrumbsProvider>
        </Theme>
    );
}

export default App;
