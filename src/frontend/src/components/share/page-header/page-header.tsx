import {ReactNode} from "react";
import "./page-header.css";

type PageHeaderProps = {
    children: ReactNode;
    className?: string;
};

export default function PageHeader({children, className}: PageHeaderProps) {
    return (
        <div className={`pageHeader ${className}`}>
            {children}
        </div>
    );
}