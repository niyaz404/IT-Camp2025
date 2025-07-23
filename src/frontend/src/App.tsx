import {Routes, Route, Navigate} from 'react-router-dom';
import Login from './pages/Auth/Login';
import {Theme, presetGpnDefault} from '@consta/uikit/Theme';
import Header from "./components/share/header/header.tsx";
import './App.css'

function App() {
    return (
        <Theme preset={presetGpnDefault}>
            <div className={"app"}>
                <Header></Header>
                <Routes>
                    <Route path="/" element={<Navigate to="/login"/>}/>
                    <Route path="/login" element={<Login/>}/>
                    {/*<Route path="/register" element={<Register />} />*/}
                    {/*<Route path="*" element={<NotFound />} />*/}
                </Routes>
            </div>
        </Theme>
    );
}

export default App;
