import {useState} from 'react';
import {TextField} from "@consta/uikit/TextField";
import './register-form.css';
import {Button} from "@consta/uikit/Button";
import {register, saveToken} from '../../../services/auth';
import {Text} from "@consta/uikit/Text";

export default function RegisterForm() {
    const [userNameValue, setuserNameValue] = useState<string | null>(null);
    const [loginValue, setLoginValue] = useState<string | null>(null);
    const [passwordValue, setPasswordValue] = useState<string | null>(null);
    const handleuserNameChange = (value: string | null ) => setuserNameValue(value);
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

            // TODO: Навигация или обновление состояния приложения

        } catch (err: any) {
            setError(err.message || 'Ошибка регистрации');
        } finally {
            setLoading(false);
        }
    };

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
               />
               {error && (
                   <Text view="warning">{error}</Text>
               )}
               <Button label="Зарегистрироваться" disabled={loading} onClick={handleSubmit}/>
           </div>
        </>
    );
}
