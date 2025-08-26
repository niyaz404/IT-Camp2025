import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import StandCard from "../stand-card/stand-card";
import { type StandInfo } from "../../../types/common-types";
import { Text } from "@consta/uikit/Text";
import { getAllStands } from "../../../services/stands";
import { useBreadcrumbs } from "../../../context/BreadcrumbsContext";
import { useAuth } from "../../../context/AuthContext.tsx";
import PageWithHeader from "../../share/page-with-header/page-with-header.tsx";

import "./stand-monitoring.css";

export default function StandMonitoring() {
    const [stands, setStands] = useState<StandInfo[]>([]);
    const navigate = useNavigate();
    const { setItems } = useBreadcrumbs();
    const { authenticated, keycloak, loading } = useAuth();
    const token = keycloak?.token;

    useEffect(() => {
        setItems([{ label: "Стенды", path: "/stands" }]);
    }, [setItems]);

    useEffect(() => {
        if (loading) return;
        if (!authenticated || !token) return;

        getAllStands(token)
            .then((data) => setStands(data))
            .catch((err) => {
                console.error(err);
            });
    }, [authenticated, token, loading]);

    return (
        <PageWithHeader header={(
            <Text size="2xl" weight={"bold"}>Стенды</Text>
        )}>
            <div className="cardsContainer">
                {stands.map((stand) => (
                    <StandCard
                        key={stand.id}
                        stand={stand}
                        onClick={() => navigate(`/stands/${stand.id}`)}
                    />
                ))}
            </div>
        </PageWithHeader>
    );
}
