import {useState} from 'react';
import {TextField} from "@consta/uikit/TextField";
import './login-form.css';
import {Button} from "@consta/uikit/Button";
import {Text} from '@consta/uikit/Text';
import {login, resetPassword, saveToken} from '../../../services/auth';

export default function LoginForm() {
    const [loginValue, setLoginValue] = useState<string | null>(null);
    const [passwordValue, setPasswordValue] = useState<string | null>(null);
    const [newPasswordValue, setNewPasswordValue] = useState<string | null>(null);
    const handleLoginChange = (value: string | null) => setLoginValue(value);
    const handlePasswordChange = (value: string | null) => setPasswordValue(value);
    const handleNewPasswordChange = (value: string | null) => setNewPasswordValue(value);


    const [resetPswTab, setResetPswTab] = useState(false);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);
    const [success, setSuccess] = useState<string | null>(null);
    const [errorReset, setErrorReset] = useState<string | null>(null);

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setError(null);
        setSuccess(null);

        if (!loginValue || !passwordValue) {
            setError('Введите логин и пароль');
            return;
        }

        setLoading(true);

        try {
            const data = await login({login: loginValue, password: passwordValue});
            saveToken(data);

            // TODO: Навигация или обновление состояния приложения

        } catch (err: Error){
            console.log(err);
            setError(err.message);
        } finally {
            setLoading(false);
        }
    };

    const handleResetPassword = async (e: React.FormEvent) => {
        e.preventDefault();
        setErrorReset(null);
        setSuccess(null);

        if (!loginValue || !newPasswordValue) {
            setError('Введите логин и пароль');
            return;
        }

        setLoading(true);

        try {
            await resetPassword({login: loginValue, password: newPasswordValue});
            setSuccess("Пароль сброшен");
            setResetPswTab(false);
            setNewPasswordValue(null);

        } catch {
            setErrorReset('Ошибка сброса');
        } finally {
            setLoading(false);
        }
    };

    return (
        <>
            {!resetPswTab && (<div className={"loginForm"}>
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
                <Text
                    as="span"
                    view="link"
                    size="s"
                    onClick={() => setResetPswTab(true)}
                >
                    Забыли пароль?
                </Text>
                {error && (
                    <Text view="warning">{error}</Text>
                )}
                {success && (
                    <Text view="success">{success}</Text>
                )}
                <Button label="Войти" disabled={loading} onClick={handleSubmit}/>
            </div>)}
            {resetPswTab && (<div className={"loginForm"}>
                <TextField
                    onChange={handleLoginChange}
                    value={loginValue}
                    type="text"
                    label="Логин"
                    placeholder="Логин"/>
                <TextField
                    onChange={handleNewPasswordChange}
                    value={newPasswordValue}
                    type="password"
                    label="Новый пароль"
                    placeholder="Новый пароль"
                />
                <Text
                    as="span"
                    view="link"
                    size="s"
                    onClick={() => setResetPswTab(false)}
                >
                    Вспомнили пароль?
                </Text>
                {errorReset && (
                    <Text view="warning">{errorReset}</Text>
                )}
                <Button label="Сбросить" disabled={loading} onClick={handleResetPassword}/>
            </div>)}
        </>
    );
}
