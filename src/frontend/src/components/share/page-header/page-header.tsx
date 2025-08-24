import {ReactNode} from "react";
import "./page-header.css";

type PageHeaderProps = {
    children: ReactNode;
};

export default function PageHeader({children}: PageHeaderProps) {
    return (
        <div className="pageHeader">
            {children}
        </div>
    );
}