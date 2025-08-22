import {createContext, useContext, useState, type ReactNode} from "react";

export type BreadcrumbItem = {
    label: string;
    path: string;
};

type BreadcrumbsContextType = {
    items: BreadcrumbItem[];
    setItems: (items: BreadcrumbItem[]) => void;
};

const BreadcrumbsContext = createContext<BreadcrumbsContextType | undefined>(undefined);

export const BreadcrumbsProvider = ({children}: {children: ReactNode}) => {
    const [items, setItems] = useState<BreadcrumbItem[]>([]);
    return (
        <BreadcrumbsContext.Provider value={{items, setItems}}>
            {children}
        </BreadcrumbsContext.Provider>
    );
};

export const useBreadcrumbs = () => {
    const ctx = useContext(BreadcrumbsContext);
    if (!ctx) {
        throw new Error("useBreadcrumbs must be used inside BreadcrumbsProvider");
    }
    return ctx;
};