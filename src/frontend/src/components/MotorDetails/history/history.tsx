import { Card } from "@consta/uikit/Card";
import { Text } from "@consta/uikit/Text";
import type {User} from "../../../types/common-types.tsx";

import "./history.css";

type HistoryProps = {
    date: string;
    user: User;
};

function formatDate(isoDate: string): string {
    const date = new Date(isoDate);
    return date.toLocaleString("ru-RU", {
        day: "2-digit",
        month: "2-digit",
        year: "numeric",
        hour: "2-digit",
        minute: "2-digit",
    });
}

export const History = ({ date, user }: HistoryProps) => {
    return (
        <Card className="historyCard">
            <Text size="l" weight="bold" className="historyTitle">
                Последнее изменение
            </Text>
            <div className="historyContent">
                <Text size="s" view="secondary">Дата:</Text>
                <Text size="s">{formatDate(date)}</Text>

                <Text size="s" view="secondary">Пользователь:</Text>
                <Text size="s">{`${user.firstName} ${user.lastName}`}</Text>
            </div>
        </Card>
    );
};
