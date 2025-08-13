import {useEffect, useState} from "react";
import {useNavigate} from "react-router-dom";
import StandCard from "../stand-card/stand-card.tsx";
import {type StandInfo} from "../../../types/common-types.tsx";
import {Text} from "@consta/uikit/Text";
import {getAllStands} from "../../../services/stands.ts";
import {getToken} from "../../../services/auth.ts";
import AppBreadcrumbs from "../../share/breadcrumbs/breadcrumbs.tsx";

import "./stand-monitoring.css";

export default function StandMonitoring() {
    const [stands, setStands] = useState<StandInfo[]>([]);
    const navigate = useNavigate();

    useEffect(() => {
        const token = getToken();
        if (!token) {
            navigate("/login");
            return;
        }

        getAllStands()
            .then((data) => setStands(data))
            .catch((err) => {
                console.error(err);
                if (err?.status === 401 || err?.message?.includes("401")) {
                    navigate("/login");
                }
            });
    }, [navigate]);

    return (
        <div className={"monitoring"}>
            <AppBreadcrumbs
                items={[
                    {label: "Главная", path: "/"},
                    {label: "Стенды", path: "/stand"}
                ]}
            />
            <Text size="2xl" weight="bold">
                Стенды
            </Text>
            <div className={"cardsContainer"}>
                {stands.map((stand) => (
                    <StandCard key={stand.id} stand={stand}/>
                ))}
            </div>
        </div>
    );
}