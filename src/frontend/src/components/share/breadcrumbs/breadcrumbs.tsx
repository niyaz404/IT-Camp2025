import { Breadcrumbs as ConstaBreadcrumbs } from "@consta/uikit/Breadcrumbs";
import { useNavigate } from "react-router-dom";

export type BreadcrumbItem = {
    label: string;
    path?: string; // если нет — значит это текущая страница
};

type Props = {
    items: BreadcrumbItem[];
};

export default function AppBreadcrumbs({ items }: Props) {
    const navigate = useNavigate();

    return (
        <ConstaBreadcrumbs
            items={items}
            getItemLabel={(item) => item.label}
            getItemHref={(item) => item.path || ""}
            onItemClick={(item, e) => {
                e.preventDefault();
                if (item.path) navigate(item.path);
            }}
        />
    );
}
