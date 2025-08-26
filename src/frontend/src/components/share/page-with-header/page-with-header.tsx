import {type ReactNode} from "react";
import PageHeader from "../../share/page-header/page-header.tsx";

import "./page-with-header.css"

type PageWithHeaderProps = {
    header: ReactNode;
    children: ReactNode;
    headerClassName: string;
};

export default function PageWithHeader({header, headerClassName, children}: PageWithHeaderProps) {

    return (
        <div className={"container"}>
            <PageHeader className={headerClassName}>
                {header && header}
            </PageHeader>

            <div className={"content"}>
                {children}
            </div>
        </div>
    );
}
