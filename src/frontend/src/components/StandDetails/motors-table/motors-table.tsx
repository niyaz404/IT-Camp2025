import {Table} from "@consta/uikit/Table";
import {Text} from "@consta/uikit/Text";
import {Badge} from "@consta/uikit/Badge";
import {IconAllDone} from "@consta/icons/IconAllDone";
import {
    MotorDefectStatus,
    type MotorInfo,
    MotorState,
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
            return {label: "Активен", status: "success" as const, iconLeft: IconAllDone};
        case StandState.Off:
        case MotorState.Off:
            return {label: "Неактивен", status: "alert" as const, iconLeft: IconAllDone};
    }

    return {label: "Неизвестно", status: "system" as const};
};

const getDefectBadgeProps = (motor: MotorInfo) => {
    switch (motor.defectStatus) {
        case MotorDefectStatus.None:
            return {label: "Дефектов нет", status: "success" as const}
        case MotorDefectStatus.Minor:
            return {label: "Есть дефект", status: "warning" as const}
        case MotorDefectStatus.Critical:
            return {label: "Критический", status: "alert" as const}
    }

    return {label: "Неизвестно", status: "system" as const};
};

export default function MotorsTable({motors}: { motors: MotorInfo[] }) {
    const navigate = useNavigate();
    const {standId} = useParams<{ standId: string }>();
    console.log(standId);

    const columns: TableColumn<MotorInfo>[] = [
        {title: "Название", accessor: "name", width: '3fr', renderCell: (row) => <Text>{row.name}</Text>},
        {
            title: "Состояние",
            accessor: "state",
            width: '2fr',
            renderCell: (row) => <Badge {...getStateBadgeProps(row)} />
        },
        {title: "Тип", accessor: "type", width: '2fr', renderCell: (row) => <Text>{row.type}</Text>},
        {title: "Мощность, кВт", accessor: "power", width: '1fr', renderCell: (row) => <Text>{row.power}</Text>},
        {
            title: "Дефекты",
            accessor: "defectStatus",
            width: '2fr',
            renderCell: (row) => <Badge view={"stroked"} {...getDefectBadgeProps(row)} />
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
