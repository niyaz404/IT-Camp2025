import {useState} from 'react';
import {Tabs} from "@consta/uikit/Tabs";
import LoginForm from "../login-form/login-form.tsx";
import RegisterForm from "../register-form/register-form.tsx";
import {Text} from "@consta/uikit/Text";
import {useNavigate} from "react-router-dom";
import type {UserInfo} from "../../../types/common-types.tsx";

import './auth-form.css';

type TabItem = {
    name: string;
    id: 'login' | 'register';
};

export default function AuthForm({setUser}: { setUser: (user: UserInfo | null) => void }) {
    const items: TabItem[] = [
        {name: 'Вход', id: 'login'},
        {name: 'Регистрация', id: 'register'},
    ];

    const navigate = useNavigate();
    const handleAuthSuccess = () => {
        navigate("/", {replace: true});
    };

    const [activeTab, setActiveTab] = useState<TabItem>(items[0]);
    const handleActiveTabChange = (value: TabItem) => setActiveTab(value);

    return (
        <>
            <div className="authForm">
                <Text className="formHeader" size="3xl" weight="bold">газпром_нефть</Text>
                {activeTab.id === 'login' && <Text className="formHeader" size="2xl" weight="bold">Вход в систему PREDITRIX</Text>}
                {activeTab.id === 'register' && <Text className="formHeader" size="2xl" weight="bold">Регистрация в системе PREDITRIX</Text>}
                <Tabs<TabItem>
                    items={items}
                    value={activeTab}
                    onChange={(value) => handleActiveTabChange(value)}
                    getItemLabel={(item) => item.name}
                />

                <div style={{marginTop: '24px'}}>
                    {activeTab.id === 'login' && <LoginForm setUser={setUser} onAuthSuccess={handleAuthSuccess}/>}
                    {activeTab.id === 'register' && <RegisterForm setUser={setUser} onAuthSuccess={handleAuthSuccess}/>}
                </div>
            </div>
        </>
    );
}
