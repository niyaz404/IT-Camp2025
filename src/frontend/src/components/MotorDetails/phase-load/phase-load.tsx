import { Card } from "@consta/uikit/Card";
import { Text } from "@consta/uikit/Text";
import {PhaseLoadChart} from "../phase-load-chart/phase-load-chart.tsx";

export const PhaseLoad = () => {
    return (
        <Card verticalSpace="m" horizontalSpace="m">
            <Text size="l" weight="bold">Дополнительные параметры</Text>
            <Text size="m" style={{ marginTop: 16 }}>Нагрузка по фазам</Text>

            <PhaseLoadChart />
            <div style={{ marginTop: 16, height: 200, background: "#f4f4f4" }}>
                <Text align="center" view="secondary">График</Text>
            </div>
        </Card>
    );
};
