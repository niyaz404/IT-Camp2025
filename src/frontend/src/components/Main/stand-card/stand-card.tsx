import { Card } from "@consta/uikit/Card";
import { Badge } from "@consta/uikit/Badge";
import { IconAllDone } from "@consta/icons/IconAllDone";
import { IconWarning } from "@consta/icons/IconWarning";
import { IconQuestion } from "@consta/icons/IconQuestion";
import { Text } from "@consta/uikit/Text";
import { User } from "@consta/uikit/User";
import {Tag} from "@consta/uikit/Tag";
import {type StandInfo, StandState} from "../../../types/common-types.tsx";
import {formatFio} from "../../../utils/string-utils.ts";

import './stand-card.css'

const getStateBadgeProps = (stand: StandInfo) => {
    switch (stand.state) {
        case StandState.On: return { label: "ВКЛЮЧЕН", status: "success" as const, iconLeft: IconAllDone };
        case StandState.Off: return { label: "ВЫКЛЮЧЕН", status: "error" as const, iconLeft: IconWarning };
    }

    return { label: "НЕЗИВЕСТНО", status: "system" as const, iconLeft: IconQuestion };
}

const getDefectsBadgeProps = (stand: StandInfo) => {
    if(stand.defectsCount == 0) {
        return { label: "Дефектов нет", status: "success" as const };
    }

    return { label: `Дефектов: ${stand.defectsCount}`, status: "error" as const };
}

export default function StandCard({stand, onClick}: {stand: StandInfo, onClick?: () => void}) {

    return (
        <Card className={`card ${stand.defectsCount != 0 ? "alert" : ""}`} onClick={() => onClick && onClick()}>
            <div className={"stateBadgeContainer"}>
                <Badge {...getStateBadgeProps(stand)} />
                {stand.defectsCount > 0 && (<Badge {...getDefectsBadgeProps(stand)}/>)}
            </div>

            <Text size="xl" weight="bold" className={"cardTitle"}>
                {stand.name}
            </Text>
            <Text size="s" view="secondary">
                {stand.description}
            </Text>
            <Text size="s" weight={"semibold"} view="secondary">
                {stand.location}
            </Text>

            <div className={"tagsContainer"}>
                <Tag label={`Кол-во двигателей: ${stand.motorsCount}`} />
            </div>
            <div className={"divider"}></div>
            <div className={"responsibleContainer"}>
                <Text size="s" view="secondary">
                    Ответственный
                </Text>
                <User
                    name={formatFio(`${stand.responsiblePerson.lastName} ${stand.responsiblePerson.firstName}`)}
                    size="m"
                />
            </div>
        </Card>
    );
}
