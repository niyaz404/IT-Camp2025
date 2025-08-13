import AuthForm from "../../components/Auth/auth-form/auth-form.tsx";
import type {UserInfo} from "../../types/common-types.tsx";

export default function LoginPage({setUser}: { setUser: (user: UserInfo | null) => void }) {

    return (
        <>
            <AuthForm setUser={setUser}/>
        </>
    );
}
