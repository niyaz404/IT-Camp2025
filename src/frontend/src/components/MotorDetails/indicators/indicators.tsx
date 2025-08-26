import { Card } from "@consta/uikit/Card";
import { Text } from "@consta/uikit/Text";
import { Table } from "@consta/uikit/Table";

const rows = [
    { metric: "SNR (f1-2sf1)", value: "8.40 дБ", limit: "≥ 6 дБ" },
    { metric: "SNR (f1-2sf2)", value: "9.20 дБ", limit: "≥ 6 дБ" },
];

const columns = [
    { title: "Метрика", accessor: "metric" },
    { title: "Значение", accessor: "value" },
    { title: "Порог", accessor: "limit" },
];

export const Indicators = () => {
    return (
        <Card verticalSpace="m" horizontalSpace="m">
            <Text size="l" weight="bold">Показатели</Text>

            <div style={{ marginTop: 16, display: "grid", gridTemplateColumns: "1fr 1fr", gap: 16 }}>
                <Table rows={rows} columns={columns} />
                <div style={{ height: 200, background: "#f4f4f4" }}>
                    <Text align="center" view="secondary">Мини-график</Text>
                </div>
            </div>
        </Card>
    );
};
