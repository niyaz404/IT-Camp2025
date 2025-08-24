import {useParams} from "react-router-dom";
import {useEffect, useState} from "react";
import { IconAllDone } from "@consta/icons/IconAllDone";
import { IconWarning } from "@consta/icons/IconWarning";
import {
    type MotorDetails,
    type MotorInfo,
    MotorState,
    type StandDetails,
    StandState
} from "../../../types/common-types.tsx";
import {useBreadcrumbs} from "../../../context/BreadcrumbsContext.tsx";
import {Text} from "@consta/uikit/Text";
import {Badge} from "@consta/uikit/Badge";
import {SkeletonBrick} from "@consta/uikit/Skeleton";
import { useAuth } from "../../../context/AuthContext";
import PageHeader from "../../share/page-header/page-header.tsx";
import {getMotorDetails} from "../../../services/motors.ts";

import "./motor-details.css"
import {getStandDetails} from "../../../services/stands.ts";

export default function MotorDetailsPage() {
    const {motorId} = useParams<{motorId: string}>();
    const [motor, setMotor] = useState<MotorDetails | null>(null);
    const [loadingData, setLoadingData] = useState(true);
    const {setItems} = useBreadcrumbs();

    const { authenticated, loading, keycloak } = useAuth();
    const token = keycloak?.token ?? null;

    useEffect(() => {
        if (!motorId) return;
        if (loading || !authenticated || !token) return;

        setLoadingData(true);

        getMotorDetails(motorId, token)
            .then(async (m) => {
                setMotor(m);

                let standName = "";
                try {
                    const stand = await getStandDetails(m.standId.toString(), token);
                    standName = stand.name;
                } catch (e) {
                    console.error("Не удалось получить стенд:", e);
                    standName = m.standId.toString();
                }

                setItems([
                    { label: "Стенды", path: "/stands" },
                    { label: standName, path: `/stands/${m.standId}` },
                    { label: m.name, path: `/stands/${m.standId}/motors/${m.id}` },
                ]);
            })
            .catch(console.error)
            .finally(() => setLoadingData(false));
    }, [motorId, setItems, loading, authenticated, token]);

    const getStateBadgeProps = (state: StandDetails | MotorInfo) => {
        switch (state.state) {
            case StandState.On | MotorState.On:
                return { label: "Активен", status: "success" as const, iconLeft: IconAllDone };
            case StandState.Off | MotorState.Off:
                return { label: "Неактивен", status: "error" as const, iconLeft: IconWarning };
        }
    }

    return (
        <div className={"container"}>
            <PageHeader>
                <div style={{display: "flex", alignItems: "center", gap: 16, marginBottom: 24}}>
                    {loadingData ? (
                        <>
                            <SkeletonBrick className={"skeleton"} height={49} width={250} />
                        </>
                    ) : (
                        <>
                            <Text size="3xl" weight={"extrabold"}>{motor?.name}</Text>
                            {motor && <Badge {...getStateBadgeProps(motor)} />}
                        </>
                    )}
                </div>
            </PageHeader>

            <div className={"content"}>

            </div>
        </div>
    );
}
