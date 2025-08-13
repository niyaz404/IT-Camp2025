import {Navigate} from 'react-router-dom';
import {getToken} from "../../../services/auth.ts";

export default function PrivateRoute({children}: { children: JSX.Element }) {
    const token = getToken();

    if (!token) {
        return <Navigate to="/login" replace/>;
    }

    return children;
}