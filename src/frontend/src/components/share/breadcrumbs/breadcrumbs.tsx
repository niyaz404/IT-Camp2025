import {useBreadcrumbs} from "../../../context/BreadcrumbsContext";
import {Breadcrumbs as ConstaBreadcrumbs} from "@consta/uikit/Breadcrumbs";
import {useNavigate} from "react-router-dom";

import "./breadcrumbs.css"
import {SkeletonBrick} from "@consta/uikit/Skeleton";

export default function AppBreadcrumbs() {
    const {items} = useBreadcrumbs();
    const navigate = useNavigate();

    if (!items || items.length === 0) {
        return (
            <div className="breadcrumbs" style={{ display: "flex" }}>
                <SkeletonBrick className={"skeleton"} height={20} width={200} />
            </div>
        );
    }

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