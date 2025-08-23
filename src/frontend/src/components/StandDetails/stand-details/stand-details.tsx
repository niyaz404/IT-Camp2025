import {useParams} from "react-router-dom";
import {useEffect, useState} from "react";
import { IconAllDone } from "@consta/icons/IconAllDone";
import { IconWarning } from "@consta/icons/IconWarning";
import {
    type MotorInfo,
    MotorState,
    type StandDetails,
    StandState
} from "../../../types/common-types.tsx";
import {getStandDetails} from "../../../services/stands.ts";
import {useBreadcrumbs} from "../../../context/BreadcrumbsContext.tsx";
import {Text} from "@consta/uikit/Text";
import {Badge} from "@consta/uikit/Badge";
import MotorsTable from "../motors-table/motors-table.tsx";
import { useAuth } from "../../../context/AuthContext";

export default function StandDetailsPage() {
    const {standId} = useParams<{standId: string}>();
    const [stand, setStand] = useState<StandDetails | null>(null);
    const {setItems} = useBreadcrumbs();

    const { authenticated, loading, keycloak } = useAuth();
    const token = keycloak?.token ?? null;

    useEffect(() => {
        if (!standId) return;
        if (loading || !authenticated || !token) return;

        getStandDetails(standId, token)
            .then((s) => {
                setStand(s);
                setItems([
                    {label: "Стенды", path: "/stands"},
                    {label: s.name, path: `/stands/${s.id}`},
                ]);
            })
            .catch(console.error);
    }, [standId, setItems, loading, authenticated, token]);

    const getStateBadgeProps = (state: StandDetails | MotorInfo) => {
        switch (state.state) {
            case StandState.On | MotorState.On: return { label: "ВКЛЮЧЕН", status: "success" as const, iconLeft: IconAllDone };
            case StandState.Off | MotorState.Off: return { label: "ВЫКЛЮЧЕН", status: "error" as const, iconLeft: IconWarning };
        }
    }

    if (!stand) return <div>Загрузка...</div>;

    return (
        <div style={{padding: 20}}>
            <div style={{display: "flex", alignItems: "center", gap: 16, marginBottom: 24}}>
                <Text size="2xl">{stand.name}</Text>
                <Badge {...getStateBadgeProps(stand)} />
            </div>

            <div style={{marginTop: 32}}>
                <Text size="l">Электродвигатели</Text>
                <MotorsTable motors={stand.motors || []} />
            </div>
        </div>
    );
}
