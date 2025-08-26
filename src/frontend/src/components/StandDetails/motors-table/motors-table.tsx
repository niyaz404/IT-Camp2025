import {Table} from "@consta/uikit/Table";
import {Text} from "@consta/uikit/Text";
import {Badge} from "@consta/uikit/Badge";
import {
    MotorDefectStatus,
    type MotorInfo,
    MotorState, MotorTypeMap,
    type StandDetails,
    StandState
} from "../../../types/common-types.tsx";
import type {TableColumn} from "@consta/table/Table";

import "./motors-table.css"
import {useNavigate, useParams} from "react-router-dom";

const getStateBadgeProps = (state: StandDetails | MotorInfo) => {
    switch (state.state) {
        case StandState.On:
        case MotorState.On:
            return {label: "Активен", status: "success" as const};
        case StandState.Off:
        case MotorState.Off:
            return {label: "Неактивен", status: "alert" as const};
    }

    return {label: "Неизвестно", status: "system" as const};
};

const getDefectBadgeProps = (motor: MotorInfo) => {
    if(motor.maxSeverity == 0){
        return {label: "Дефектов нет", status: "success" as const}
    }

    if(motor.maxSeverity > 80){
        return {label: "Критический", status: "alert" as const}
    }

    return {label: "Есть дефект", status: "warning" as const}

    return {label: "Неизвестно", status: "system" as const};
};

export default function MotorsTable({motors}: { motors: MotorInfo[] }) {
    const navigate = useNavigate();
    const {standId} = useParams<{ standId: string }>();
    console.log(standId);

    const columns: TableColumn<MotorInfo>[] = [
        {
            title: "Состояние",
            accessor: "state",
            width: '2fr',
            renderCell: (row) => <Badge  className={"filledBadge"} {...getStateBadgeProps(row)} />
        },
        {
            title: "Модель",
            accessor: "name",
            width: '3fr',
            renderCell: (row) => <Text weight="bold">{row.name}</Text>
        },
        {title: "Производитель", accessor: "manufacturer", width: '3fr', renderCell: (row) => <Text>{row.manufacturer}</Text>},
        {title: "Заводской номер", accessor: "factoryNumber", width: '3fr', renderCell: (row) => <Text>{row.factoryNumber}</Text>},
        {title: "Тип", accessor: "type", width: '2fr', renderCell: (row) => <Text>{MotorTypeMap[row.type]}</Text>},
        {title: "Мощность, кВт", accessor: "power", width: '1fr', renderCell: (row) => <Text>{row.ratedPower}</Text>},
        {title: "Кол-во дефектов", accessor: "defectsCount", width: '1fr', renderCell: (row) => <Text>{row.defectsCount}</Text>},
        {
            title: "Дефекты",
            accessor: "defectStatus",
            width: '2fr',
            renderCell: (row) => <Badge view={"stroked"} className={"strokedBadge"} {...getDefectBadgeProps(row)} />
        },
    ];

    return <Table className={"motors"}
                  rows={motors}
                  columns={columns}
                  items={motors}
                  borderBetweenRows
                  onRowClick={(row) => navigate(`/stands/${standId}/motors/${row.id}`)}
                  getRowProps={() => ({
                      style: {cursor: "pointer"},
                  })}/>;
}
