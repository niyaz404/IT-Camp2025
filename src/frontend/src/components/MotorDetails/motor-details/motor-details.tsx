import {useParams} from "react-router-dom";
import {useEffect, useState} from "react";
import { IconAllDone } from "@consta/icons/IconAllDone";
import { IconWarning } from "@consta/icons/IconWarning";
import { IconAlert } from "@consta/icons/IconAlert";
import { IconQuestion } from "@consta/icons/IconQuestion";
import {
    MotorDefectStatus,
    type MotorDetails,
    type MotorInfo,
    MotorState
} from "../../../types/common-types.tsx";
import {useBreadcrumbs} from "../../../context/BreadcrumbsContext.tsx";
import {Text} from "@consta/uikit/Text";
import {Badge} from "@consta/uikit/Badge";
import {SkeletonBrick} from "@consta/uikit/Skeleton";
import { useAuth } from "../../../context/AuthContext";
import {getMotorDetails} from "../../../services/motors.ts";
import {getStandDetails} from "../../../services/stands.ts";
import PageWithHeader from "../../share/page-with-header/page-with-header.tsx";
import {Button} from "@consta/uikit/Button";
import {Avatar} from "@consta/uikit/Avatar";
import {Dashboard} from "../dashboard/dashboard.tsx";
import {DashboardTabs, type TabId} from "../dashboard-tabs/dashboard-tabs.tsx";
import {Characteristics} from "../characteristics/characteristics.tsx";
import {History} from "../history/history.tsx";
import motorImg from '../../../assets/images/default_motor.png';

import "./motor-details.css"
import CsvUploader from "../csv-uploader/csv-uploader.tsx";

export default function MotorDetailsPage() {
    const {motorId} = useParams<{motorId: string}>();
    const [motor, setMotor] = useState<MotorDetails | null>(null);
    const [loadingData, setLoadingData] = useState(true);
    const [activeTab, setActiveTab] = useState<TabId>("overview");
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

    const getStateBadgeProps = (state: MotorInfo) => {
        switch (state.state) {
            case MotorState.On:
                return { label: "Активен", status: "success" as const, iconLeft: IconAllDone };
            case MotorState.Off:
                return { label: "Неактивен", status: "error" as const, iconLeft: IconAlert };
        }

        return {label: "Неизвестно", status: "system" as const, iconLeft: IconQuestion};
    }

    const getDefectBadgeProps = (motor: MotorInfo) => {
        if(motor.maxSeverity == 0){
            return {label: "Дефектов нет", status: "success" as const}
        }

        if(motor.maxSeverity > 80){
            return {label: "Критический", status: "alert" as const}
        }

        if(motor.maxSeverity > 0 && motor.maxSeverity < 80){
            return {label: "Есть дефект", status: "warning" as const}
        }

        return {label: "Неизвестно", status: "system" as const};
    };


    const renderContent = () => {
        switch (activeTab) {
            case "overview":
                return <Dashboard />;
            case "specs":
                return <Characteristics motor={motor} />;
            case "history":
                return <History date={motor?.updatedAt.toString()} user={motor.responsiblePerson} />;
            default:
                return null;
        }
    };

    return (
        <PageWithHeader headerClassName={"withoutBottomPadding"} header={(
            <div className={"motorHeader"}>
                {loadingData ? (
                    <>
                        <SkeletonBrick className={"skeleton"} height={60} width={400} />
                    </>
                ) : (
                    <>
                        <div className={"headerContainer"}>
                            <Avatar className={"avatar"} size="l" url={motorImg} name="Мотор" />
                            <div className={"headerText"}>
                                <div className={"headerFirstRow"}>
                                    <div className={"motorTitle"}>
                                        <Text size="2xl" weight={"bold"}>{motor?.name}</Text>
                                        {motor &&
                                            <>
                                                <Badge {...getStateBadgeProps(motor)} />
                                                <Badge {...getDefectBadgeProps(motor)} />
                                            </>
                                        }
                                    </div>
                                    <div className={"buttonContainer"}>
                                        <CsvUploader />
                                        <Button size={"s"} view={"ghost"} label="Выписать" disabled/>
                                        <Button size={"s"} label="Отправить на ТО" disabled/>
                                    </div>
                                </div>
                                <div className={"headerSecondRow"}>
                                    <Text view={"secondary"}>Производитель</Text>
                                    <Text weight={"semibold"}>{motor?.manufacturer}</Text>
                                    <Text size="s" view="secondary">•</Text>
                                    <Text view={"secondary"}>Заводской номер</Text>
                                    <Text weight={"semibold"}>{motor?.factoryNumber}</Text>
                                </div>
                            </div>
                        </div>
                        <DashboardTabs value={activeTab} onChange={setActiveTab} />
                    </>
                )}
            </div>)}>
            {renderContent()}
        </PageWithHeader>
    );
}
