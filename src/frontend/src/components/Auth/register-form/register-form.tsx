import {useState} from 'react';
import {TextField} from "@consta/uikit/TextField";
import {Button} from "@consta/uikit/Button";
import {register} from '../../../services/auth';
import {Text} from "@consta/uikit/Text";
import type {UserInfo} from "../../../types/common-types.tsx";

import './register-form.css';

export default function RegisterForm({ setUser, onAuthSuccess }: { onAuthSuccess: () => void, setUser: (user: UserInfo) => void }) {
    const [userNameValue, setUserNameValue] = useState<string | null>(null);
    const [loginValue, setLoginValue] = useState<string | null>(null);
    const [passwordValue, setPasswordValue] = useState<string | null>(null);
    const handleuserNameChange = (value: string | null ) => setUserNameValue(value);
    const handleLoginChange = (value: string | null ) => setLoginValue(value);
    const handlePasswordChange = (value: string | null ) => setPasswordValue(value);

    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setError(null);

        if (!loginValue || !passwordValue) {
            setError('Введите логин и пароль');
            return;
        }

        setLoading(true);

        try {
            const data = await register({ userName: userNameValue, login: loginValue, password: passwordValue });

            setUser(data.user);
            onAuthSuccess();

        } catch (err: any) {
            setError(err.message || 'Ошибка регистрации');
        } finally {
            setLoading(false);
        }
    };

    const onEnterKeyDown = (e: React.KeyboardEvent<HTMLInputElement>) => {
        {
            if (e.key === 'Enter') {
                handleSubmit(e);
            }
        }
    }

    return (
        <>
           <div className={"registerForm"}>
               <TextField
                   onChange={handleuserNameChange}
                   value={userNameValue}
                   type="text"
                   label="ФИО пользователя"
                   placeholder="ФИО пользователя"/>
               <TextField
                   onChange={handleLoginChange}
                   value={loginValue}
                   type="text"
                   label="Логин"
                   placeholder="Логин"/>
               <TextField
                   onChange={handlePasswordChange}
                   value={passwordValue}
                   type="password"
                   label="Пароль"
                   placeholder="Пароль"
                   onKeyDown={onEnterKeyDown}
               />
               {error && (
                   <Text view="warning">{error}</Text>
               )}
               <Button label="Зарегистрироваться" disabled={loading} onClick={handleSubmit}/>
           </div>
        </>
    );
}
