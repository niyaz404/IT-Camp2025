import {useBreadcrumbs} from "../../../context/BreadcrumbsContext";
import {Breadcrumbs as ConstaBreadcrumbs} from "@consta/uikit/Breadcrumbs";
import {useNavigate} from "react-router-dom";

import "./breadcrumbs.css"

export default function AppBreadcrumbs() {
    const {items} = useBreadcrumbs();
    const navigate = useNavigate();

    return (
        <ConstaBreadcrumbs
            className={"breadcrumbs"}
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