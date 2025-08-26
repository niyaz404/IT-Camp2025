import { Tabs } from "@consta/uikit/Tabs";

import "./dashboard-tabs.css"
const items = [
    { label: "Обзор", id: "overview" },
    { label: "Технические характеристики", id: "specs" },
    { label: "История изменений и обслуживания", id: "history" },
] as const;

export type TabId = typeof items[number]["id"];

type Props = {
    value: TabId;
    onChange: (tab: TabId) => void;
};

export const DashboardTabs = ({ value, onChange }: Props) => {
    return (
        <Tabs className={"tabs"}
            items={items}
            value={items.find((i) => i.id === value)}
            getLabel={(item) => item.label}
            onChange={({ id }) => onChange(id)}
        />
    );
};
