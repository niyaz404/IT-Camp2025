import { Card } from "@consta/uikit/Card";
import { Text } from "@consta/uikit/Text";

export const StatusSection = () => {
    return (
        <>
            <Card verticalSpace="m" horizontalSpace="m">
                <Text size="l" weight="bold">Общий статус</Text>
                <div style={{ marginTop: 16, height: 100, background: "#eee" }}>
                    <Text>Состояние компонентов</Text>
                </div>
            </Card>

            <Card verticalSpace="m" horizontalSpace="m">
                <Text size="l" weight="bold">Рекомендации</Text>
                <div style={{ marginTop: 16 }}>
                    <Text view="secondary">Нет данных</Text>
                </div>
            </Card>
        </>
    );
};