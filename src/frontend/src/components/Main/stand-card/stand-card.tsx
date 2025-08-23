import { Card } from "@consta/uikit/Card";
import { Badge } from "@consta/uikit/Badge";
import { IconAllDone } from "@consta/icons/IconAllDone";
import { IconWarning } from "@consta/icons/IconWarning";
import { Text } from "@consta/uikit/Text";
import { User } from "@consta/uikit/User";
import {Tag} from "@consta/uikit/Tag";
import {type StandInfo, StandState} from "../../../types/common-types.tsx";
import {formatFio} from "../../../utils/string-utils.ts";

import './stand-card.css'

const getStateBadgeProps = (state: StandInfo) => {
    switch (state.state) {
        case StandState.On: return { label: "ВКЛЮЧЕН", status: "success" as const, iconLeft: IconAllDone };
        case StandState.Off: return { label: "ВЫКЛЮЧЕН", status: "error" as const, iconLeft: IconWarning };
    }
}

export default function StandCard({stand, onClick}: {stand: StandInfo, onClick?: () => void}) {

    return (
        <Card className="card" onClick={() => onClick && onClick()}>
            <div className={"stateBadgeContainer"}>
                <Badge {...getStateBadgeProps(stand)} />
            </div>

            <Text size="xl" weight="bold" className={"cardTitle"}>
                {stand.name}
            </Text>
            <Text size="s" view="secondary">
                {`${stand.description} ${stand.power} кВт`}
            </Text>

            <div className={"tagsContainer"}>
                <Tag label={`Кол-во фаз: ${stand.phasesCount}`} />
                <Tag label={`Частота дискретизации: ${stand.frequency} кГц`} />
                <Tag label={`Tau: ${stand.frequency} кГц`} />
            </div>
            <div className={"divider"}></div>
            <div className={"responsibleContainer"}>
                <Text size="s" view="secondary">
                    Ответственный
                </Text>
                <User
                    name={formatFio(stand.responsiblePerson.username)}
                    size="m"
                />
            </div>
        </Card>
    );
}
