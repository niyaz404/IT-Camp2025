import {useState} from 'react';
import './auth-form.css';
import {Tabs} from "@consta/uikit/Tabs";
import LoginForm from "../login-form/login-form.tsx";
import RegisterForm from "../register-form/register-form.tsx";
import {Text} from "@consta/uikit/Text";

type TabItem = {
    name: string;
    id: 'login' | 'register';
};

export default function AuthForm() {
    const items: TabItem[] = [
        { name: 'Вход', id: 'login' },
        { name: 'Регистрация', id: 'register' },
    ];

    const [activeTab, setActiveTab] = useState<TabItem>(items[0]);
    const handleActiveTabChange = (value : TabItem) => setActiveTab(value);

    return (
        <>
            <div className="authForm">
                <Text className="formHeader" size="3xl" weight="bold">газпром_нефть</Text>
                <Tabs<TabItem>
                    items={items}
                    value={activeTab}
                    onChange={(value) => handleActiveTabChange(value)}
                    getItemLabel={(item) => item.name}
                />

                <div style={{ marginTop: '24px' }}>
                    {activeTab.id === 'login' && <LoginForm />}
                    {activeTab.id === 'register' && <RegisterForm />}
                </div>
            </div>
        </>
    );
}
