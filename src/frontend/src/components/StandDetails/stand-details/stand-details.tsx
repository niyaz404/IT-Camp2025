import {useParams} from "react-router-dom";
import {useEffect, useState} from "react";
import {IconAllDone} from "@consta/icons/IconAllDone";
import {IconWarning} from "@consta/icons/IconWarning";
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
import {SkeletonText, SkeletonBrick} from "@consta/uikit/Skeleton";
import MotorsTable from "../motors-table/motors-table.tsx";
import {useAuth} from "../../../context/AuthContext";
import PageWithHeader from "../../share/page-with-header/page-with-header.tsx";

import "./stand-details.css"

export default function StandDetailsPage() {
    const {standId} = useParams<{ standId: string }>();
    const [stand, setStand] = useState<StandDetails | null>(null);
    const [loadingData, setLoadingData] = useState(true);
    const {setItems} = useBreadcrumbs();

    const {authenticated, loading, keycloak} = useAuth();
    const token = keycloak?.token ?? null;

    useEffect(() => {
        if (!standId) return;
        if (loading || !authenticated || !token) return;

        setLoadingData(true);
        getStandDetails(standId, token)
            .then((s) => {
                setStand(s);
                setItems([
                    {label: "Стенды", path: "/stands"},
                    {label: s.name, path: `/stands/${s.id}`},
                ]);
            })
            .catch(console.error)
            .finally(() => setLoadingData(false));
    }, [standId, setItems, loading, authenticated, token]);

    const getStateBadgeProps = (state: StandDetails | MotorInfo) => {
        switch (state.state) {
            case StandState.On | MotorState.On:
                return {label: "Активен", status: "success" as const, iconLeft: IconAllDone};
            case StandState.Off | MotorState.Off:
                return {label: "Неактивен", status: "error" as const, iconLeft: IconWarning};
        }
    }

    return (
        <PageWithHeader header={(<div style={{display: "flex", alignItems: "center", gap: 16, marginBottom: 24}} headerClassName={"withoutBottomPadding"}>
            {loadingData ? (
                <>
                    <SkeletonBrick className={"skeleton"} height={49} width={250}/>
                </>
            ) : (
                <>
                    <Text size="2xl" weight={"bold"}>{stand?.name}</Text>
                    {stand && <Badge {...getStateBadgeProps(stand)} />}
                </>
            )}
        </div>)}>
            <div style={{marginTop: 32}}>
                {loadingData ? (
                    <div>
                        <SkeletonText className={"skeleton"} rows={1} fontSize={"2xl"}></SkeletonText>
                        <div style={{display: "flex", gap: 16}}>
                            {Array.from({length: 5}, (_, i) => i + 1).map(() => (
                                <SkeletonBrick className={"tableSkeleton skeleton"} style={{marginTop: "2em"}}
                                               height={48} width="20%"/>
                            ))}
                        </div>
                        {Array.from({length: 8}, (_, i) => i + 1).map(() => (
                            <div style={{display: "flex", gap: 16}}>
                                {Array.from({length: 5}, (_, i) => i + 1).map(() => (
                                    <SkeletonBrick height={48} width="100%" className={"tableSkeleton skeleton"}/>
                                ))}
                            </div>
                        ))}
                    </div>
                ) : (
                    <>
                        <Text size="xl" weight={"semibold"}>Электродвигатели</Text>
                        <MotorsTable motors={stand?.motors || []}/>
                    </>
                )}
            </div>
        </PageWithHeader>
    );
}
