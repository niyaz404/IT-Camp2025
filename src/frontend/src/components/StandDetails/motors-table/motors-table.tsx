import { Table } from "@consta/uikit/Table";
import { Text } from "@consta/uikit/Text";
import { Badge } from "@consta/uikit/Badge";
import { IconAllDone } from "@consta/icons/IconAllDone";
import { IconWarning } from "@consta/icons/IconWarning";
import {StandState, MotorState, type MotorInfo, type StandDetails, MotorType} from "../../../types/common-types.tsx";
import type {TableColumn} from "@consta/table/Table";

import "./motors-table.css"

const getStateBadgeProps = (state: StandDetails | MotorInfo) => {
    if (state.state === StandState.On || state.state === MotorState.On) {
        return { label: "ВКЛЮЧЕН", status: "success" as const, iconLeft: IconAllDone };
    }
    if (state.state === StandState.Off || state.state === MotorState.Off) {
        return { label: "ВЫКЛЮЧЕН", status: "error" as const, iconLeft: IconWarning };
    }
    return { label: "Неизвестно", status: "normal" as const };
};

const handleRowClick = (row: MotorInfo) => {
    console.log("Клик по мотору:", row);
    // здесь можно делать редирект или открывать модальное окно
};

export default function MotorsTable({ motors }: { motors: MotorInfo[] }) {
    const columns: TableColumn<MotorInfo>[] = [
        { title: "Название", accessor: "name", renderCell: (row) => <Text>{row.name}</Text> },
        { title: "Статус", accessor: "state", renderCell: (row) => <Badge {...getStateBadgeProps(row)} /> },
        { title: "Тип", accessor: "type", renderCell: (row) => <Text>{row.type}</Text> },
        { title: "Мощность, кВт", accessor: "power", renderCell: (row) => <Text>{row.power}</Text> },
    ];

    return <Table className={"motors"} rows={motors} columns={columns} items={motors} borderBetweenRows onRowClick={handleRowClick} getRowProps={() => ({
        style: { cursor: "pointer" },
    })}/>;
}
